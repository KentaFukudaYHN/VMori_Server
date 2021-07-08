using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMori.ReqRes
{
    /// <summary>
    /// ランキングトップページレスポンス
    /// </summary>
    public class VideoSummaryInfoByGenreRes
    {
        /// <summary>
        /// ジャンルごとの動画情報s
        /// </summary>
        public List<VideoSummaryByGenreRes> Items { get; set; }
    }
}
