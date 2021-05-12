
namespace ApplicationCore.Entities
{
    /// <summary>
    /// アプリに関する状態管理クラス
    /// </summary>
    public class ApplicationDataContainer
    {
        /// <summary>
        /// ログインユーザー情報
        /// </summary>
        public LoginUser LoginUser { get; set; }
    }

    /// <summary>
    /// ログインユーザー情報
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// AccountID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名前
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>

        public string Mail { get; set; }
    }
}
