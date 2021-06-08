using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// Youtube動画統計情報DataService
    /// </summary>
    public class YoutubeVideoStatisticsDataService : IYoutubeVideoStatisticsDataService
    {
        private readonly IAsyncRepository<YoutubeVideoStatistics> _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public YoutubeVideoStatisticsDataService(IAsyncRepository<YoutubeVideoStatistics> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<bool> Regist(YoutubeVideoStatistics entity, IDbContext db)
        {
            if (string.IsNullOrEmpty(entity.ID))
                throw new ArgumentException("IDが設定されていません");

            await _repository.AddAsync(entity, db);

            return true;
        }
    }
}
