using ApplicationCore.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// 外部動画Entity
    /// </summary>
    [Index(nameof(VideoId), nameof(ViewCount), nameof(CommentCount), nameof(LikeCount), nameof(PublishDateTime), nameof(RegistDateTime))]
    public class Video : BaseEntity
    {
        /// <summary>
        /// 動画ID
        /// </summary>
        public string VideoId { get; set; }

        /// <summary>
        /// 動画プラットフォームの種類
        /// </summary>
        public VideoPlatFormKinds PlatFormKinds { get; set; }

        /// <summary>
        /// 動画タイトル
        /// </summary>
        public string VideoTitle { get; set; }

        /// <summary>
        /// 動画チャンネルID
        /// </summary>
        public string ChanelId { get; set; }

        /// <summary>
        /// チャンネル名
        /// </summary>
        public string ChanelTitle { get; set; }

        /// <summary>
        /// 説明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// サムネイルリンク
        /// </summary>
        public string ThumbnailLink { get; set; }

        ///// <summary>
        ///// 統計情報
        ///// </summary>
        //public List<OutsourceVideoStatistics> Statistics { get; set; }

        /// <summary>
        /// 再生回数
        /// </summary>
        public ulong ViewCount { get; set; }
        /// <summary>
        /// VMoriでの再生回数
        /// </summary>
        public ulong VMoriViewCount { get; set; }

        /// <summary>
        /// コメント数
        /// </summary>
        public ulong CommentCount { get; set; }

        /// <summary>
        /// VMoriでのいいね数
        /// </summary>
        public ulong VMoriLikeCount { get; set; }

        /// <summary>
        /// いいね数
        /// </summary>
        public ulong LikeCount { get; set; }

        /// <summary>
        /// 動画コメント
        /// </summary>
        public List<VideoComment> VideoComments { get; set; }

        /// <summary>
        /// 動画ジャンル
        /// </summary>
        public VideoGenreKinds Genre { get; set; }

        /// <summary>
        /// タグ
        /// </summary>
        [NotMapped]
        public List<string> Tags { get; set; }

        public string TagsData
        {
            get
            {
                if (this.Tags == null || this.Tags.Count == 0)
                    return "";
                return string.Join(',', this.Tags);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    this.Tags = value.Split(',').ToList();
                else
                    this.Tags = new List<string>();
            }
        }

        /// <summary>
        /// 日本語を話しているか
        /// </summary>
        public bool SpeakJP { get; set; }

        /// <summary>
        /// 英語を話しているか
        /// </summary>
        public bool SpeakEnglish { get; set; }

        /// <summary>
        /// その他の言葉を話しているか
        /// </summary>
        public bool SpeakOther { get; set; }


        /// <summary>
        /// 翻訳の有無
        /// </summary>
        public bool IsTranslation { get; set; }

        /// <summary>
        /// 日本語の翻訳がされてるか
        /// </summary>
        public bool TranslationJP { get; set; }

        /// <summary>
        /// 英語の翻訳がされてるか
        /// </summary>
        public bool TranslationEnglish { get; set; }

        /// <summary>
        /// その他の翻訳がされてるか
        /// </summary>
        public bool TranslationOther { get; set; }

        /// <summary>
        /// チャンネル情報
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// 投稿日時
        /// </summary>
        public DateTime PublishDateTime { get; set; }

        /// <summary>
        /// Vtuberの森に登録した日時
        /// </summary>
        public DateTime RegistDateTime { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDateTime { get; set; }

        /// <summary>
        /// 更新回数
        /// </summary>
        public sbyte UpdateCount { get; set; }

        /// <summary>
        /// 有効無効の有無
        /// </summary>
        public bool Available { get; set; }
    }
}
