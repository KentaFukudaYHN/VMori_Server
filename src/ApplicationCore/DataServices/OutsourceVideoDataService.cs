
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// Outsource動画DataService
    /// </summary>
    public class OutsourceVideoDataService : IOutsourceVideoDataService
    {
        private readonly IAsyncRepository<OutsourceVideo> _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public OutsourceVideoDataService(IAsyncRepository<OutsourceVideo> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="video"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<bool> Regist(OutsourceVideo video, IDbContext db)
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
        public async Task<OutsourceVideo> GetByVideoID( string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("IDが設定されていません");

            return (await _repository.ListAsync(new OutsourceVideoWithVideoIdSpecification(videoId))).FirstOrDefault();
        }

    }
}
