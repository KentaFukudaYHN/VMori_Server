using Microsoft.EntityFrameworkCore;
using System;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// チャンネル推移データ
    /// </summary>
    [Index(nameof(ChannelId))]
    public class ChannelTransition : BaseEntity
    {
        /// <summary>
        /// チャンネルID
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// 再生回数
        /// </summary>
        public int? ViewCount { get; set; }

        /// <summary>
        /// チャンネル登録者数
        /// </summary>
        public int? SubscriverCount { get; set; }

        /// <summary>
        /// 取得日時
        /// </summary>
        public DateTime GetDateTime { get; set; }
    }
}
