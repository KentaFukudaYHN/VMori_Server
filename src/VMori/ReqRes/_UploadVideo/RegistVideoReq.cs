using ApplicationCore.Enum;
using System.Collections.Generic;

namespace VMori.ReqRes
{
    /// <summary>
    /// 動画登録リクエスト
    /// </summary>
    public class RegistVideoReq
    {
        /// <summary>
        /// 登録リクエストID
        /// </summary>
        public string UpReqVideoId { get; set; }

        /// <summary>
        /// 動画のジャンル
        /// </summary>
        public VideoGenreKinds Genre { get; set; }

        /// <summary>
        /// タグ
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// 動画の話している言語
        /// </summary>
        public List<VideoLanguageKinds> Langes { get; set; }

        /// <summary>
        /// 翻訳の有無
        /// </summary>
        public bool IsTranslation { get; set; }

        /// <summary>
        /// 翻訳している言語
        /// </summary>
        public List<VideoLanguageKinds> LangForTranslation { get; set; }

        /// <summary>
        /// 動画プラットフォーム種類
        /// </summary>
        public VideoPlatFormKinds PlatFormKinds { get; set; }
    }
}
