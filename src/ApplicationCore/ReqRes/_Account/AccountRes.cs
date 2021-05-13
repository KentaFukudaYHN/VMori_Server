using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ReqRes
{
    /// <summary>
    /// アカウント情報レスポンスクラス
    /// </summary>
    public class AccountRes
    {
        private readonly Account _original;

        public string Name => _original.Name;

        public AccountRes(Account account)
        {
            _original = account;
        }
    }
}
