namespace VMori.ReqRes
{
    /// <summary>
    /// メールアドレス変更リクエスト
    /// </summary>
    public class ChangeMailReq
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChangeMailReq() { }
    }
}
