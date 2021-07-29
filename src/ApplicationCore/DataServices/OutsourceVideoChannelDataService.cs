using ApplicationCore.Entities;
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
        private readonly IAsyncRepository<Channel> _reository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public OutsourceVideoChannelDataService(IAsyncRepository<Channel> repository)
        {
            _reository = repository;
        }

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<Channel> Get(string channelTableId)
        {
            if (string.IsNullOrEmpty(channelTableId))
                throw new ArgumentException("IDが空です");

            return await _reository.GetByIdAsync(channelTableId);
        }

        /// <summary>
        /// チャンネルIDで取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<Channel> GetByChannelId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("チャンネルIDが空です");

            return await _reository.GetByIdAsync(id);
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Regist(Channel entity, IDbContext db)
        {
            if (string.IsNullOrEmpty(entity.ID))
                throw new ArgumentException("IDを空にすることはできません");

            await _reository.AddAsync(entity, db);

            return true;
        }
    }
}
