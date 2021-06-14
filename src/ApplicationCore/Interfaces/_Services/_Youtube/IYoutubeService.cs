using ApplicationCore.ServiceReqRes;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Youtube動画サービス
    /// </summary>
    public interface IYoutubeService
    {
        /// <summary>
        /// 動画の統計情報取得
        /// </summary>
        /// <param name="outsourceVideoId"></param>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        Task<IOutsourceVideoStatisticsServiceRes> GetVideoStatistics(string youtubeVideoId);

        /// <summary>
        /// 動画IDを取得
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        string GetVideoId(Uri uri);

        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        Task<IOutsourveVideoServiceRes> GetVideo(string youtubeVideoId);

        /// <summary>
        /// 動画のリンクを生成
        /// </summary>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        string CreateVideoLink(string youtubeVideoId);
    }
}
