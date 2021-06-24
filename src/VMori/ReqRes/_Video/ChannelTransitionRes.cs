using ApplicationCore.ServiceReqRes;
using System;

namespace VMori.ReqRes
{
    public class ChannelTransitionRes
    {
        private readonly ChannelTrantisionServiceRes _original;

        /// <summary>
        /// 再生回数
        /// </summary>
        public int? ViewCount => _original.ViewCount;

        /// <summary>
        /// 登録者数
        /// </summary>
        public int? SubscriverCount => _original.SubscriverCount;

        /// <summary>
        /// 日時
        /// </summary>
        public DateTime GetDateTime => _original.GetDateTime;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public ChannelTransitionRes(ChannelTrantisionServiceRes original)
        {
            _original = original;
        }
    }
}
