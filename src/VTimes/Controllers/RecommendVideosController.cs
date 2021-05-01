using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ViewModel;

namespace VMori.Controllers
{
    /// <summary>
    /// おすすめ動画Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendVideosController : Controller
    {
        private IRecommendVideosWorker _worker;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RecommendVideosController(IRecommendVideosWorker worker)
        {
            _worker = worker;
        }

        /// <summary>
        /// おすすめ動画の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RecommendVideoHeaderViewModel> Get()
        {
            return await _worker.GetList();
        }
    }
}
