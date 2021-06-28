using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// 動画コメント
    /// </summary>
    [Index(nameof(VideoId))]
    public class VideoComment: BaseEntity
    {
        /// <summary>
        /// テキスト
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// コメントの時間 ※動画開始時からの秒数
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// 動画ID
        /// </summary>
        public string VideoId { get; set; }

        /// <summary>
        /// リレーションされれるEntity
        /// </summary>
        public OutsourceVideo Video { get; set; }
    }
}
