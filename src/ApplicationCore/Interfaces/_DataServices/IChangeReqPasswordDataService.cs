using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// パスワード変更要求
    /// </summary>
    public interface IChangeReqPasswordDataService
    {
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Regist(ChangeReqPassword entity);

        /// <summary>
        /// Tokenで検索
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ChangeReqPassword> GetByAccountID(string accountID);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteById(string id, IDbContext db);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteById(string id);
    }
}
