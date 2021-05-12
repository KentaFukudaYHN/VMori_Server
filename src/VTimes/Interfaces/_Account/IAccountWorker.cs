using System;
using System.Threading.Tasks;
using VMori.ViewModel;

namespace VMori.Interfaces
{
    public interface IAccountWorker
    {
        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        Task<bool> Regist(RegistAccountViewModel vm);

        /// <summary>
        /// メールアドレスの使用チェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<bool> NotExitsMail(string mail);

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
