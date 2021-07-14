using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// 動画視聴履歴IDataService
    /// </summary>
    public interface IVideoHistoryDataService
    {
        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<VideoHistory> Get(string videoId, string ipAddress);

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Regist(VideoHistory entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Update(VideoHistory entity);
    }
}
