using ApplicationCore.Enum;

namespace VMori.ReqRes
{
    /// <summary>
    /// 動画登録レスポンス
    /// </summary>
    public class RegistVideoRes
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
