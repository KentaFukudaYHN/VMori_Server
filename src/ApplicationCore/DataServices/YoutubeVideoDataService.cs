
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// Youtube動画DataService
    /// </summary>
    public class YoutubeVideoDataService : IYoutubeVideoDataService
    {
        private readonly IAsyncRepository<YoutubeVideo> _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public YoutubeVideoDataService(IAsyncRepository<YoutubeVideo> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="video"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<bool> Regist(YoutubeVideo video, IDbContext db)
        {
            if (string.IsNullOrEmpty(video.ID))
                throw new ArgumentException("IDが設定されていません");

            await _repository.AddAsync(video, db);

            return true;
        }

        /// <summary>
        /// ビデオIDで動画を検索
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<YoutubeVideo> GetByVideoID( string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("IDが設定されていません");

            return (await _repository.ListAsync(new YoutubeVideoWithVideoIdSpecification(videoId))).FirstOrDefault();
        }

    }
}
