
namespace VMori.ReqRes
{
    /// <summary>
    /// パスワード変更要求の認証コードチェックリクエスト
    /// </summary>
    public class CheckChangeReqPasswordRes
    {
        /// <summary>
        /// 成功の有無
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///　エラーメッセージ
        /// </summary>
        public string ErrMsg { get; set; }
    }
}
