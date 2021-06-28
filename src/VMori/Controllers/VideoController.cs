using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<VideoRes> Get(string videoId)
        {
            return await _videoWorker.Get(videoId);
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
        /// チャンネルに紐づく動画を取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<List<VideoSummaryItem>> GetChannelVideos(string channelId, int page, int take)
        {
            return await _videoWorker.GetChannelVideos(channelId, page, take);
        }

        /// <summary>
        /// チャンネル情報取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ChannelRes> GetChannel(string id)
        {
            return await _videoWorker.GetChannel(id);
        }

        /// <summary>
        /// チャンネル推移情報を取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<List<ChannelTransitionRes>> GetChannelTransitions(string channelId)
        {
            return await _videoWorker.GetChannelTransitions(channelId);
        }

        /// <summary>
        /// 動画コメントの取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<List<VideoCommentRes>> GetComments(string videoId)
        {
            return await _videoWorker.GetComments(videoId);
        }

        /// <summary>
        /// 動画コメントの登録
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="text"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> RegistComment(VideoCommentReq req)
        {
            return await _videoWorker.RegistComment(req.VideoId, req.Text, req.Time);
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
