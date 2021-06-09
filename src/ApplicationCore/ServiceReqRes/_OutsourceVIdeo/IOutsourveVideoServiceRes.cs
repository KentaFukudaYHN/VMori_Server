using System;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// 外部動画サービスの動画情報Baseクラス
    /// </summary>
    public interface IOutsourveVideoServiceRes
    {
        /// <summary>
        /// 動画ID
        /// </summary>
        public string VideoId { get; }
        /// <summary>
        /// 動画タイトル
        /// </summary>
        public string VideoTitle { get; }

        /// <summary>
        /// 動画リンク
        /// </summary>
        public string VideoLink { get; }

        /// <summary>
        /// チャンネルID
        /// </summary>
        public string ChannelId { get; }

        /// <summary>
        /// チャンネルタイトル
        /// </summary>
        public string ChannelTitle { get; }

        /// <summary>
        /// サムネイルリンク
        /// </summary>
        public string ThumbnailLink { get; }

        /// <summary>
        /// 説明欄
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// 投稿日時
        /// </summary>
        public DateTime PublishDateTime { get; }

    }
}
