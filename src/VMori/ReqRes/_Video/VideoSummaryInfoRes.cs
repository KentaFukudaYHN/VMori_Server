using System.Collections.Generic;


namespace VMori.ReqRes
{
    /// <summary>
    /// 動画情報SummaryRes
    /// </summary>
    public class VideoSummaryInfoRes
    {
        /// <summary>
        /// 総レコード数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 動画リスト
        /// </summary>
        public List<VideoSummaryItem> Items { get; set; }
    }
}
