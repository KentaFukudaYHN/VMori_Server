using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ReqRes;

namespace VMori.Controllers
{
    [Route("[controller]/{action}")]
    public class AuthController : VMoriBaseController
    {
        private readonly IAuthWorker _authWorker;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="authWorker"></param>
        public AuthController(IAuthWorker authWorker)
        {
            _authWorker = authWorker;
        }

        /// <summary>
        /// ログイン
        /// </summary>
        /// <param name="vModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel vModel)
        {
            var token = await _authWorker.Login(vModel, HttpContext);

            if (string.IsNullOrEmpty(token))
                return StatusCode((int)HttpStatusCode.Unauthorized);

            Response.Cookies.Append("X-Access-Token", token, new CookieOptions() { HttpOnly = true });
            return StatusCode((int)HttpStatusCode.OK);
        }

        /// <summary>
        /// ログイン中かどうか
        /// </summary>
        /// <returns></returns>
        public bool IsLogging()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// メールアドレスの本人認証の最中か
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> CheckAppReqMail(string token)
        {
            return await _authWorker.InMiddleAppReqMail(token);
        }

        /// <summary>
        /// メールアドレス本人確認のメールを要求
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<bool> CreateAppReqMail()
        {
            return await _authWorker.CreateAppReqmail(ADC);
        }

        /// <summary>
        /// メールアドレスの本人認証
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AppReqMailRes> AppReqMail(AppReqMailReq req)
        {
            return await _authWorker.AppReqMail(req);
        }

        /// <summary>
        /// パスワードの変更要求作成
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateChangeReqPassword(CreateChangeReqPasswordReq req)
        {
            return await _authWorker.CreateChangeReqPassword(req);
        }

        /// <summary>
        /// パスワードの変更認証コードのチェック
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<LetterAndSuccessRes> CheckChangeReqPassword(CheckChangeReqPasswordReq req)
        {
            return await _authWorker.CheckChangeReqPassword(req);
        }

        /// <summary>
        /// パスワードの変更要求実行
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<LetterAndSuccessRes> ChangeReqPassword(ChangeReqPasswordReq req)
        {
            return await _authWorker.ChangeReqPassword(req);
        }
    }
}
