using ApplicationCore.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// 外部動画Entity
    /// </summary>
    public class OutsourceVideo : BaseEntity
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

        /// <summary>
        /// 統計情報
        /// </summary>
        public List<OutsourceVideoStatistics> Statistics { get; set; }

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
                if (this.Tags == null)
                    return "";
                return string.Join(',', this.Tags);
            }
            set
            {
                if(!string.IsNullOrEmpty(value))
                    this.Tags = value.Split(',').ToList();
            }
        }

        /// <summary>
        /// 動画の話している言語
        /// </summary>
        [NotMapped]
        public List<VideoLanguageKinds> Langes { get; set; }

        public string LangesData
        {
            get
            {
                if (this.Langes == null)
                    return "";
                return string.Join(',', this.Langes.ConvertAll(x => (int)x));
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    this.Langes = value.Split(',').ToList().ConvertAll(x => (VideoLanguageKinds)System.Enum.ToObject(typeof(VideoLanguageKinds), int.Parse(x)));
            }
        }

        /// <summary>
        /// 翻訳の有無
        /// </summary>
        public bool IsTranslation { get; set; }

        /// <summary>
        /// 翻訳している言語
        /// </summary>
        [NotMapped]
        public List<VideoLanguageKinds> LangForTranslation { get; set; }

        public string LangForTranslationData
        {
            get
            {
                if (this.LangForTranslation == null)
                    return "";
                return string.Join(',', this.LangForTranslation.ConvertAll(x => (int)x));
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    this.LangForTranslation = value.Split(',').ToList().ConvertAll(x => (VideoLanguageKinds)System.Enum.ToObject(typeof(VideoLanguageKinds), int.Parse(x)));
            }
        }

        /// <summary>
        /// 投稿日時
        /// </summary>
        public DateTime PublishDateTime { get; set; }

        /// <summary>
        /// Vtuberの森に登録した日時
        /// </summary>
        public DateTime RegistDateTime { get; set; }
    }
}
