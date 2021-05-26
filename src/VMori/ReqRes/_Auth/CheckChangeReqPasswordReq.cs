
namespace VMori.ReqRes
{
    /// <summary>
    /// パスワード変更要求の認証コードチェックリクエスト
    /// </summary>
    public class CheckChangeReqPasswordReq
    {
        /// <summary>
        /// 認証コード
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///　対象のメールアドレス
        /// </summary>
        public string Mail { get; set; }
    }
}
