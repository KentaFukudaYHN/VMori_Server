using Microsoft.EntityFrameworkCore;
using System;


namespace ApplicationCore.Entities
{
    /// <summary>
    /// 動画視聴履歴情報
    /// </summary>
    [Index(nameof(IpAddress), nameof(VideoId))]
    public class VideoHistory : BaseEntity
    {
        /// <summary>
        /// 動画ID
        /// </summary>
        public string VideoId { get; set; }

        /// <summary>
        /// IPアドレス
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime RegistDateTime { get; set; }
    }
}
