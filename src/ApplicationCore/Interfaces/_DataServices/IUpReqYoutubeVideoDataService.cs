using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Outsource動画アップロードリクエストDataService
    /// </summary>
    public interface IUpReqOutsourceVideoDataService
    {
        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UpReqOutsourceVideo> GetById(string id);

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> Regist(UpReqOutsourceVideo entity);
    }
}
