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
        Task<bool> CreateAppReqMail(string accountId, string mail, string userName, bool isNew);

        /// <summary>
        /// パスワードの更新要求
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<bool> CreateChangeReqPassoword(string mail);

        /// <summary>
        /// パスワードの認証Tokenが正しいか
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<(bool, string)> CheckChangeReqPasswordToken(string token, string mail);

        /// <summary>
        /// パスワードの更新実行
        /// </summary>
        /// <param name="token"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<(bool, string)> ChangeReqPassword(string mail, string token, string password);

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


        /// <summary>
        /// パスワードが要件を満たしているかチェック
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CheckPassword(string password);

    }
}
