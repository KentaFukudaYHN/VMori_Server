using ApplicationCore.Entities;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// VideoIDでYoutube動画検索
    /// </summary>
    public class YoutubeVideoWithVideoIdSpecification : BaseSpecification<YoutubeVideo>
    {
        /// <summary>
        /// VideoIDで検索
        /// </summary>
        /// <param name="videoId"></param>
        public YoutubeVideoWithVideoIdSpecification(string videoId)
            :base(x => x.VideoId == videoId) { }
    }
}
