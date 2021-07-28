using System.Collections.Generic;

namespace VMori.ReqRes.Channel
{
    /// <summary>
    /// チャンネル情報リストレスポンス
    /// </summary>
    public class ChannelListRes
    {
        /// <summary>
        /// チャンネル情報のリスト
        /// </summary>
        public List<ChannelRes> Items { get; set; }

        /// <summary>
        /// 総レコード数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
