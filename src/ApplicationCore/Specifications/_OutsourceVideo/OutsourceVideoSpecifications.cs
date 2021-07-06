using ApplicationCore.Entities;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// 動画情報検索条件
    /// </summary>
    public class OutsourceVideoSpecifications : BaseSpecification<OutsourceVideo>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="videoId"></param>
        public OutsourceVideoSpecifications(string videoId, bool includeStatistics) : base(x => x.VideoId == videoId)
        {
        }
    }
}
