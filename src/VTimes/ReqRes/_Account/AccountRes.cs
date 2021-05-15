using ApplicationCore.Enum;
using System;

namespace VMori.ReqRes._Account
{
    /// <summary>
    /// アカウント情報レスポンスクラス
    /// </summary>
    public class AccountRes
    {
        private ApplicationCore.ReqRes.AccountRes _original;

        /// <summary>
        /// 名前
        /// </summary>
        public string Name => _original.Name;

        /// <summary>
        /// 表示ID
        /// </summary>
        public string DisplayID => _original.DisplayID;

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Mail => _original.Mail;

        /// <summary>
        /// パスワード
        /// </summary>

        public string Password => _original.Password;

        /// <summary>
        /// アイコン
        /// </summary>
        public string Icon => _original.Icon;

        /// <summary>
        /// 性別
        /// </summary>
        public GenderKinds Gender => _original.Gender;

        /// <summary>
        /// 誕生日
        /// </summary>

        public DateTime Birthday => _original.Birthday;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public AccountRes(ApplicationCore.ReqRes.AccountRes original)
        {
            _original = original;
        }
    }
}
