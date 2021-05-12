using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VMori.ViewModel;

namespace VMori.Interfaces
{
    /// <summary>
    /// 認証Worker
    /// </summary>
    public interface IAuthWorker
    {
        /// <summary>
        /// ログイン
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        Task<bool> Login(LoginViewModel vModel, HttpContext context);

        /// <summary>
        /// メールアドレス本人認証の最中かどうか
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> InMiddleAppReqMail(string token);

        /// <summary>
        /// メールアドレスの本人認証
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>

        Task<AppReqMailRes> AppReqMail(AppReqMailReq req);
    }
}
