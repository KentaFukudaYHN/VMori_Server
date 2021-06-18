using ApplicationCore.Enum;
using System.Collections.Generic;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// 詳細検索条件
    /// </summary>
    public class SearchDetailCriteriaVideoReq
    {
        /// <summary>
        /// 話している言語
        /// </summary>
        public List<VideoLanguageKinds>? Langs { get; set; }

        /// <summary>
        /// 翻訳の有無
        /// </summary>
        public bool? IsTranslation { get; set; }

        /// <summary>
        /// 翻訳している言語
        /// </summary>
        public List<VideoLanguageKinds>? TransrationLangs { get; set; }
    }
}
