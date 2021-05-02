using ApplicationCore.ReqRes._RecommendVideos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// おすすめ動画Serviceのインターフェース
    /// </summary>
    public interface IRecommendVideosService
    {
        /// <summary>
        /// おすすめ動画の取得
        /// </summary>
        /// <returns></returns>
        public Task<List<RecommendVideosRes>> GetVideos();
    }
}
