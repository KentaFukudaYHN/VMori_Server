
namespace VMori.ReqRes
{
    /// <summary>
    /// ログイン情報
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LoginViewModel() { }
    }
}
