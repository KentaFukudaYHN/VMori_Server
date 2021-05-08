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
    }
}
