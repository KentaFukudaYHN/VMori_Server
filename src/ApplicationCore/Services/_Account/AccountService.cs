using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ReqRes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthService _authService;
        private readonly IAccountDataService _accountDataService;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IStorageService _storageService;
        private const string USER_ICON_CONTAINER = "user-icons";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccountService(IAuthService authService, IAccountDataService accountDataService, IDateTimeUtility dateTimeUtility, IStorageService storageService)
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
        public async Task<AccountRes> GetLoginAccount(ApplicationDataContainer adc)
        {
            if (string.IsNullOrEmpty(adc.LoginUser.Id))
                throw new ArgumentException("ログインしてないのにアカウント情報にアクセスしようとしています");

            var account = await _accountDataService.GetByIdAsync(adc.LoginUser.Id);

            if (account == null)
                throw new ArgumentException("存在しないアカウントIDです");

            return new AccountRes()
            {
                Name = account.Name,
                DisplayID = account.DisplayID,
                Mail = account.Mail,
                Password = account.Password,
                Gender = account.Gender,
                Icon = account.Icon,
                Birthday = _dateTimeUtility.ConvertStringToDate(account.Birthday)
            };
        }

        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<bool> Regist(RegistAccountReq req)
        {
            //メールアドレスのチェック ※形式が正しくない場合例外が発生
            new MailAddress(req.Mail);

            //メールアドレスの重複チェック
            if (await this.CanRegistMail(req.Mail) == false)
                return false;

            //名前が既に100件以上登録されてないかチェック
            var sameNameAccounts = await _accountDataService.GetListByNameAsync(req.Name);
            if (sameNameAccounts.Count >= 100)
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
                Birthday = req.BirthDay.ToString("yyyyMMdd"),
                RegistDateTime = DateTime.Now
            };

            await _accountDataService.RegistAsync(account);

            //メールアドレスの本人認証要求情報を登録
            await _authService.CreateAppReqMail(req.Mail, req.Name);

            return true;
        }

        /// <summary>
        /// ユーザーアイコンの登録
        /// </summary>
        /// <returns></returns>
        public async Task<string> RegistIcon(Stream stream, string extension,  ApplicationDataContainer adc)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;

            //Blobに画像をアップロード
            if (await _storageService.UploadImg(stream, USER_ICON_CONTAINER, fileName) == false)
                return string.Empty;

            //ファイル名をDBに保存
            if (await _accountDataService.UpdateIcon(fileName, adc) == false)
                return string.Empty;

            return GetIconUrl(fileName);
        }

        /// <summary>
        /// ユーザーアイコンの画像URLを生成
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetIconUrl(string fileName)
        {
            return _storageService.GetStorageDomain() + "/" + USER_ICON_CONTAINER + "/" + fileName;
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

    }
}
