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
        Expression<Func<T, bool>> Criteria { get; }
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
    }
}
