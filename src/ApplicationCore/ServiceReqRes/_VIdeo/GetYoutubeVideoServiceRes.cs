
using System;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// Youtube動画取得情報
    /// </summary>
    public class GetYoutubeVideoServiceRes
    {
        /// <summary>
        /// 動画タイトル
        /// </summary>
        public string VideoTitle { get; set; }

        /// <summary>
        /// チャンネルタイトル
        /// </summary>
        public string ChanelTitle { get; set; }

        /// <summary>
        /// 動画リンク
        /// </summary>
        public string VideoLink { get; set; }

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
        /// いいね回数
        /// </summary>
        public ulong LikeCount { get; set; }

        /// <summary>
        /// 投稿日時
        /// </summary>
        public DateTime PublishDate { get; set; }
    }
}
