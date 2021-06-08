using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Youtube動画Dataservice
    /// </summary>
    public interface IYoutubeVideoDataService
    {
        /// <summary>
        /// 動画情報を登録
        /// </summary>
        /// <param name="video"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public Task<bool> Regist(YoutubeVideo video, IDbContext db);

        /// <summary>
        /// 動画IDで検索
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        Task<YoutubeVideo> GetByVideoID(string videoId);
    }
}
