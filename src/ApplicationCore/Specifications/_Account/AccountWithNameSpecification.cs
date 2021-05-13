using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// アカウント情報を名前で検索
    /// </summary>
    public class AccountWithNameSpecification : BaseSpecification<Account>
    {
        public AccountWithNameSpecification(string name)
            : base(a => a.Name == name) { }
    }
}
