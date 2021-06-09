using ApplicationCore.ServiceReqRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// 動画サービス
    /// </summary>
    public interface IOutsourcePlatFormVideoService
    {
        /// <summary>
        /// 動画の統計情報取得
        /// </summary>
        /// <param name="outsourceVideoId"></param>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        Task<IOutsourceVideoStatisticsServiceRes> GetVideoStatistics(string youtubeVideoId);

        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        Task<IOutsourveVideoServiceRes> GetVideo(string youtubeVideoId);

        /// <summary>
        /// URLからVideoId取得
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        string GetVideoId(Uri uri);
    }
}
