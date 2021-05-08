using Microsoft.AspNetCore.Mvc;

namespace VMori.ViewModel
{
    /// <summary>
    /// アカウント登録ViewModel
    /// </summary>
    public class RegistAccountViewModel
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
        /// 名前
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年 誕生日
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// 月 誕生日
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// 日 誕生日
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        ///コンストラクタ
        /// </summary>
        public RegistAccountViewModel() { }
    }
}
