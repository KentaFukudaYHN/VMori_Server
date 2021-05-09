using System;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// メールアドレス認証Entity
    /// </summary>
    public class AppReqMail : BaseEntity
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// トークン
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime RegistDateTime { get; set; }
    }
}
