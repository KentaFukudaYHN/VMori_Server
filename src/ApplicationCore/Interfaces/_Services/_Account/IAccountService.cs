using ApplicationCore.ReqRes;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// アカウントInterface
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<bool> Regist(RegistAccountReq req);

        /// <summary>
        /// メールアドレスの使用チェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<bool> NotExistsMail(string mail);

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
        Task<(bool, string)> CertificationAppReqMail(string password, string token);
    }
}
