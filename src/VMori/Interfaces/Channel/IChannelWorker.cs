

using System.Threading.Tasks;
using VMori.ReqRes.Channel;

namespace VMori.Interfaces.Channel
{
    /// <summary>
    /// チャンネル情報IWorker
    /// </summary>
    public interface IChannelWorker
    {
        /// <summary>
        /// チャンネル情報リストの取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ChannelListRes> GetList(ChannelListReq req);
    }
}
