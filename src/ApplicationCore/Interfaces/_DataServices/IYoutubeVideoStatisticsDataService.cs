using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Youtube動画統計情報DataService
    /// </summary>
    public interface IYoutubeVideoStatisticsDataService
    {
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public Task<bool> Regist(YoutubeVideoStatistics entity, IDbContext db);
    }
}
