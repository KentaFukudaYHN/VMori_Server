
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

    }
}
