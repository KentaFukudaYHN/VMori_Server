namespace VMori.ReqRes
{
    /// <summary>
    /// 話している言語の更新リクエスト
    /// </summary>
    public class UpdateVideoLangsReq
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 日本語の有無
        /// </summary>
        public bool SpeakJP { get; set; }

        /// <summary>
        /// 英語の有無
        /// </summary>
        public bool SpeakEnglish { get; set; }

        /// <summary>
        /// その他言語の有無
        /// </summary>
        public bool SpeakOther { get; set; }

    }
}
