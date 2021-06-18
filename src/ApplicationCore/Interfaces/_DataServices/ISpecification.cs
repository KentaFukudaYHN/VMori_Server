using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    ///抽出条件設定Interface
    /// </summary>
    public interface ISpecification<T>
    {
        /// <summary>
        /// 判定基準
        /// </summary>
        List<Expression<Func<T, bool>>> Criterias { get; }
        /// <summary>
        /// 結合情報
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }
        /// <summary>
        /// 結合情報
        /// </summary>
        List<string> IncludeStrings { get; }
        /// <summary>
        /// 昇順条件
        /// </summary>
        Expression<Func<T, object>> OrderBy { get; }
        /// <summary>
        /// 降順条件
        /// </summary>
        Expression<Func<T, object>> OrderByDescending { get; }
        /// <summary>
        /// フルテキスト検索
        /// </summary>
        List<Expression<Func<T, bool>>> FullTextCriteria { get; }
        /// <summary>
        /// 件数を絞る
        /// </summary>
        bool IsTake { get; }
        /// <summary>
        /// 取得件数
        /// </summary>
        int Take { get; }
        /// <summary>
        /// x件目のデータ取得(ページングに使用)
        /// </summary>
        int Skip { get;  }
        /// <summary>
        /// ページングの利用有無
        /// </summary>
        bool IsPagingEnabled { get; }
    }
}
