using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// Entity抽出条件Baseクラス
    /// </summary>
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// 判定基準
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// 結合情報
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        /// <summary>
        /// 結合情報
        /// </summary>
        public List<string> IncludeStrings { get; } = new List<string>();

        /// <summary>
        /// 昇順条件
        /// </summary>
        public Expression<Func<T, object>> OrderBy { get; private set; }

        /// <summary>
        /// 降順条件
        /// </summary>
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        /// <summary>
        /// 件数を絞る
        /// </summary>
        public bool IsTake { get; private set; }

        /// <summary>
        /// 件数
        /// </summary>
        public int Take { get; private set; }

        /// <summary>
        /// Entityの結合
        /// </summary>
        /// <param name="includeExpression"></param>
        protected virtual void AddIncludes(Expression<Func<T,object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// Entityの結合
        /// </summary>
        /// <param name="includeString"></param>
        protected virtual void AppIncludes(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        /// <summary>
        /// 昇順設定
        /// </summary>
        /// <param name="orderByExpression"></param>
        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        /// <summary>
        /// 降順設定
        /// </summary>
        /// <param name="orderByDecExpression"></param>
        protected virtual void ApplyOrderByDescending(Expression<Func<T,object>> orderByDecExpression)
        {
            OrderByDescending = orderByDecExpression;
        }

        /// <summary>
        /// 取得件数を設定
        /// </summary>
        /// <param name="take"></param>
        protected virtual void ApplyTake(int take)
        {
            IsTake = true;
            Take = take;
        }
    }
}
