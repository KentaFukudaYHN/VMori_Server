using System;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Youtube動画統計情報
    /// </summary>
    public  class YoutubeVideoStatistics : BaseEntity
    {
        /// <summary>
        /// 元動画のID
        /// </summary>
        public string YoutubeVideoID { get; set; }

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
        /// リレーションされるEntity
        /// </summary>
        public YoutubeVideo YoutubeVideo { get; set; }

        /// <summary>
        /// 統計取得日時
        /// </summary>
        public DateTime GetDateTime { get; set; }
    }
}
