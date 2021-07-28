using ApplicationCore.Entities;
using System;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Channel
{
    /// <summary>
    /// チャンネル情報抽出条件
    /// </summary>
    public class ChannelSpecifications : BaseSpecification<OutsourceVideoChannel>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChannelSpecifications() : base(null) { }

        /// <summary>
        /// ページングの設定
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        public new void ApplyPaging(int page, int take)
        {
            var skip = CalcSkip(page, take);

            base.ApplyPaging(skip, take);
        }

        /// <summary>
        /// 並べ替え
        /// </summary>
        /// <param name="isDesc"></param>
        /// <param name="func"></param>
        public void ApplySort(bool isDesc, Expression<Func<OutsourceVideoChannel, object>> func)
        {
            if (isDesc)
                base.ApplyOrderByDescending(func);
            else
                base.ApplyOrderBy(func);
        }

        /// <summary>
        /// ページングのスキップ数を計算
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <returns></returns>
        private int CalcSkip(int page, int displayNum)
        {
            var skip = 0;
            if (page > 1)
                skip = (page - 1) * displayNum;

            return skip;
        }
    }
}
