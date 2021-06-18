using System.Linq;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// Entityの抽出条件設定クラス
    /// </summary>
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            //条件式が設定されていたら、IQueryableに追加
            if (specification.Criterias != null)
            {
                foreach(var item in specification.Criterias)
                {
                    query = query.Where(item);
                }
            }

            //フルテキスト検索の条件式が設定されていたら、IQuerybleに追加
            if(specification.FullTextCriteria != null)
            {
                foreach(var item in specification.FullTextCriteria)
                {
                    query = query.Where(item);
                }
            }

            //結合
            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include));

            query = specification.IncludeStrings.Aggregate(query,
                (current, include) => current.Include(include));

            //OrderByが設定されていれば書き換える
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);

            }

            //Takeが設定されていれば取得件数を設定する
            if (specification.IsTake)
            {
                query = query.Take(specification.Take);
            }

            //Pageingが設定されていれば適用
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                    .Take(specification.Take);
            }

            return query;
        }
    }
}
