using System.Threading.Tasks;
using VMori.ReqRes;

namespace VMori.Interfaces
{
    /// <summary>
    /// 動画情報Interface
    /// </summary>
    public interface IVideoWorker
    {
        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <returns></returns>
        Task<VideoSummaryInfoRes> GetList(GetVideoSummaryReq req);

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<VideoSummaryInfoRes> GetList(SearchCriteriaVideoReq req);
    }
}
