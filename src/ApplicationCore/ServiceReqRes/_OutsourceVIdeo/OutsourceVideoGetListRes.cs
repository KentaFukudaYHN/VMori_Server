using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// 動画情報レスポンス
    /// </summary>
    public class OutsourceVideoGetListRes
    {
        /// <summary>
        /// 動画情報
        /// </summary>
        public List<OutsourceVideoSummaryServiceRes> Items { get; set; }

        /// <summary>
        /// 総レコード数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
