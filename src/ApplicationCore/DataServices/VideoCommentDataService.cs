using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// 動画コメントDataService
    /// </summary>
    public class VideoCommentDataService: IVideoCommentDataService
    {
        private readonly IAsyncRepository<VideoComment> _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VideoCommentDataService(IAsyncRepository<VideoComment> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Regist(VideoComment entity)
        {
            if (string.IsNullOrEmpty(entity.ID))
                entity.ID = Guid.NewGuid().ToString();

            try
            {
                await _repository.AddAsync(entity);
            }
            catch (Exception e)
            {

            }

            return true;
        }

        /// <summary>
        /// 動画IDで取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<List<VideoComment>> GetListByVideoId(string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("パラメーターが不正です");

            var result = await _repository.ListAsync(new VideoCommentSpecifications(videoId));
            if (result == null)
                return null;

            return result.ToList();
        }
    }
}
