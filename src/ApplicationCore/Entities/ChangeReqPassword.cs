using Microsoft.EntityFrameworkCore;
using System;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// パスワード変更申請Entity
    /// </summary>
    [Index(nameof(Code))]
    public class ChangeReqPassword : BaseEntity
    {
        /// <summary>
        /// アカウントID
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// 認証コード
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime RegistDateTime { get; set; }
    }
}
