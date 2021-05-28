using ApplicationCore.ServiceReqRes;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IYoutubeVideoService
    {
        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<GetYoutubeVideoServiceRes> GetVideo(string videoId);
    }
}
