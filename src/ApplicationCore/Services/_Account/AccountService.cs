using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ReqRes;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataService _accountDataService;
        private readonly IAppReqMailDataService _appReqMailDataService;
        private readonly IHashService _hashService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccountService(IAccountDataService accountDataService, IAppReqMailDataService appReqMailDataService, IHashService hashService)
        {
            _accountDataService = accountDataService;
            _appReqMailDataService = appReqMailDataService;
            _hashService = hashService;
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
            if (await this.NotExistsMail(req.Mail) == false)
                return false;

            //アカウント情報登録
            var account = new Account()
            {
                ID = Guid.NewGuid().ToString(),
                Mail = req.Mail,
                Name = req.Name,
                AppMail = false,
                Password = req.Password, //DataServiceでハッシュ化される
                Birthday = req.BirthDay.ToString("yyyyMMdd"),
                RegistDateTime = DateTime.Now
            };

            await _accountDataService.RegistAsync(account);

            //メールアドレスの本人認証要求情報を登録
            await this.AppReqMail(req.Mail);

            return true;
        }

        /// <summary>
        /// メールアドレスの本人認証要求
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AppReqMail(string mail)
        {
            var appReqMail = new AppReqMail()
            {
                ID = Guid.NewGuid().ToString(),
                Token = Guid.NewGuid().ToString().Replace("-", string.Empty),
                Mail = mail,
                RegistDateTime = DateTime.Now
            };
            return await _appReqMailDataService.Regist(appReqMail);
        }

        /// <summary>
        /// メールアドレスの本人認証の最中
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> InMiddleAppReqMail(string token)
        {
            var appReqMail = await _appReqMailDataService.GetByToken(token);

            if (appReqMail == null)
                return false;

            return this.CheckPeriodAppReqMail(appReqMail.RegistDateTime);
        }

        /// <summary>
        /// メールアドレスの本人認証
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<(bool, string)> CertificationAppReqMail(string mail, string password, string token)
        {
            //AppReqMailの情報を取得
            var appReqMail = await _appReqMailDataService.GetByToken(token);

            if (appReqMail == null)
                return (false, "原因不明のエラーです。認証期間を過ぎている可能性があります。アカウント情報画面から再度メールアドレスの本人確認を行ってください。");

            //期間内か確認
            if (this.CheckPeriodAppReqMail(appReqMail.RegistDateTime) == false)
                return (false, "認証期間を過ぎています。アカウント情報画面から再度メールアドレスの認証を行ってください。");

            //Account情報の取得
            var account = await _accountDataService.GetAsync(mail, password);

            if (account == null)
                return (false, "パスワードが間違っています。");

            //AppReqの更新
            await _accountDataService.UpdateAppMail(account.ID, true);

            return (true, "");
        }

        /// <summary>
        /// メールアドレス本人認証期間内か確認
        /// </summary>
        private bool CheckPeriodAppReqMail(DateTime target)
        {
            //認証は1時間以内
            if (target.AddHours(1) <= DateTime.Now)
                return false;

            return true;
        }

        /// <summary>
        /// メールアドレスの使用チェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<bool> NotExistsMail(string mail)
        {
            return await _accountDataService.GetAsync(mail) == null;
        }

    }
}
