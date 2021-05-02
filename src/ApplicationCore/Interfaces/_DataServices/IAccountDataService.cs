using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces._DataServices
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
        /// <param name="password"></param>
        public Task<Account> GetAsync(string mail, string password);
    }
}
