using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMori.ReqRes
{
    /// <summary>
    /// 　メールアドレスの本人認証結果
    /// </summary>
    public class AppReqMailRes
    {
        /// <summary>
        /// 成功の有無
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrMsg { get; set; }

        public AppReqMailRes()
        {

        }
    }
}
