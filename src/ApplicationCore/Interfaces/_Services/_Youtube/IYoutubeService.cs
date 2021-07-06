using ApplicationCore.Entities;
using ApplicationCore.ServiceReqRes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Youtube動画サービス
    /// </summary>
    public interface IYoutubeService
    {
        /// <summary>
        /// 動画IDを取得
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        string GetVideoId(Uri uri);

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="videoIds"></param>
        /// <returns></returns>
        Task<List<OutsourceVideoSummaryServiceRes>> GetVideos(List<string> videoIds);

        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        Task<IOutsourveVideoServiceRes> GetVideo(string youtubeVideoId);

        /// <summary>
        /// チャンネル情報取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        Task<OutsourceVideoChannel> GetChannel(string channelId);

        /// <summary>
        /// 動画のリンクを生成
        /// </summary>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        string CreateVideoLink(string youtubeVideoId);
    }
}
