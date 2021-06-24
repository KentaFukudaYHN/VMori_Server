using ApplicationCore.Entities;
using System;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// チャンネル推移データServiceRes
    /// </summary>
    public class ChannelTrantisionServiceRes
    {
        private readonly ChannelTransition _original;

        /// <summary>
        /// 再生回数
        /// </summary>
        public int? ViewCount => _original.ViewCount;

        /// <summary>
        /// チャンネル登録者数
        /// </summary>
        public int? SubscriverCount => _original.SubscriverCount;

        /// <summary>
        /// 取得日時
        /// </summary>
        public DateTime GetDateTime => _original.GetDateTime;

        public ChannelTrantisionServiceRes(ChannelTransition original)
        {
            _original = original;
        }
    }
}
