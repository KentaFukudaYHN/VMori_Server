using ApplicationCore.Enum;
using System.Collections.Generic;

namespace VMori.ReqRes
{
    /// <summary>
    /// 各ジャンルごとの動画取得要求
    /// </summary>
    public class SearchCriteriaVideoByGenreReq
    {
        /// <summary>
        /// 検索条件
        /// </summary>
        public SearchCriteriaVideoReq SearchReq { get; set; }

        /// <summary>
        /// ジャンルのリスト
        /// </summary>
        public List<VideoGenreKinds> Genres { get; set; }
    }
}
