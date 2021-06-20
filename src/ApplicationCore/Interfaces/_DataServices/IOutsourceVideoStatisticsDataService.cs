﻿using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Outsource動画統計情報DataService
    /// </summary>
    public interface IOutsourceVideoStatisticsDataService
    {
        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="outsourceVideoId"></param>
        /// <param name="onlyLatest"></param>
        /// <returns></returns>
        Task<OutsourceVideoStatistics> Get(string outsourceVideoId, bool onlyLatest);
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public Task<bool> Regist(OutsourceVideoStatistics entity, IDbContext db);
    }
}
