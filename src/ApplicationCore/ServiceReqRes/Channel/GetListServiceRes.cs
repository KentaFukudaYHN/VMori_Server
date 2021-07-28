using System.Collections.Generic;

namespace ApplicationCore.ServiceReqRes.Channel
{
    /// <summary>
    /// チャンネル情報リストレスポンス
    /// </summary>
    public class GetListServiceRes
    { 
        /// <summary>
        /// チャンネル情報のリスト
        /// </summary>
        public List<ChannelServiceRes> Items { get; set; }

        /// <summary>
        /// 総レコード数
        /// </summary>
        public int TotalRecord { get; set; }
    }
}
