
using ApplicationCore.Enum;
using System;
using static ApplicationCore.Services.VideoService;

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
        /// 並び順を降順にするかどうか
        /// </summary>
        public bool IsDesc { get; set; }

        /// <summary>
        /// 開始期間
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// 終了期間
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// Vmoriの期間かどうか
        /// </summary>
        public bool? IsPublish { get; set; }

        /// <summary>
        /// 詳細検索条件
        /// </summary>
        public SearchDetailCriteriaVideoReq Detail { get; set; }
    }
}
