using System.Threading.Tasks;
using VMori.ReqRes;

namespace VMori.Interfaces
{
    /// <summary>
    /// 動画アップロードWorker
    /// </summary>
    public interface IUploadVideoWorker
    {
        /// <summary>
        /// 登録予定の動画情報を取得
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<GetUploadVideoInfoRes> GetUploadVideoInfo(string url);

        /// <summary>
        /// 動画情報の登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RegistVideoRes> RegistVideo(RegistVideoReq req);
    }
}
