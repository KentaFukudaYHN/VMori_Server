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
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Account> GetByIdAsync(string id, string password);

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
        Task<bool> UpdateAppMail(string id, string mail, bool AppMail, IDbContext db);

        /// <summary>
        /// 名前の更新
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdateName(string name, ApplicationDataContainer adc);

        /// <summary>
        /// 誕生日の更新
        /// </summary>
        /// <param name="birthday"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdateBirthday(string birthday, ApplicationDataContainer adc);

        /// <summary>
        /// メールアドレスの更新
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdateMail(string mail, ApplicationDataContainer adc);

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
