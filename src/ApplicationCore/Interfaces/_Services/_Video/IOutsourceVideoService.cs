using ApplicationCore.ServiceReqRes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IOutsourceVideoService
    {

        /// <summary>
        /// 動画情報をリストで取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <returns></returns>
        Task<List<OutsourceVideoServiceRes>> GetList(int page, int displayNum);

        /// <summary>
        /// 動画情報をリストで取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        Task<List<OutsourceVideoServiceRes>> GetList(SearchCriteriaVideoServiceReq req);

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<GetOutsourceVideoServiceRes> GetVideoByLink(string url);

        /// <summary>
        /// 動画情報の登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RegistOutsourceVideoServiceRes> RegistVideo(RegistOutsourceVideoServiceReq req);
    }
}
