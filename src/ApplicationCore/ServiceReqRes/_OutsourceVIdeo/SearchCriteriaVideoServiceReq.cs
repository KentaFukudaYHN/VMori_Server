
using ApplicationCore.Enum;
using static ApplicationCore.Services.OutsourceVideoService;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// 動画検索リクエスト
    /// </summary>
    public class SearchCriteriaVideoServiceReq
    {
        /// <summary>
        /// ページ
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 表示数
        /// </summary>
        public int DisplayNum { get; set; }
        /// <summary>
        /// テキスト検索
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 動画のジャンル
        /// </summary>
        public VideoGenreKinds? Genre { get; set; }

        /// <summary>
        /// 並び順
        /// </summary>
        public SortKinds SortKinds { get; set; }

        /// <summary>
        /// 詳細検索条件
        /// </summary>
        public SearchDetailCriteriaVideoReq Detail { get; set; }
    }
}
