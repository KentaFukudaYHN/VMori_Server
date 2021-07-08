using ApplicationCore.Enum;
using System.Collections.Generic;

namespace VMori.ReqRes
{
    /// <summary>
    /// ジャンルごとの動画情報
    /// </summary>
    public class VideoSummaryByGenreRes
    {
        /// <summary>
        /// 動画ジャンル
        /// </summary>
        public VideoGenreKinds GenreKinds { get; set; }

        public List<VideoSummaryItem> Items { get; set; }
    }
}
