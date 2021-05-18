using ApplicationCore.Entities;
using System.Collections.Generic;
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
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Account> GetByIdAsync(string id);

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
        /// 名前が一致するアカウント全件取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<Account>> GetListByNameAsync(string name);

        /// <summary>
        /// アカウント情報登録
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<bool> RegistAsync(Account account);

        /// <summary>
        /// メールアドレスの本人認証の更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="AppMail"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<bool> UpdateAppMail(string id, bool AppMail, IDbContext db);

        /// <summary>
        /// パスワードの更新
        /// </summary>
        /// <param name="password"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdatePassword(string password, ApplicationDataContainer adc);

        /// <summary>
        /// ユーザーアイコンのファイル名を更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<bool> UpdateIcon(string fileName, ApplicationDataContainer adc);

        /// <summary>
        /// 名前が一致するレコードの件数を取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<int> CountByName(string name);
    }
}
