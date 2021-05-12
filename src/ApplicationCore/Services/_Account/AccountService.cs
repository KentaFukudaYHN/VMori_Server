using ApplicationCore.Config;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ReqRes;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthService _authService;
        private readonly IAccountDataService _accountDataService;
        private readonly IAppReqMailDataService _appReqMailDataService;
        private readonly IMailService _mailService;
        private readonly ClientConfig _clientConfig;
        private readonly IDbContext _db;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccountService(IAuthService authService, IAccountDataService accountDataService, IAppReqMailDataService appReqMailDataService, IMailService mailService, IOptions<ClientConfig> clientConfig, IDbContext db)
        {
            _authService = authService;
            _accountDataService = accountDataService;
            _appReqMailDataService = appReqMailDataService;
            _mailService = mailService;
            _clientConfig = clientConfig.Value;
            _db = db;
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
            await _authService.CreateAppReqMail(req.Mail, req.Name);

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
