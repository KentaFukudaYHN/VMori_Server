using ApplicationCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// アカウント情報
    /// </summary>
    public class Account : BaseEntity
    {
        /// <summary>
        /// ニックネーム
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// メール
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
        /// 生年月日 YYYYMMdd
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime RegistDateTime { get; set; }
    }
}
