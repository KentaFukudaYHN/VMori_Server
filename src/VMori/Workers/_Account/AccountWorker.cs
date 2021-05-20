using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ReqRes;
using VMori.ReqRes._Account;

namespace VMori.Workers
{
    /// <summary>
    /// アカウントWorker
    /// </summary>
    public class AccountWorker : IAccountWorker
    {
        private readonly IAccountService _accountService;
        private readonly IDateTimeUtility _dateTimeUtility;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccountWorker(IAccountService accountService, IDateTimeUtility dateTimeUtility)
        {
            _accountService = accountService;
            _dateTimeUtility = dateTimeUtility;
        }

        /// <summary>
        /// ログイン中のアカウント情報を取得
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<AccountRes> Get(ApplicationDataContainer adc)
        {
            var res = await _accountService.GetLoginAccount(adc);

            return new AccountRes(res);
        }

        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<bool> Regist(ReqRes.RegistAccountReq vm)
        {
            //reqの生成
            var req = new ApplicationCore.ReqRes.RegistAccountReq()
            {
                Mail = vm.Mail,
                Password = vm.Password,
                BirthDay = new DateTime(int.Parse(vm.Year), int.Parse(vm.Month), int.Parse(vm.Day)),
                Name = vm.Name
            };

            return await _accountService.Regist(req);
        }

        /// <summary>
        /// ユーザーアイコンの登録
        /// </summary>
        /// <param name="file"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<string> RegistIcon(string base64, string fileName, ApplicationDataContainer adc)
        {
            var byteData = Convert.FromBase64String(base64);
            using (var ms = new MemoryStream(byteData, 0, byteData.Length))
            {
                ms.Write(byteData, 0, byteData.Length);
                return await _accountService.RegistIcon(ms, Path.GetExtension(fileName), adc);
            }
        }

        /// <summary>
        /// 名前の更新
        /// </summary>
        /// <param name="req"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdateName(ChangeNameReq req, ApplicationDataContainer adc)
        {
            return await _accountService.UpdateName(req.Name, adc);
        }

        /// <summary>
        /// 誕生日の更新
        /// </summary>
        /// <param name="req"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBirthday(ChangebBrthdayReq req, ApplicationDataContainer adc)
        {
            var birthday = _dateTimeUtility.ConvertStringToDate(req.Year, req.Month, req.Date);
            return await _accountService.UpdateBirthday(birthday, adc);

        }

        /// <summary>
        /// メールアドレスの更新
        /// </summary>
        /// <param name="req"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMail(ChangeMailReq req, ApplicationDataContainer adc)
        {
            return await _accountService.UpdateMail(req.Mail, adc);
        }

        /// <summary>
        /// パスワードの更新
        /// </summary>
        /// <param name="password"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassword(string password, ApplicationDataContainer adc)
        {
            return await _accountService.UpdatePassword(password, adc);
        }

        /// <summary>
        /// メールアドレスの使用チェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<bool> CanRegistMail(string mail)
        {
            return await _accountService.CanRegistMail(mail);
        }

        /// <summary>
        /// 登録可能な名前かチェック
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> CanRegistName(string name)
        {
            return await _accountService.CanRegistName(name);
        }
    }
}
