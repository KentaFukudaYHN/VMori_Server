using ApplicationCore.Config;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.RegularExpressions;
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
        private readonly IMailService _mailService;
        private readonly IDbContext _db;
        private readonly IChangeReqPasswordDataService _changeReqPasswordDataService;
        private readonly ClientConfig _clientConfig;
        private readonly MailConfig _mailConfig;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="accountDataService"></param>
        public AuthService(IAccountDataService accountDataService, IAppReqMailDataService appReqMailDataService, IChangeReqPasswordDataService changeReqPasswordDataService,IMailService mailService, IOptions<ClientConfig> clientConfig, IOptions<MailConfig> mailConfig, IDbContext db)
        {
            _accountDataService = accountDataService;
            _appReqMailDataService = appReqMailDataService;
            _mailService = mailService;
            _changeReqPasswordDataService = changeReqPasswordDataService;
            _clientConfig = clientConfig.Value;
            _mailConfig = mailConfig.Value;
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
            var token = this.CreateUniqueToken();
            await this.AppReqMail(accountId, mail, token);

            //本人認証用のメールを送信
            var title = "こんにちは！" + userName + "さん！";
            var btnText = "メールアドレスを認証する";
            var btnLink = "https://" + _clientConfig.Domain + "/AppReqMail?token=" + token;
            string content;
            if (isNew)
            {
                content = "Vtuberの森アカウントの登録ありがとうございます！<br>" +
                                "登録したメールアドレスが本人のものか確認する必要があります。<br>" +
                                "『メールアドレスを認証する』をクリックして、パスワードを入力して本人認証を行ってください。";
            }
            else
            {
                content = "メールアドレスが本人のものか確認する必要があります。<br/>" +
                            "『メールアドレスを認証する』をクリックして、パスワードを入力して本人認証を行ってください。";
            }

            var htmlMail =
                "<div>" +
                    this.CreateHtmlMailContent(title, content) +
                    this.CreateHtmlMailBtn(btnText, btnLink) +
                "</div>";

            await _mailService.SendMail(mail, "[Vtuberの森]メールアドレスの本人確認", htmlMail);

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
        /// パスワードの変更要求作成
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<bool> CreateChangeReqPassoword(string mail)
        {
            //アカウント情報を取得
            var account = await _accountDataService.GetAsync(mail);

            var title = "[Vtuberの森]パスワードの変更";
            if (account == null)
            {
                //メールアドレスチェック
                new MailAddress(mail);
                var noAccountMsg = "Vtuberの森をご利用いただきありがとうございます！<br>" +
                                    "このメールはVtuberの森のパスワードリセットを行う為に、入力されたメールアドレスに自動送信されています。<br>" +
                                    "こちらのメールアドレスはVtuberの森への登録がありません！<br>" +
                                    "再度ご確認をお願いします";

                var htmlNoAccountMsg =
                    "<div>" +
                        this.CreateHtmlMailContent("", noAccountMsg) +
                    "</div>";

                await _mailService.SendMail(mail, title, htmlNoAccountMsg);
                return false;
            }

            //6文字の認証コードを生成
            var code = new Random().Next(0, 9999).ToString();
            var changeReqPassword = new ChangeReqPassword()
            {
                ID = Guid.NewGuid().ToString(),
                AccountID = account.ID,
                Code = code,
                RegistDateTime = DateTime.Now
            };

            //パスワード認証用のメールを送信
            var titleMsg = "こんにちは！" + account.Name + "さん！";
            var msg =
                "このメールはVtuberの森のパスワードリセットを行う為に、入力されたメールアドレスに自動送信されています。<br>" +
                "こちらの認証コードを入力してパスワードのリセットを行ってください<br><br>" +
                code;

            var htmlMsg =
                "<div>" +
                    this.CreateHtmlMailContent(titleMsg, msg) +
                "</div>";
            await _mailService.SendMail(mail, title, htmlMsg);

            //既にアカウントに対してメールアドレス変更要求がある場合は削除
            var exitChangeReqpassword = await _changeReqPasswordDataService.GetByAccountID(account.ID);
            if(exitChangeReqpassword != null)
            {
                await _changeReqPasswordDataService.DeleteById(exitChangeReqpassword.ID);
            }

            return await _changeReqPasswordDataService.Regist(changeReqPassword);
        }

        /// <summary>
        /// パスワード変更Tokenが有効か確認
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<(bool, string)> CheckChangeReqPasswordToken(string code, string mail)
        {
            if (code == null || mail == null)
                throw new ArgumentException("Tokenまたはメールアドレスが空です");

            //メールアドレスからアカウント情報を取得
            var account = await _accountDataService.GetAsync(mail);

            //不正なアクセスな可能性があるので、メールアドレスに該当するアカウントがいないことはいわない
            if (account == null)
                return (false, "原因不明のエラーです。再度パスワードのリセットを行ってください");

            //Token情報を取得
            var target = await _changeReqPasswordDataService.GetByAccountID(account.ID);

            if (target == null)
                return (false, "認証コードの期間が過ぎている可能性があります\r\n、再度パスワードのリセットを行ってください。");

            //期間内かチェック
            if (this.CheckPeriodChangeReqPassword(target.RegistDateTime) == false)
                return (false, "認証コードの期限が過ぎています。お手数ですが再度パスワードリセットを行ってください。");

            //codeが正しいかチェック
            if (target.Code != code)
            {
                //セキュリティの観点から間違ったコードの情報を削除する
                await _changeReqPasswordDataService.DeleteById(target.ID);
                await this.CreateChangeReqPassoword(mail);
                return (false, "認証コードが違います。該当のメールアドレスに再度認証コードを送りましたので、メールに記載されている認証コードを入力してください");
            }

            return (true, "");
        }

        /// <summary>
        /// パスワードの変更要求実行
        /// </summary>
        /// <param name="token"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<(bool, string)> ChangeReqPassword(string mail, string code, string password)
        {
            //パスワードが要件を見たいしてるかチェック
            if (this.CheckPassword(password) == false)
                return (false, "パスワードの要件を満たしていません。8文字以上で半角英数字大文字を含む必要があります");

            //アカウント情報を取得
            var account = await _accountDataService.GetAsync(mail);

            if (account == null)
                return (false, "原因不明のエラーです。再度パスワードのリセットを申請してください");

            //ChangeReqPassword1の情報を取得
            var changeReqPassword = await _changeReqPasswordDataService.GetByAccountID(account.ID);

            if (changeReqPassword == null)
                return (false, "認証コードの期限が過ぎている可能性があります。再度パスワードのリセット申請を行ってください");

            //期間内か確認
            if (this.CheckPeriodChangeReqPassword(changeReqPassword.RegistDateTime) == false)
                return (false, "認証コードの期限が過ぎてきます。再度パスワードのリセット申請を行ってください。");

            //コードが正しいか確認
            if (code != changeReqPassword.Code)
            {
                //セキュリティの観点から間違ったコードの情報を削除する
                await _changeReqPasswordDataService.DeleteById(changeReqPassword.ID);
                return (false, "原因不明のエラーです。再度パスワードのリセットを申請してください");
            }


            using (var tx = _db.Database.BeginTransaction())
            {
                try
                {
                    //アカウント情報の更新
                    await _accountDataService.UpdatePassword(account.ID, password, _db);

                    //AppReqMailのレコードを削除
                    await _changeReqPasswordDataService.DeleteById(changeReqPassword.ID, _db);

                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return (false, "原因不明のエラーです。お手数ですが再度アカウント情報画面からメールアドレスの本人確認を行ってください。");
                }
            }

            //パスワードが変更された事を通知
            var msg = "パスワードの変更が完了しました！<br>" +
                    "もしパスワードの変更に身に覚えがない場合は下記のサポートメールアドレスにメールをください。<br><br>" +
                    _mailConfig.SupportMailAddress;
            var htmlMsg =
                "<div>" +
                    this.CreateHtmlMailContent("", msg) +
                "</div>";

            await _mailService.SendMail(mail, "[Vtuberの森]パスワードの変更", htmlMsg);

            return (true, "");
        }

        /// <summary>
        /// パスワードが要件を満たしているかチェック
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            //パスワードは8文字から100文字以内
            if (password.Length < 8 || password.Length >= 100)
            {
                return false;
            }

            //半角英数字大文字を含んでいるか
            if (Regex.IsMatch(password, "(?=.*?[a-z])(?=.*?[A-Z])(?=.*?\\d)") == false)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// HTMLメールの内容を生成
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string CreateHtmlMailContent(string title, string msg)
        {
            var h1Tag = "";
            if(string.IsNullOrEmpty(title) == false)
            {
                h1Tag = 
                "<h1 style=\"margin-bottom:40px;font-size:22px;size:22px;\">" +
                    title +
                "</h1>";
            }
            else
            {
                h1Tag =
                    "<div style=\"margin-bottom:20px\"></div>";
            }
                 
            return 
                "<div>" +
                    "<div style=\"margin-top:60px;\">" +
                        "<img style=\"width:150px;\" src=\"https://vmoridev.blob.core.windows.net/public/title_icon.png\"/>" +
                    "</div>" +
                    "<div style=\"margin:50px 30px;\">" +
                        h1Tag +
                    "<div style=\"font-size:16px;size:16px;\">" +
                        msg +
                    "</div>" +
                "</div>";
        }

        /// <summary>
        /// HTMLのメールのボタンを生成
        /// </summary>
        /// <param name="btnText"></param>
        /// <param name="btnLink"></param>
        /// <returns></returns>
        private string CreateHtmlMailBtn(string btnText, string btnLink)
        {
            return
                "<div style=\"margin:50px 30px\">" +
                    "<a href=\"" + btnLink + "\" style=\"box-sizing:border-box;border-radius:5px;border-color:#348eda;font-weight:400;text-decoration:none;display:inline-block;margin:0;padding:12px 45px;color:#ffffff;border:solid 1px #348eda;border-radius:2px;font-size:14px;text-transform:capitalize;background-color:#348eda\" target=\"_blank\">" +
                        "<font style=\"vertical-align: inherit;\">" +
                            "<font style=\"vertical-align: inherit;\">" +
                            btnText +
                         "</font>" +
                    "</a>" +
                "</div>";      
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
        /// パスワード変更可能期間か確認
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool CheckPeriodChangeReqPassword(DateTime target)
        {
            //変更可能時間は1時間以内
            if (target.AddHours(1) <= DateTime.Now)
                return false;

            return true;
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
        /// ユニークなTokenを生成
        /// </summary>
        /// <returns></returns>
        private string CreateUniqueToken()
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
                token = this.CreateUniqueToken();
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
