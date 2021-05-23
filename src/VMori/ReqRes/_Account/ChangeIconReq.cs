using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMori.ReqRes
{
    /// <summary>
    /// アイコン変更リクエスト
    /// </summary>
    public class ChangeIconReq
    {
        /// <summary>
        /// base64形式の画像データ
        /// </summary>
        public string base64 { get; set; }

        /// <summary>
        /// ファイル名 ※拡張子付き
        /// </summary>
        public string name { get; set; }
    }
}
