

using ApplicationCore.Enum;
using ApplicationCore.ServiceReqRes.Channel;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ApplicationCore.Services.Channel.ChannelService;

namespace ApplicationCore.Interfaces._Services.Channel
{
    /// <summary>
    /// チャンネルService
    /// </summary>
    public interface IChannelService
    {
        Task<GetListServiceRes> GetList(int page, int displayNum, ChannelSortKins sortKinds, bool isDesc);
    }
}
