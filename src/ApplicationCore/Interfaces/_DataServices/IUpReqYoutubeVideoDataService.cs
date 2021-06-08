using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Youtube動画アップロードリクエストDataService
    /// </summary>
    public interface IUpReqYoutubeVideoDataService
    {
        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UpReqYoutubeVideo> GetById(string id);

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> Regist(UpReqYoutubeVideo entity);
    }
}
