using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMori.ViewModel
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
