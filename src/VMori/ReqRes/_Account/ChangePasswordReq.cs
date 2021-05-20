namespace VMori.ReqRes
{
    /// <summary>
    /// パスワード変更リクエスト
    /// </summary>
    public class ChangePasswordReq
    {
        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChangePasswordReq() { }
    }
}
