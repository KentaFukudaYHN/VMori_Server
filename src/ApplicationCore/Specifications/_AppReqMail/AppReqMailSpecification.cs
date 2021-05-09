using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// メールアドレス本人認証要求検索条件設定クラス
    /// </summary>
    public class AppReqMailSpecification : BaseSpecification<AppReqMail>
    {
        /// <summary>
        /// Tokenで検索
        /// </summary>
        /// <param name="token"></param>
        public AppReqMailSpecification(string token)
            : base( a => a.Token == token ) { }
    }
}
