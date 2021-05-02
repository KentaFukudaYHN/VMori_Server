using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// アカウント検索条件設定クラス
    /// </summary>
    public class AccountSpecification : BaseSpecification<Account>
    {
        /// <summary>
        /// メールアドレスとパスワードで検索
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="ps"></param>
        public AccountSpecification(string mail, string ps)
            : base(a => a.Mail == mail && a.Password == ps) { }
    }
}
