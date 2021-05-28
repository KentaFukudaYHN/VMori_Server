using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// 動画サービス
    /// </summary>
    public interface IVideoService
    {
        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="videoUrl"></param>
        /// <returns></returns>
        public Task<IGetVideoServiceRes> GetVideos(string videoUrl);
    }
}
