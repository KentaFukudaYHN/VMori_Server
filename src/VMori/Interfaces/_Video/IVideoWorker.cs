using ApplicationCore.Enum;
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
        /// ジャンルごとの動画情報を取得
        /// </summary>
        /// <param name="req"></param>
        /// <param name="genres"></param>
        /// <returns></returns>
        Task<VideoSummaryInfoByGenreRes> GetListByGenre(SearchCriteriaVideoReq req, List<VideoGenreKinds> genres);

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
        /// 動画コメントの取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        Task<List<VideoCommentRes>> GetComments(string videoId);

        /// <summary>
        /// コメントの登録
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="text"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<bool> RegistComment(string videoId, string text, int time);

        /// <summary>
        /// チャンネル情報取得
        /// </summary>
        /// <param name="channelTableId"></param>
        /// <returns></returns>
        Task<ChannelRes> GetChannel(string channelTableId);

    }
}
