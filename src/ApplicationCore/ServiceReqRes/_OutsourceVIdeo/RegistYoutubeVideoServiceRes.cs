using ApplicationCore.Enum;


namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// Outsource動画登録Serviceレスポンス
    /// </summary>
    public class RegistOutsourceVideoServiceRes
    {
        /// <summary>
        /// 成功の有無
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// エラー種類
        /// </summary>
        public RegistVideoErrKinds ErrKinds { get; set; }
    }
}
