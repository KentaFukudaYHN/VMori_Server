using Microsoft.EntityFrameworkCore;
using System;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// 外部動画統計情報
    /// </summary>
    [Index(nameof(VideoId))]
    public  class OutsourceVideoStatistics : BaseEntity
    {
        /// <summary>
        /// 元動画のID
        /// </summary>
        public string VideoId { get; set; }

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
        /// リレーションされるEntityの外部キー
        /// </summary>
        public string OutsourceVideoId { get; set; }

        /// <summary>
        /// リレーションされるEntity
        /// </summary>
        public OutsourceVideo OutsourceVideo { get; set; }

        /// <summary>
        /// 統計取得日時
        /// </summary>
        public DateTime GetDateTime { get; set; }
    }
}
