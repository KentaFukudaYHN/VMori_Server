
using ApplicationCore.Entities;
using System;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// Youtube動画統計情報
    /// </summary>
    public class OutsourceVideoStatisticsServiceRes : IOutsourceVideoStatisticsServiceRes
    {
        /// <summary>
        /// 再生回数
        /// </summary>
        public ulong ViewCount { get; set; }

        /// <summary>
        /// いいね数
        /// </summary>
        public ulong LikeCount { get; set; }

        /// <summary>
        /// コメント数
        /// </summary>
        public ulong CommentCount { get; set; }

        /// <summary>
        /// 取得日時
        /// </summary>
        public DateTime GetDateTime { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OutsourceVideoStatisticsServiceRes()
        {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public OutsourceVideoStatisticsServiceRes(OutsourceVideoStatistics original)
        {
            ViewCount = original.ViewCount;
            LikeCount = original.LikeCount;
            CommentCount = original.CommentCount;
            GetDateTime = original.GetDateTime;
        }

    }
}
