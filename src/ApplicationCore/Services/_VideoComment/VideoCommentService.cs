using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    /// <summary>
    /// 動画コメントサービス
    /// </summary>
    public class VideoCommentService: IVideoCommentService
    {
        private readonly IVideoCommentDataService _videoCommentDataService;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VideoCommentService(IVideoCommentDataService videoCommentDataService)
        {
            _videoCommentDataService = videoCommentDataService;
        }

        /// <summary>
        /// コメントの登録
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="text"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task<bool> Regist(string videoId, string text, int time)
        {
            var entity = new VideoComment()
            {
                ID = Guid.NewGuid().ToString(),
                VideoId = videoId,
                Text = text,
                Time = time,
            };

            return await _videoCommentDataService.Regist(entity);
        }

        /// <summary>
        /// 動画コメントの取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<List<VideoCommentServiceRes>> GetListByVideoId(string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("不正なパラメータです");

            var result = await _videoCommentDataService.GetListByVideoId(videoId);

            if (result == null)
                return null;

            return result.ConvertAll(x => new VideoCommentServiceRes(x));
        }
    }
}
