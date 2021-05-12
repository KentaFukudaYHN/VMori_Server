using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// メールアドレス本人認証要求DataService
    /// </summary>
    public interface IAppReqMailDataService
    {
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Regist(AppReqMail entity);

        /// <summary>
        /// Tokenで参照
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AppReqMail> GetByToken(string token);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string id, IDbContext db);
    }
}
