using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// 永続データ管理Interface
    /// </summary>
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// 全件取得
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetAll();
        /// <summary>
        /// IDで検索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(string id);

        /// <summary>
        /// 条件検索
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
       
        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity, IDbContext db);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity, IDbContext db);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task UpdateListAsync(List<T> entitys);

        /// <summary>
        /// 特定のカラムを更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="Propertys">更新するカラム</param>
        /// <returns></returns>
        Task UpdateAsyncOnlyClumn(T entity, List<string> Propertys);

        /// <summary>
        /// 特定のカラムの更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="Propertys"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task UpdateAsyncOnlyClumn(T entity, List<string> Propertys, IDbContext db);

        /// <summary>
        /// 特定のカラムを更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ExcludeFields">更新しないカラム</param>
        /// <returns></returns>
        Task UpdateAsyncNotUpdateColumn(T entity, List<string> ExcludeFields);

        /// <summary>
        /// 特定のカラムを更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ExcludeFields"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task UpdateAsyncNotUpdateColumn(T entity, List<string> ExcludeFields, IDbContext db);

        /// <summary>
        /// レコードの総数をカウント
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 検索結果の総数をカウント
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<int> CountAsync(ISpecification<T> spec);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity, IDbContext db);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(string id);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(string id, IDbContext db);
    }
}
