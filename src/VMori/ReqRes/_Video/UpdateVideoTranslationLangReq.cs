
namespace VMori.ReqRes
{
    /// <summary>
    /// 翻訳している言語の更新
    /// </summary>
    public class UpdateVideoTranslationLangReq
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 日本語の有無
        /// </summary>
        public bool TranslationJP { get; set; }

        /// <summary>
        /// 英語の有無
        /// </summary>
        public bool TranslationEnglish { get; set; }

        /// <summary>
        /// その他言語の有無
        /// </summary>
        public bool TranslationOther { get; set; }
    }
}
