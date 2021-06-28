using ApplicationCore.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Outsourceのuploadリクエスト
    /// </summary>
    public class UpReqOutsourceVideo : BaseEntity
    {
        /// <summary>
        /// 動画ID
        /// </summary>
        public string VideoId { get; set; }

        /// <summary>
        /// 動画タイトル
        /// </summary>
        public string VideoTitle { get; set; }

        /// <summary>
        /// 動画チャンネルID
        /// </summary>
        public string ChanelId { get; set; }

        /// <summary>
        /// チャンネル名
        /// </summary>
        public string ChanelTitle { get; set; }

        /// <summary>
        /// 説明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// サムネイルリンク
        /// </summary>
        public string ThumbnailLink { get; set; }

        /// <summary>
        /// 動画プラットフォームの種類
        /// </summary>
        public VideoPlatFormKinds PlatFormKinds { get; set; }

        /// <summary>
        /// 統計情報
        /// </summary>
        public List<OutsourceVideoStatistics> Statistics { get; set; }

        /// <summary>
        /// 投稿日時
        /// </summary>
        public DateTime PublishDateTime { get; set; }

    }
}
