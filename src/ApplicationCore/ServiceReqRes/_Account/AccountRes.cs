using ApplicationCore.Entities;
using ApplicationCore.Enum;
using System;
namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// アカウント情報レスポンスクラス
    /// </summary>
    public class AccountServiceRes
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表示ID
        /// </summary>
        public string DisplayID { get; set; }

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
        /// 誕生日
        /// </summary>

        public DateTime Birthday { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>

        public DateTime RegistDateTime { get; set; }

        /// <summary>
        /// メールアドレスの本人認証
        /// </summary>
        public bool AppMail { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccountServiceRes()
        {
        }
    }
}
