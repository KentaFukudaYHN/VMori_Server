using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Outsource動画統計情報DataService
    /// </summary>
    public interface IOutsourceVideoStatisticsDataService
    {
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public Task<bool> Regist(OutsourceVideoStatistics entity, IDbContext db);
    }
}
