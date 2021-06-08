using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ReqRes;

namespace VMori.Controllers
{
    /// <summary>
    /// 動画アップロードコントローラー
    /// </summary>
    public class UploadVideoController : VMoriBaseController
    {
        private readonly IUploadVideoWorker _uploadVideoWorker;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="uploadVideoWorker"></param>
        public UploadVideoController(IUploadVideoWorker uploadVideoWorker)
        {
            _uploadVideoWorker = uploadVideoWorker;
        }

        /// <summary>
        /// アップロード予定の動画情報取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<GetUploadVideoInfoRes> GetUploadVideoInfo(string url)
        {
            return await _uploadVideoWorker.GetUploadVideoInfo(url);
        }

        /// <summary>
        /// 動画情報の登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<RegistVideoRes> RegistVideo(RegistVideoReq req)
        {
            return await _uploadVideoWorker.RegistVideo(req);
        }
    }
}
