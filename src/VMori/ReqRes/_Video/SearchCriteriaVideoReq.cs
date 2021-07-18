using ApplicationCore.Enum;
using System;
using System.Collections.Generic;
using static ApplicationCore.Services.OutsourceVideoService;

namespace VMori.ReqRes
{
    /// <summary>
    /// 動画検索条件リクエスト
    /// </summary>
    public class SearchCriteriaVideoReq
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
        /// 並び順を降順にするか
        /// </summary>
        public bool IsDesc { get; set; }

        /// <summary>
        /// 話している言語
        /// </summary>
        public List<VideoLanguageKinds>? Langs { get; set; }

        /// <summary>
        /// 翻訳の有無
        /// </summary>
        public bool? IsTranslation { get; set; }

        /// <summary>
        /// 開始期間
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// 終了期間
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// Youtubeの期間かどうか
        /// </summary>
        public bool? IsPublish { get; set; }

        /// <summary>
        /// 翻訳している言語
        /// </summary>
        public List<VideoLanguageKinds>? TransrationLangs { get; set; }
    }
}
