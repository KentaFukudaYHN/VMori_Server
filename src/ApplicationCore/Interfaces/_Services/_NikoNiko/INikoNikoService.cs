using ApplicationCore.ServiceReqRes;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// ニコニコ動画サービス
    /// </summary>
    public interface INikoNikoService
    {
        /// <summary>
        /// 動画のVideoIDを取得
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        string GetVideoId(Uri uri);

        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        public Task<IOutsourveVideoServiceRes> GetVideo(string nikonikoVideoId);
    }
}
