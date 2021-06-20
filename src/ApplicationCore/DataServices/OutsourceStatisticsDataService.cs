using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// Outsource動画統計情報DataService
    /// </summary>
    public class OutsourceVideoStatisticsDataService : IOutsourceVideoStatisticsDataService
    {
        private readonly IAsyncRepository<OutsourceVideoStatistics> _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OutsourceVideoStatisticsDataService(IAsyncRepository<OutsourceVideoStatistics> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="outsourceVideoId"></param>
        /// <returns></returns>
        public async Task<OutsourceVideoStatistics> Get(string outsourceVideoId, bool onlyLatest)
        {
            if (string.IsNullOrEmpty(outsourceVideoId))
                throw new ArgumentException("idが設定されていません");

            return  (await _repository.ListAsync(new OutsourceVideoStatisticsSpecifications(outsourceVideoId, onlyLatest))).FirstOrDefault();
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<bool> Regist(OutsourceVideoStatistics entity, IDbContext db)
        {
            if (string.IsNullOrEmpty(entity.ID))
                throw new ArgumentException("IDが設定されていません");

            await _repository.AddAsync(entity, db);

            return true;
        }
    }
}
