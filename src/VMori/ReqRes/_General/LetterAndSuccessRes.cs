using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMori.ReqRes
{
    /// <summary>
    /// 文字と成功の有無のレスポンス
    /// </summary>
    public class LetterAndSuccessRes
    {
        /// <summary>
        /// 成功の有無
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrMsg { get; set; }
    }
}
