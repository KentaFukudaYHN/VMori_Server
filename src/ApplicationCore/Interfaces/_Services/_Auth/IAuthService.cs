using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// 認証IService
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// ログイン
        /// </summary>
        /// <returns></returns>
        Task<bool> Login(string mail, string ps, HttpContext context);

        /// <summary>
        /// メールアドレス認証準備用の情報作成
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<bool> CreateAppReqMail(string mail, string userName);

        /// <summary>
        /// メールアドレスの本人認証の最中か
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
        Task<(bool, string)> CertificationMail(string password, string token);

    }
}
