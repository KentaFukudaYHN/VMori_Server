

using ApplicationCore.Enum;
using System;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// 動画情報
    /// </summary>
    public class OutsourceVideoSummaryServiceRes : IOutsourveVideoServiceRes
    {
        /// <summary>
        /// 動画ID
        /// </summary>
        public string VideoId { get; set; }
        /// <summary>
        /// 動画タイトル
        /// </summary>
        public string VideoTitle { get; set; }

        /// <summary>
        /// 動画リンク
        /// </summary>
        public string VideoLink { get; set; }

        /// <summary>
        /// チャンネルID
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// チャンネルタイトル
        /// </summary>
        public string ChannelTitle { get; set; }

        /// <summary>
        /// サムネイルリンク
        /// </summary>
        public string ThumbnailLink { get; set; }

        /// <summary>
        /// 説明欄
        /// </summary>
        public string Description { get; set; }

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
        /// Vの森登録日時
        /// </summary>
        public DateTime RegistDateTime { get; set; }

    }
}
