using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// 動画履歴情報
    /// </summary>
    public class VideoHistoryDataService: IVideoHistoryDataService
    {
        private readonly IAsyncRepository<VideoHistory> _repository;

        /// <summary>
        /// 動画視聴履歴情報取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<VideoHistory> Get(string videoId, string ipAddress)
        {
            if (string.IsNullOrEmpty(videoId) || string.IsNullOrEmpty(ipAddress))
                throw new ArgumentException("パラメーターが不正です");

            //検索条件
            var spec = new VideoHistorySpecifications(videoId, ipAddress);

            return (await _repository.ListAsync(spec)).FirstOrDefault();
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Regist(VideoHistory entity)
        {
            if (string.IsNullOrEmpty(entity.ID))
                entity.ID = Guid.NewGuid().ToString();

            await _repository.AddAsync(entity);

            return true;
        }

        public async Task<bool> Update(VideoHistory entity)
        {
            if (string.IsNullOrEmpty(entity.ID))
                throw new ArgumentException("IDが設定されていません");

            await _repository.UpdateAsync(entity);

            return true;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public VideoHistoryDataService(IAsyncRepository<VideoHistory> repository)
        {
            _repository = repository;
        }
    }
}
