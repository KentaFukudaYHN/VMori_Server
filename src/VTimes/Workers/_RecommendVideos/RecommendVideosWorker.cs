using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ViewModel;

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
        public async Task<RecommendVideoHeaderViewModel> GetList()
        {
            var resList = await _recommenVideosService.GetVideos();

            var vList = resList.ConvertAll(x =>
            {
                return new RecommendVieoViewModel(x);
            });

            return new RecommendVideoHeaderViewModel()
            {
                Videos = vList
            };
        }
    }
}
