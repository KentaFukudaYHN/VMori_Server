using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ReqRes._RecommendVideos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    /// <summary>
    /// おすすめ動画Service
    /// </summary>
    public class RecommendVideosService : IRecommendVideosService
    {
        private IAsyncRepository<VideoInfo> _videoInfoRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RecommendVideosService(IAsyncRepository<VideoInfo> videoInfoRepository)
        {
            _videoInfoRepository = videoInfoRepository;
        }

        /// <summary>
        /// おすすめ動画の取得
        /// </summary>
        /// <returns></returns>
        public async Task<List<RecommendVideosRes>> GetVideos()
        {
            var resList = await _videoInfoRepository.GetAll();
            var resultList = new List<RecommendVideosRes>();
            for (int i = 0; i < resList.Count(); i++)
            {
                resultList.Add(new RecommendVideosRes(resList[i]));
            }

            return resultList;
        }
    }
}
