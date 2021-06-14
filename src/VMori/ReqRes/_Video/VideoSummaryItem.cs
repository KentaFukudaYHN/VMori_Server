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
        private OutsourceVideoServiceRes _original;

        /// <summary>
        /// ID
        /// </summary>
        public string Id => _original.VideoId;

        /// <summary>
        /// 動画タイトル
        /// </summary>
        public string Title => _original.VideoTitle;

        ///// <summary>
        ///// 動画リンク
        ///// </summary>
        //public string Link => _original.VideoLink;

        /// <summary>
        /// チャンネル名
        /// </summary>
        public string ChannelTitle => _original.ChannelTitle;

        /// <summary>
        /// サムネイルリンク
        /// </summary>
        public string ThumbnailLink => _original.ThumbnailLink;

        ///// <summary>
        ///// 動画説明
        ///// </summary>
        //public string Description => _original.Description;

        /// <summary>
        /// 再生回数
        /// </summary>
        public ulong ViewCount => _original.ViewCount;

        /// <summary>
        /// 動画プラットフォーム種類
        /// </summary>
        public VideoPlatFormKinds PlatFormKinds => _original.PlatFormKinds;

        /// <summary>
        /// 投稿日時
        /// </summary>
        public DateTime PublishDateTime => _original.PublishDateTime;

        /// <summary>
        /// Vtuberの森登録日時
        /// </summary>
        public DateTime RegistDateTime => _original.RegistDateTime;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public VideoSummaryItem(OutsourceVideoServiceRes original)
        {
            _original = original;
        }
    }
}
