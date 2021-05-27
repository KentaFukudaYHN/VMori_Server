using ApplicationCore.Enum;
using ApplicationCore.ServiceReqRes;
using System;

namespace VMori.ReqRes._Account
{
    /// <summary>
    /// アカウント情報レスポンスクラス
    /// </summary>
    public class AccountRes
    {
        private AccountServiceRes _original;

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
        /// アイコン
        /// </summary>
        public string Icon => _original.Icon;

        /// <summary>
        /// 性別
        /// </summary>
        public GenderKinds Gender => _original.Gender;

        /// <summary>
        /// 生年月日 年
        /// </summary>
        public string BirthdayYear => _original.Birthday.ToString("yyyy");

        /// <summary>
        /// 生年月日 月
        /// </summary>
        public string BirthdayMonth => _original.Birthday.ToString("MM");

        /// <summary>
        /// 生年月日 日
        /// </summary>
        public string BirthdayDate => _original.Birthday.ToString("dd");

        /// <summary>
        /// メールアドレスの本人認証
        /// </summary>
        public bool AppMail => _original.AppMail;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public AccountRes(AccountServiceRes original)
        {
            _original = original;
        }
    }
}
