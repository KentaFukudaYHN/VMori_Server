using ApplicationCore.Enum;
using System.Collections.Generic;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// ジャンル別動画ランキング情報Serviceレスポンス
    /// </summary>
    public class OutsourceGetListByGenresRes
    {

        /// <summary>
        /// ジャンル別動画ランキング情報
        /// </summary>
        public Dictionary<VideoGenreKinds, List<OutsourceVideoSummaryServiceRes>> Items { get; set; }
    }
}
