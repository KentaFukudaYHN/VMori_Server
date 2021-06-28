using ApplicationCore.ServiceReqRes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IVideoCommentService
    {
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="text"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<bool> Regist(string videoId, string text, int time);

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        Task<List<VideoCommentServiceRes>> GetListByVideoId(string videoId);
    }
}
