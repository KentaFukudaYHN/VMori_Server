using ApplicationCore.Config;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    /// <summary>
    /// 認証Service
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IAccountDataService _accountDataService;
        private readonly IAppReqMailDataService _appReqMailDataService;
        private readonly IDbContext _db;
        private readonly ClientConfig _clientConfig;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="accountDataService"></param>
        public AuthService(IAccountDataService accountDataService, IAppReqMailDataService appReqMailDataService, IOptions<ClientConfig> clientConfig, IDbContext db)
        {
            _accountDataService = accountDataService;
            _appReqMailDataService = appReqMailDataService;
            _clientConfig = clientConfig.Value;
            _db = db;
        }

        /// <summary>
        /// ログイン
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public async Task<bool> Login(string mail, string ps, HttpContext context)
        {
            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(ps))
                throw new ArgumentException();

            var account = await _accountDataService.GetAsync(mail, ps);
            if (account == null)
                return false;

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.ID), //ユニーク
                new Claim(ClaimTypes.Name, account.Name),
                new Claim("Mail", account.Mail),
            };

            //一意のID情報
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                //認証セッションの更新許可
                AllowRefresh = true,
                //認証セッションの有効期限を1日に設定
                ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                //認証セッションの要求間の永続化
                IsPersistent = false
            };

            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity),
                authProperties
                );

            return true;
        }

        /// <summary>
        /// メールアドレス認証準備用の情報作成
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<bool> CreateAppReqMail(string accountId, string mail, string userName, bool isNew)
        {
            var token = this.CreateAppReqMailToken();
            await this.AppReqMail(accountId, mail, token);

            //本人認証用のメールを送信
            var msg = "";
            if (isNew)
            {
                msg = "<h2>こんにちは、" + userName + "さん！</h2>"
                        + "<div>Vtuberの森アカウントの登録ありがとうございます！ <br/>" +
                        "登録したメールアドレスが本人のものか確認する必要があります。<br/>" +
                        "以下のリンクをクリックして、パスワードを入力して本人認証を行ってください。<br/></div>" +
                        "<a href='" + _clientConfig.Domain + "/AppReqMail?token=" + token + "'>メールアドレスを認証する</a>";
            }
            else
            {
                msg = "<h2>こんにちは、" + userName + "さん！</h2>" +
                        "メールアドレスが本人のものか確認する必要があります。<br/>" +
                        "以下のリンクをクリックして、パスワードを入力して本人認証を行ってください。<br/></div>" +
                        "<a href='" + _clientConfig.Domain + "/AppReqMail?token=" + token + "'>メールアドレスを認証する</a>";
            }

            //await _mailService.SendMail(req.Mail, "メールアドレスの本人確認", msg);

            return true;
        }

        /// <summary>
        /// メールアドレスの本人認証の最中
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> InMiddleAppReqMail(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

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
        public async Task<(bool, string)> CertificationMail(string password, string token)
        {
            //AppReqMailの情報を取得
            var appReqMail = await _appReqMailDataService.GetByToken(token);

            if (appReqMail == null)
                return (false, "原因不明のエラーです。認証期間を過ぎている可能性があります。アカウント情報画面から再度メールアドレスの本人確認を行ってください。");

            //期間内か確認
            if (this.CheckPeriodAppReqMail(appReqMail.RegistDateTime) == false)
                return (false, "認証期間を過ぎています。アカウント情報画面から再度メールアドレスの認証を行ってください。");

            //Account情報の取得
            var account = await _accountDataService.GetByIdAsync(appReqMail.AccountID, password);

            //重複したメールがないかチェック
            if (await this.CanRegistMail(appReqMail.Mail, appReqMail.AccountID) == false)
                return (false, "既に同じメールアドレスを使用しているユーザーがいます。他のメールアドレスを登録してください。");

            if (account == null)
                return (false, "パスワードが間違っています。");

            using (var tx = _db.Database.BeginTransaction())
            {
                try
                {
                    //AppReqの更新
                    await _accountDataService.UpdateAppMail(account.ID, appReqMail.Mail, true, _db);

                    //AppReqMailのレコードを削除
                    await _appReqMailDataService.Delete(appReqMail.ID, _db);

                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return (false, "原因不明のエラーです。お手数ですが再度アカウント情報画面からメールアドレスの本人確認を行ってください。");
                }
            }

            return (true, "");
        }

        /// <summary>
        ///　登録可能なメールアドレスかチェック
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="targetAccountId"></param>
        /// <returns></returns>
        private async Task<bool> CanRegistMail(string mail, string targetAccountId)
        {
            var accounts = await _accountDataService.GetAsync(mail);
            if (accounts == null)
                return true;

            if (accounts.ID == targetAccountId)
                return true;

            return false;
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
        /// メールアドレスの本人認証用のTokenを生成
        /// </summary>
        /// <returns></returns>
        private string CreateAppReqMailToken()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        /// <summary>
        /// メールアドレスの本人認証要求
        /// </summary>
        /// <returns></returns>
        private async Task<bool> AppReqMail(string accountId, string mail, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                token = this.CreateAppReqMailToken();
            }

            var appReqMail = new AppReqMail()
            {
                ID = Guid.NewGuid().ToString(),
                AccountID = accountId,
                Token = token,
                Mail = mail,
                RegistDateTime = DateTime.Now
            };
            return await _appReqMailDataService.Regist(appReqMail);
        }

        /// <summary>
        /// メールアドレスの本人認証要求
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<bool> AppReqMail(string accountId, string mail)
        {
            return await this.AppReqMail(accountId, mail, null);
        }
    }
}
