using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// アカウント情報取得
    /// </summary>
    public interface IAccountDataService
    {

        /// <summary>
        /// アカウント情報取得
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<Account> GetAsync(string mail);

        /// <summary>
        /// アカウント情報取得
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        Task<Account> GetAsync(string mail, string password);

        /// <summary>
        /// アカウント情報登録
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<bool> RegistAsync(Account account);
    }
}
