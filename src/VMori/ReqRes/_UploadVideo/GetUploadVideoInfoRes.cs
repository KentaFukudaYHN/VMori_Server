
using ApplicationCore.Enum;
using System;

namespace VMori.ReqRes
{
    /// <summary>
    /// アップロード予定動画情報レスポンス
    /// </summary>
    public class GetUploadVideoInfoRes
    {
        /// <summary>
        ///　動画情報取得成功の有無
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// エラー種類
        /// </summary>
        public RegistVideoErrKinds ErrKinds { get; set; }

        /// <summary>
        /// 動画プラットフォームの種類
        /// </summary>
        public VideoPlatFormKinds PlatFormKinds { get; set; }

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
        /// 動画作成リクエストToken
        /// </summary>
        public string UpReqOutsourceVideoToken { get; set; }

        /// <summary>
        /// 投稿日時
        /// </summary>
        public DateTime PublishDate { get; set; }
    }
}
