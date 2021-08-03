using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMori.ReqRes
{
    /// <summary>
    /// 動画タグ更新リスト
    /// </summary>
    public class UpdateVideoTagsReq
    {
        /// <summary>
        /// 動画ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// タグ
        /// </summary>
        public List<string> Tags {get;set;}
    }
}
