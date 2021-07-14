using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// 動画視聴履歴検索条件
    /// </summary>
    public class VideoHistorySpecifications: BaseSpecification<VideoHistory>
    {
        public VideoHistorySpecifications(string videoId, string ipAddress): base(x => x.VideoId == videoId && x.IpAddress == ipAddress) { }
    }
}
