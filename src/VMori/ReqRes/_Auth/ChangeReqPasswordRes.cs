
namespace VMori.ReqRes
{
    /// <summary>
    /// パスワード変更要求レスポンス
    /// </summary>
    public class ChangeReqPasswordRes
    {
        /// <summary>
        /// 成功の有無
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrMsg { get; set; }
    }
}
