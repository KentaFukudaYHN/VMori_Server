using Microsoft.EntityFrameworkCore;
using System;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// 外部動画のチャンネル情報v
    /// </summary>
    public class OutsourceVideoChannel : BaseEntity
    {
        /// <summary>
        /// チェンネルタイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// チャンネル説明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// チャンネル作成日
        /// </summary>
        public DateTime? PublishAt { get; set; }

        /// <summary>
        /// チャンネルサムネイルURL
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// チャンネルの再生回数
        /// </summary>
        public ulong? ViewCount { get; set; }

        /// <summary>
        /// チャンネルのコメント数
        /// </summary>
        public ulong? CommentCount { get; set; }

        /// <summary>
        /// チャンネル登録者数
        /// </summary>
        public ulong? SubscriverCount { get; set; }

        /// <summary>
        /// チャンネルにアップロードされた動画数
        /// </summary>
        public ulong? VideoCount { get; set; }

        /// <summary>
        /// V森登録日時
        /// </summary>
        public DateTime GetRegistDateTime { get; set; }
    }
}
