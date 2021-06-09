using ApplicationCore.Config;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Youtubeサービス
    /// </summary>
    public class YoutubeService : IYoutubeService, IOutsourcePlatFormVideoService
    {
        private readonly YoutubeConfig _youtubeConfig;
        const string YOUTUBE_DOMAIN = "https://www.youtube.com";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public YoutubeService(IOptions<YoutubeConfig> youtubeConfig)
        {
            _youtubeConfig = youtubeConfig.Value;
        }

        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        public async Task<IOutsourveVideoServiceRes> GetVideo(string youtubeVideoId)
        {
            var youtubeService = this.CreateYoutubeSearvice();

            //ネストされているプロパティを取得する場合はsnippet,そうでなければid
            var searchReq = youtubeService.Search.List("snippet");
            //Outsource動画IDで検索
            var searchId = youtubeVideoId;
            if (searchId.Substring(0, 1) == "-")
            {
                searchId = searchId.Substring(1, searchId.Length - 1);
            }
            searchReq.Q = searchId;

            try
            {
                var searchRes = await searchReq.ExecuteAsync();

                if (searchRes.Items == null || searchRes.Items.Count == 0)
                    return null;

                var result = searchRes.Items[0];

                return new OutsourceVideoServiceRes()
                {
                    VideoId = youtubeVideoId,
                    VideoTitle = result.Snippet.Title,
                    VideoLink = this.CreateVideoLink(youtubeVideoId),
                    ChannelId = result.Snippet.ChannelId,
                    ChannelTitle = result.Snippet.ChannelTitle,
                    Description = result.Snippet.Description,
                    ThumbnailLink = result.Snippet.Thumbnails.High.Url,
                    PublishDateTime = result.Snippet.PublishedAt.Value
                };
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        /// <summary>
        /// 動画の統計情報取得
        /// </summary>
        /// <param name="outsourceVideoId"></param>
        /// <param name="youtubeVideoId"></param>
        /// <returns></returns>
        public async Task<IOutsourceVideoStatisticsServiceRes> GetVideoStatistics(string youtubeVideoId)
        {
            var youtubeService = this.CreateYoutubeSearvice();

            //統計情報を取得
            var searchVideoReq = youtubeService.Videos.List("contentDetails, statistics");
            //動画IDで検索
            searchVideoReq.Id = youtubeVideoId;

            var videoResult = await searchVideoReq.ExecuteAsync();

            if (videoResult.Items == null || videoResult.Items.Count == 0)
                return null;

            //動画じゃなければnull
            var targetVideoDetail = videoResult.Items[0];
            if (targetVideoDetail.Kind != "youtube#video")
                return null;

            return new OutsourceVideoStatisticsServiceRes()
            {
                CommentCount = targetVideoDetail.Statistics.CommentCount.Value,
                LikeCount = targetVideoDetail.Statistics.LikeCount.Value,
                ViewCount = targetVideoDetail.Statistics.ViewCount.Value,
            };
        }

        /// <summary>
        /// YoutubeのUrlからIDを取得
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public string GetVideoId(Uri uri)
        {
            try
            {
                var id = HttpUtility.ParseQueryString(uri.Query).Get("v");

                return id;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// OutsourceServiceの生成
        /// </summary>
        /// <returns></returns>
        private YouTubeService CreateYoutubeSearvice()
        {
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _youtubeConfig.ApiKey
            });
        }

        /// <summary>
        /// VideoIDから動画リンクを生成
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        private string CreateVideoLink(string youtubeVideoId)
        {
            return YOUTUBE_DOMAIN + "/watch?v=" + youtubeVideoId;
        }
    }
}
