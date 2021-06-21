using ApplicationCore.Enum;
using ApplicationCore.ServiceReqRes;
using System;

namespace VMori.ReqRes
{
    /// <summary>
    /// 動画情報
    /// </summary>
    public class VideoSummaryItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 動画タイトル
        /// </summary>
        public string Title { get; set; }

        ///// <summary>
        ///// 動画リンク
        ///// </summary>
        //public string Link { get; set; }_original.VideoLink;

        /// <summary>
        /// チャンネル名
        /// </summary>
        public string ChannelTitle { get; set; }

        /// <summary>
        /// サムネイルリンク
        /// </summary>
        public string ThumbnailLink { get; set; }

        ///// <summary>
        ///// 動画説明
        ///// </summary>
        //public string Description { get; set; }_original.Description;

        /// <summary>
        /// 再生回数
        /// </summary>
        public ulong ViewCount { get; set; }

        /// <summary>
        /// 動画プラットフォーム種類
        /// </summary>
        public VideoPlatFormKinds PlatFormKinds { get; set; }

        /// <summary>
        /// 投稿日時
        /// </summary>
        public DateTime PublishDateTime { get; set; }

        /// <summary>
        /// Vtuberの森登録日時
        /// </summary>
        public DateTime RegistDateTime { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VideoSummaryItem()
        {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public VideoSummaryItem(OutsourceVideoSummaryServiceRes original)
        {
            Id = original.VideoId;
            Title = original.VideoTitle;
            ChannelTitle = original.ChannelTitle;
            ThumbnailLink = original.ThumbnailLink;
            ViewCount = original.ViewCount;
            PlatFormKinds = original.PlatFormKinds;
            PublishDateTime = original.PublishDateTime;
            RegistDateTime = original.RegistDateTime;
        }
    }
}
