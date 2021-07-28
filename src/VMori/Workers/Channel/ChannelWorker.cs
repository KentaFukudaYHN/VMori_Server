using ApplicationCore.Enum;
using ApplicationCore.Interfaces._Services.Channel;
using ApplicationCore.Services.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMori.Interfaces.Channel;
using VMori.ReqRes.Channel;

namespace VMori.Workers.Channel
{
    /// <summary>
    /// チャンネルワーカー
    /// </summary>
    public class ChannelWorker : IChannelWorker
    {
        private IChannelService _channelService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channelService"></param>
        public ChannelWorker(IChannelService channelService) 
        {
            _channelService = channelService;
        }

        /// <summary>
        /// チャンネル情報のリストを取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ChannelListRes> GetList(ChannelListReq req)
        {
            var result = await _channelService.GetList(req.Page, req.DisplayNum, req.SortKinds, req.IsDesc);

            List<ChannelRes> items = null;
            if (result.Items != null)
                items = result.Items.ConvertAll(x => new ChannelRes(x));

            return new ChannelListRes()
            {
                Items = items,
                TotalCount = result.TotalRecord
            };
        }
    }
}
