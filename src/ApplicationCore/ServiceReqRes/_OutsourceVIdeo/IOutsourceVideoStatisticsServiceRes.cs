
namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// 外部動画サービスの動画統計情報
    /// </summary>
    public interface IOutsourceVideoStatisticsServiceRes
    {
        /// <summary>
        /// 再生回数
        /// </summary>
        public ulong ViewCount { get; }

        /// <summary>
        /// いいね数
        /// </summary>
        public ulong LikeCount { get; }

        /// <summary>
        /// コメント数
        /// </summary>
        public ulong CommentCount { get; }
    }
}
