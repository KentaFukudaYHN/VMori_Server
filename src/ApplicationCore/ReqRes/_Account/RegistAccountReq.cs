using Microsoft.AspNetCore.Http;
using System;

namespace ApplicationCore.ReqRes
{
    /// <summary>
    /// アカウント情報登録リクエスト
    /// </summary>
    public class RegistAccountReq
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
        /// HttpContext ※ログイン処理用
        /// </summary>
        public HttpContext HttpContext { get; set; }

        /// <summary>
        /// 誕生日
        /// </summary>
        public DateTime BirthDay { get; set; }
    }
}
