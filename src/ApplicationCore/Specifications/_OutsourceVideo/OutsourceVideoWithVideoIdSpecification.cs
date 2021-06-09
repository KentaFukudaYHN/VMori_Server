using ApplicationCore.Entities;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// VideoIDでOutsource動画検索
    /// </summary>
    public class OutsourceVideoWithVideoIdSpecification : BaseSpecification<OutsourceVideo>
    {
        /// <summary>
        /// VideoIDで検索
        /// </summary>
        /// <param name="videoId"></param>
        public OutsourceVideoWithVideoIdSpecification(string videoId)
            :base(x => x.VideoId == videoId) { }
    }
}
