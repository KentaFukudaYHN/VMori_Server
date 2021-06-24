using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// チャンネル推移データDataService
    /// </summary>
    public interface IChannelTransitionDataService
    {
        /// <summary>
        /// チャンネル推移データ取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        Task<List<ChannelTransition>> GetListByChannelId(string channelId);
    }
}
