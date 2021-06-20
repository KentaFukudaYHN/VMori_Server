﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// 外部動画のチャンネル情報DataService
    /// </summary>
    public class OutsourceVideoChannelDataService : IOutsouceVideoChannelDataService
    {
        private readonly IAsyncRepository<OutsourceVideoChannel> _reository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public OutsourceVideoChannelDataService(IAsyncRepository<OutsourceVideoChannel> repository)
        {
            _reository = repository;
        }

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<OutsourceVideoChannel> Get(string channelTableId)
        {
            if (string.IsNullOrEmpty(channelTableId))
                throw new ArgumentException("チャンネルIDが空です");

            return await _reository.GetByIdAsync(channelTableId);
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Regist(OutsourceVideoChannel entity, IDbContext db)
        {
            if (string.IsNullOrEmpty(entity.ID) || string.IsNullOrEmpty(entity.ChannelId))
                throw new ArgumentException("IDまたはChannelIDを空にすることはできません");

            await _reository.AddAsync(entity, db);

            return true;
        }
    }
}
