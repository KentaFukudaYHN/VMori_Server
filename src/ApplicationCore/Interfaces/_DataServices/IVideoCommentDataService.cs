using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// 動画コメントデータサービスInterface
    /// </summary>
    public interface IVideoCommentDataService
    {
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Regist(VideoComment entity);

        /// <summary>
        /// 動画IDで取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        Task<List<VideoComment>> GetListByVideoId(string videoId);
    }
}
