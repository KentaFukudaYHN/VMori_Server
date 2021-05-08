using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 誕生日
        /// </summary>
        public DateTime BirthDay { get; set; }
    }
}
