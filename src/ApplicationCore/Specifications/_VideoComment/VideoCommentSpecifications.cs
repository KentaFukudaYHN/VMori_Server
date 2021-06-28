using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// 動画コメント検索条件
    /// </summary>
    public class VideoCommentSpecifications: BaseSpecification<VideoComment>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VideoCommentSpecifications(string videoId): base(x => x.VideoId == videoId)
        {
        }
    }
}
