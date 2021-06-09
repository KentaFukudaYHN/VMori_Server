using ApplicationCore.Enum;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// 動画情報の登録要求Req
    /// </summary>
    public class RegistOutsourceVideoServiceReq
    {
        /// <summary>
        /// 登録動画ID ※UpReqOutsourceVideoのID
        /// </summary>
        public string upReqVideoId { get; set; }

        /// <summary>
        /// タグ
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// 動画のジャンル
        /// </summary>
        public VideoGenreKinds Genre { get; set; }

        /// <summary>
        /// 動画の話している言語
        /// </summary>
        public VideoLanguageKinds[] Langes { get; set; }

        /// <summary>
        /// 翻訳の有無
        /// </summary>
        public bool IsTranslation { get; set; }

        /// <summary>
        /// 翻訳している言語
        /// </summary>
        public VideoLanguageKinds[] LangForTranslation { get; set; }
    }
}
