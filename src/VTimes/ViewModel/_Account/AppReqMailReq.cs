
namespace VMori.ViewModel
{ 
    /// <summary>
    /// メールアドレス本人認証要求
    /// </summary>
    public class AppReqMailReq
    {
        /// <summary>
        /// トークン
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AppReqMailReq() { }
    }
}
