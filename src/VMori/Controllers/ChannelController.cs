using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VMori.Interfaces.Channel;
using VMori.ReqRes.Channel;

namespace VMori.Controllers
{
    public class ChannelController : VMoriBaseController
    {
        private readonly IChannelWorker _channelWorker;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channelWorker"></param>
        public ChannelController(IChannelWorker channelWorker)
        {
            _channelWorker = channelWorker;
        }

        /// <summary>
        /// チャンネル情報のリストを取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ChannelListRes> GetList([FromQuery]ChannelListReq req)
        {
            return await _channelWorker.GetList(req);
        }
    }
}
