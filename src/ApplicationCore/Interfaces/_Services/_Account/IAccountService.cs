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
        /// 使用可能なメールアドレスかチェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<bool> CanRegistMail(string mail);

        /// <summary>
        /// 使用可能な名前かチェック
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> CanRegistName(string name);
    }
}
