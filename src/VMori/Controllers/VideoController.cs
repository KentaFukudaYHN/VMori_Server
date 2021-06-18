using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ReqRes;

namespace VMori.Controllers
{
    /// <summary>
    /// 動画情報Controller
    /// </summary>
    public class VideoController : VMoriBaseController
    {
        private IVideoWorker _videoWorker;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="videoWorker"></param>
        public VideoController(IVideoWorker videoWorker)
        {
            _videoWorker = videoWorker;
        }

        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<VideoSummaryInfoRes> GetList(int page, int displayNum)
        {
            var req = new GetVideoSummaryReq() { Page = page, DisplayNum = displayNum };
            return await _videoWorker.GetList(req);
        }

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<VideoSummaryInfoRes> GetSearchList(SearchCriteriaVideoReq req)
        {
            return await _videoWorker.GetList(req);
        }
    }
}
