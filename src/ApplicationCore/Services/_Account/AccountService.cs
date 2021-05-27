using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthService _authService;
        private readonly IAccountDataService _accountDataService;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IStorageService _storageService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccountService(IAuthService authService, IAccountDataService accountDataService, IDateTimeUtility dateTimeUtility, IStorageService storageService, IDbContext db)
        {
            _authService = authService;
            _accountDataService = accountDataService;
            _dateTimeUtility = dateTimeUtility;
            _storageService = storageService;
        }

        /// <summary>
        /// ログイン中のアカウント取得
        /// </summary>
        /// <returns></returns>
        public async Task<AccountServiceRes> GetLoginAccount(ApplicationDataContainer adc)
        {
            if (string.IsNullOrEmpty(adc.LoginUser.Id))
                throw new ArgumentException("ログインしてないのにアカウント情報にアクセスしようとしています");

            var account = await _accountDataService.GetByIdAsync(adc.LoginUser.Id);

            if (account == null)
                throw new ArgumentException("存在しないアカウントIDです");

            var icon = string.Empty;
            //画像の取得
            if (string.IsNullOrEmpty(account.Icon) == false)
            {
                var base64 = await _storageService.DownloadImg(account.StorageID, account.Icon);
                //MIMEタイプ取得
                var mime = MimeTypes.GetMimeType(account.Icon);
                icon = "data:" + mime + ";base64," + base64;
            }
            return new AccountServiceRes()
            {
                Name = account.Name,
                DisplayID = account.DisplayID,
                Mail = account.Mail,
                Password = account.Password,
                Gender = account.Gender,
                Icon = icon,
                Birthday = _dateTimeUtility.ConvertStringToDate(account.Birthday),
                AppMail = account.AppMail,
            };
        }

        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<bool> Regist(RegistAccountServiceReq req)
        {
            //メールアドレスのチェック ※形式が正しくない場合例外が発生
            new MailAddress(req.Mail);

            //メールアドレスの重複チェック
            if (await this.CanRegistMail(req.Mail) == false)
                return false;

            //名前が既に100件以上登録されてないかチェック
            var sameNameAccounts = await _accountDataService.GetListByNameAsync(req.Name);
            if (this.CheckOverSameName(sameNameAccounts) == false)
                return false;

            //パスワードのチェック
            if (_authService.CheckPassword(req.Password) == false)
                return false;

            //アカウント情報登録
            var account = new Account()
            {
                ID = Guid.NewGuid().ToString(),
                DisplayID = this.CreateDisplayID(sameNameAccounts),
                Mail = req.Mail,
                Name = req.Name,
                AppMail = false,
                Password = req.Password, //DataServiceでハッシュ化される
                Birthday = _dateTimeUtility.ConvertDateToString(req.BirthDay),
                RegistDateTime = DateTime.Now,
                StorageID = Guid.NewGuid().ToString()
            };

            await _accountDataService.RegistAsync(account);

            //メールアドレスの本人認証要求情報を登録
            await _authService.CreateAppReqMail(account.ID, req.Mail, req.Name, true);

            //ログイン
            await _authService.Login(req.Mail, req.Password, req.HttpContext);

            return true;
        }

        /// <summary>
        /// ユーザーアイコンの登録
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RegistIcon(byte[] base64, string extension,  ApplicationDataContainer adc)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;

            //Blobに画像をアップロード
            if (await _storageService.UploadImg(base64, adc.LoginUser.StorageID, fileName) == false)
                return false;

            //ファイル名をDBに保存
            var beforeIconName = (await _accountDataService.GetByIdAsync(adc.LoginUser.Id)).Icon;
            if (await _accountDataService.UpdateIcon(fileName, adc) == false)
                return false;

            //元のファイルを削除
            await _storageService.DeleteImg(adc.LoginUser.StorageID, beforeIconName);
            return true;
        }

        /// <summary>
        /// 名前の更新
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdateName(string name, ApplicationDataContainer adc)
        {
            //名前が既に100件以上登録されてないかチェック
            var sameNameAccounts = await _accountDataService.GetListByNameAsync(name);
            if (this.CheckOverSameName(sameNameAccounts) == false)
                return false;

            return await _accountDataService.UpdateName(name, adc);
        }

        /// <summary>
        /// 誕生日の更新
        /// </summary>
        /// <param name="birthday"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBirthday(DateTime birthday, ApplicationDataContainer adc)
        {
            return await _accountDataService.UpdateBirthday(_dateTimeUtility.ConvertDateToString(birthday), adc);
        }

        /// <summary>
        /// メールアドレスの更新
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMail(string mail, ApplicationDataContainer adc)
        {
            //メールアドレスのチェック ※形式が正しくない場合例外が発生
            new MailAddress(mail);

            //メールアドレスの重複チェック
            if (await this.CanRegistMail(mail) == false)
                return false;

            return await _authService.CreateAppReqMail(adc.LoginUser.Id, mail, adc.LoginUser.Name, false);
        }

        /// <summary>
        /// パスワードの更新
        /// </summary>
        /// <param name="password"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassword(string password, ApplicationDataContainer adc)
        {
            if (_authService.CheckPassword(password) == false)
                return false;

            //パスワードの更新
            return await _accountDataService.UpdatePassword(password, adc);
        }

        /// <summary>
        /// メールアドレスの使用チェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<bool> CanRegistMail(string mail)
        {
            return await _accountDataService.GetAsync(mail) == null;
        }

        /// <summary>
        /// 名前が登録可能かどうか
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> CanRegistName(string name)
        {
            //100件以上同じ名前がいたらダメ
            if ((await _accountDataService.CountByName(name)) >= 100)
                return false;

            return true;
        }

        /// <summary>
        /// 名前が使われ過ぎてないかチェック
        /// </summary>
        /// <param name="sameNameList"></param>
        /// <returns></returns>
        private bool CheckOverSameName(List<Account> sameNameList)
        {
            return sameNameList.Count < 100;
        }

        /// <summary>
        /// アカウントの表示IDを生成
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string CreateDisplayID(List<Account> sameNameAccounts)
        {
            //名前が一致するアカウントを全件取得　※仕様で登録時に100件以上同じ名前が作れないようにしている
            //var accountList = await _accountDataService.GetListByNameAsync(name);

            //仕様では100件以上だが、別にIDの生成事態は9999件以内なら可能なので、9999件以内か一応確認する
            if (sameNameAccounts.Count >= 9999)
                throw new ArgumentException("9999件以上登録されている名前です");

            //ランダムなIDを生成する
            var r = new Random();
            var id = r.Next(0, 9999).ToString();

            //IDが重複してないかチェック
            bool exitId(string displayId) => sameNameAccounts.Find(x => x.DisplayID == displayId) != null;

            var isExitId = exitId(id);
            while (isExitId)
            {
                id = r.Next(0, 9999).ToString();
                isExitId = exitId(id);
            }

            return id;
        }
    }
}
