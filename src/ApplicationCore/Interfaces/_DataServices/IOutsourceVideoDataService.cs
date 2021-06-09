using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Outsource動画Dataservice
    /// </summary>
    public interface IOutsourceVideoDataService
    {
        /// <summary>
        /// 動画情報を登録
        /// </summary>
        /// <param name="video"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public Task<bool> Regist(OutsourceVideo video, IDbContext db);

        /// <summary>
        /// 動画IDで検索
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        Task<OutsourceVideo> GetByVideoID(string videoId);
    }
}
