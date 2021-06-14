﻿using System.Collections.Generic;


namespace VMori.ReqRes
{
    /// <summary>
    /// 動画情報SummaryRes
    /// </summary>
    public class VideoSummaryInfoRes
    {
        /// <summary>
        /// 動画リスト
        /// </summary>
        public List<VideoSummaryItem> Items { get; set; }
    }
}
