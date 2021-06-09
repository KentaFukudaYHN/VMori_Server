using ApplicationCore.Enum;
using ApplicationCore.ServiceReqRes;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IOutsourceVideoService
    {
        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<GetOutsourceVideoServiceRes> GetVideo(string url);

        /// <summary>
        /// 動画情報の登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RegistOutsourceVideoServiceRes> RegistVideo(RegistOutsourceVideoServiceReq req);
    }
}
