using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ReqRes;

namespace VMori.Workers
{
    /// <summary>
    /// おすすめ動画Worker
    /// </summary>
    public class RecommendVideosWorker : IRecommendVideosWorker
    {
        private IRecommendVideosService _recommenVideosService;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RecommendVideosWorker(IRecommendVideosService recommendVideosService)
        {
            _recommenVideosService = recommendVideosService;
        }

        /// <summary>
        /// おすすめ動画の取得
        /// </summary>
        /// <returns></returns>
        public async Task<RecommendVideoHeaderRes> GetList()
        {
            var resList = await _recommenVideosService.GetVideos();

            var vList = resList.ConvertAll(x =>
            {
                return new RecommendVieoRes(x);
            });

            return new RecommendVideoHeaderRes()
            {
                Videos = vList
            };
        }
    }
}
