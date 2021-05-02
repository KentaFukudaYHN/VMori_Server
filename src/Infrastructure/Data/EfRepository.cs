using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public abstract class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly VMoriContext _db;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="db"></param>
        public EfRepository(VMoriContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 全件取得
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        /// <summary>
        /// IDで検索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(string id)
        {
            return await _db.Set<T>().SingleOrDefaultAsync(x => x.ID == id);
        }

        /// <summary>tin
        /// 条件で検索
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await this.ApplySpecification(spec).ToListAsync();
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            if (string.IsNullOrEmpty(entity.ID))
            {
                entity.ID = Guid.NewGuid().ToString();
            }

            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(string id)
        {
            var target = await _db.Set<T>().SingleOrDefaultAsync(x => x.ID == id);
            _db.Set<T>().Remove(target);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// レコードの総数をカウント
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await _db.Set<T>().CountAsync();
        }

        /// <summary>
        /// クエリの生成
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_db.Set<T>().AsQueryable(), spec);
        }

        /// <summary>
        /// 特定のカラムの更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="Propertys">更新するカラム</param>
        /// <returns></returns>
        public async Task UpdateAsyncOnlyClumn(T entity, List<string> propertys)
        {

            var props = entity.GetType().GetProperties();
            var excludeFields = props.Where(x => !propertys.Contains(x.Name)).Select(x => x.Name).ToList();
            await this.UpdateAsyncNotUpdateColumn(entity, excludeFields);

        }
        /// <summary>
        /// 特定のカラムの更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ExcludeFields">更新しないカラム</param>
        /// <returns></returns>
        public async Task UpdateAsyncNotUpdateColumn(T entity, List<string> ExcludeFields)
        {

            var original = await GetByIdAsync(entity.ID);
            foreach (var originalProp in original.GetType().GetProperties())
            {
                if (!ExcludeFields.Contains(originalProp.Name))
                {
                    var targetProp = entity.GetType().GetProperty(originalProp.Name);
                    originalProp.SetValue(original, targetProp.GetValue(entity));
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}
