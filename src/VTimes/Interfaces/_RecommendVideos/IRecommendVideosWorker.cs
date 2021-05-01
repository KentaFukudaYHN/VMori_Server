using System.Threading.Tasks;
using VMori.ViewModel;

namespace VMori.Interfaces
{
    /// <summary>
    /// おすすめ動画Worker
    /// </summary>
    public interface IRecommendVideosWorker
    {
        /// <summary>
        /// おすすめ動画取得
        /// </summary>
        /// <returns></returns>
        public Task<RecommendVideoHeaderViewModel> GetList();
    }
}
