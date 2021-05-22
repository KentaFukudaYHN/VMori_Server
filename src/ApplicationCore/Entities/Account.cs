using ApplicationCore.Enum;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApplicationCore.Entities
{
    [Index(nameof(Mail))]
    public class Account : BaseEntity
    {
        /// <summary>
        /// 表示用ID
        /// </summary>
        public string DisplayID { get; set; }

        /// <summary>
        /// ニックネーム
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// アイコン
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        public GenderKinds Gender { get; set; }

        /// <summary>
        /// 誕生日 YYYYMMdd
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// メールアドレスの本人確認済み
        /// </summary>
        public bool AppMail { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime RegistDateTime { get; set; }
    }
}
