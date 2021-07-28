using ApplicationCore.DataServices;
using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces._DataServices;
using ApplicationCore.Interfaces._Services.Channel;
using ApplicationCore.ServiceReqRes.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Channel
{
    /// <summary>
    /// チャンネル情報Service
    /// </summary>
    public class ChannelService : IChannelService
    {
        private readonly IChannelDataService _channelDataService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channelDataService"></param>
        public ChannelService(IChannelDataService channelDataService)
        {
            _channelDataService = channelDataService;
        }

        /// <summary>
        /// チャンネルリスト取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <returns></returns>
        public async Task<GetListServiceRes> GetList(int page, int displayNum, ChannelSortKins sortKinds, bool isDesc)
        {
            if (page == 0 || displayNum == 0)
                throw new ArgumentException("パラメーターが不正です");

            var channels = await _channelDataService.GetList(page, displayNum, GetSortFunc(sortKinds), isDesc);

            var totalCount = await _channelDataService.GetCount();

            List<ChannelServiceRes> items = null;
            if (channels != null)
                items = channels.ConvertAll(x => new ChannelServiceRes(x));



            return new GetListServiceRes()
            {
                Items = items,
                TotalRecord = totalCount
            };
        }

        /// <summary>
        /// 並び替えの関数を取得
        /// </summary>
        /// <param name="sortKinds"></param>
        /// <returns></returns>
        private Expression<Func<OutsourceVideoChannel, object>> GetSortFunc(ChannelSortKins sortKinds)
        {
            switch (sortKinds)
            {
                case ChannelSortKins.GetRegistDateTime:
                    return x => x.GetRegistDateTime;
                case ChannelSortKins.SubscriverCount:
                    return x => x.SubscriverCount;
                case ChannelSortKins.ViewCount:
                    return x => x.ViewCount;
                default:
                    throw new ArgumentException("不明なSortKindsです:" + sortKinds);
            }
        }

    }
}
