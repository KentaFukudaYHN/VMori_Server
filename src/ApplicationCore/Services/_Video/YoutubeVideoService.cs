
using ApplicationCore.Config;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    /// <summary>
    /// ユーチューブサービス
    /// </summary>
    public class YoutubeVideoService : IYoutubeVideoService
    {
        private readonly YoutubeConfig _youtubeConfig;
        const string YOUTBE_DOMAIN = "https://www.youtube.com";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="youtubeConfig"></param>
        public YoutubeVideoService(IOptions<YoutubeConfig> youtubeConfig)
        {
            _youtubeConfig = youtubeConfig.Value;
        }

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<GetYoutubeVideoServiceRes> GetVideo(string videoId)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _youtubeConfig.ApiKey
            });
            //ネストされているプロパティを取得する場合はsnippet,そうでなければid
            var searchVideoReq = youtubeService.Videos.List("contentDetails, statistics");
            searchVideoReq.Id = videoId;

            var videoResult = searchVideoReq.Execute();

            if (videoResult.Items == null || videoResult.Items.Count == 0)
                return null;

            var targetVideoDetail = videoResult.Items[0];
            if (targetVideoDetail.Kind != "youtube#video")
                return null;


            var searchReq = youtubeService.Search.List("snippet");
            //youtube動画IDで検索
            searchReq.Q = videoId;

            var searchRes = await searchReq.ExecuteAsync();

            if (searchRes.Items == null || searchRes.Items.Count == 0)
                return null;

            var searchResult = searchRes.Items[0];

            return new GetYoutubeVideoServiceRes()
            {
                ThumbnailLink = searchResult.Snippet.Thumbnails.High.Url,
                VideoTitle = searchResult.Snippet.Title,
                ChanelTitle = searchResult.Snippet.ChannelTitle,
                Description = searchResult.Snippet.Description,
                VideoLink = YOUTBE_DOMAIN + "/watch?v=" + searchResult.Id.VideoId,
                PublishDate = searchResult.Snippet.PublishedAt.Value,
                ViewCount = targetVideoDetail.Statistics.ViewCount.Value,
                LikeCount = targetVideoDetail.Statistics.LikeCount.Value
            };
        }
    }
}
