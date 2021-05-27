using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Config
{
    /// <summary>
    /// メール設定情報
    /// </summary>
    public class MailConfig
    {
        /// <summary>
        /// APIキー
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// システム用送信メールアドレス
        /// </summary>
        public string SystemMailAddress { get; set; }

        /// <summary>
        /// サポートメールアドレス
        /// </summary>
        public string SupportMailAddress { get; set; }
    }
}
