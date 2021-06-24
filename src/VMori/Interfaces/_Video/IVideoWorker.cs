using System.Collections.Generic;
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
        /// <param name="videoId"></param>
        /// <returns></returns>
        Task<VideoRes> Get(string videoId);

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

        /// <summary>
        /// チャンネルに紐づく動画のリストを取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<VideoSummaryItem>> GetChannelVideos(string channelId, int page, int take);

        /// <summary>
        /// チャンネル推移情報の取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        Task<List<ChannelTransitionRes>> GetChannelTransitions(string channelId);

        /// <summary>
        /// チャンネル情報取得
        /// </summary>
        /// <param name="channelTableId"></param>
        /// <returns></returns>
        Task<ChannelRes> GetChannel(string channelTableId);
    }
}
