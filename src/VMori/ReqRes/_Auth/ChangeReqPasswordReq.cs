

namespace VMori.ReqRes
{
    /// <summary>
    /// パスワード変更リクエスト
    /// </summary>
    public class ChangeReqPasswordReq
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }
    }
}
