using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// 外部動画のチャンネル情報DataService
    /// </summary>
    public interface IOutsouceVideoChannelDataService
    {
        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        Task<Channel> Get(string channelId);

        /// <summary>
        /// チャンネルIDで取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        Task<Channel> GetByChannelId(string channelId);

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<bool> Regist(Channel entity, IDbContext db);
    }
}
