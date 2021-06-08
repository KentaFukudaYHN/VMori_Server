
using ApplicationCore.Config;
using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    /// <summary>
    /// ユーチューブサービス
    /// </summary>
    public class YoutubeVideoService : IYoutubeVideoService
    {
        private readonly IYoutubeVideoDataService _videoDataService;
        private readonly IYoutubeVideoStatisticsDataService _stisticsDataService;
        private readonly IUpReqYoutubeVideoDataService _upReqYoutubeVieoDataService;
        private readonly IDbContext _db;
        private readonly YoutubeConfig _youtubeConfig;
        const string YOUTUBE_DOMAIN = "https://www.youtube.com";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="youtubeConfig"></param>
        public YoutubeVideoService(IYoutubeVideoDataService videoDataService, IYoutubeVideoStatisticsDataService statisticsDataService, IUpReqYoutubeVideoDataService upReqDataService, IDbContext db, IOptions<YoutubeConfig> youtubeConfig)
        {
            _videoDataService = videoDataService;
            _stisticsDataService = statisticsDataService;
            _upReqYoutubeVieoDataService = upReqDataService;
            _db = db;
            _youtubeConfig = youtubeConfig.Value;
        }

        /// <summary>
        /// 動画の統計情報取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        private async Task<YoutubeVideoStatistics> CreateVideoStatistics(string vmYoutubeVideoId, string videoId)
        {
            var youtubeService = this.CreateYoutubeSearvice();

            //統計情報を取得
            var searchVideoReq = youtubeService.Videos.List("contentDetails, statistics");
            //動画IDで検索
            searchVideoReq.Id = videoId;

            var videoResult = await searchVideoReq.ExecuteAsync();

            if (videoResult.Items == null || videoResult.Items.Count == 0)
                return null;

            //動画じゃなければnull
            var targetVideoDetail = videoResult.Items[0];
            if (targetVideoDetail.Kind != "youtube#video")
                return null;

            return new YoutubeVideoStatistics()
            {
                ID = Guid.NewGuid().ToString(),
                CommentCount = targetVideoDetail.Statistics.CommentCount.Value,
                LikeCount = targetVideoDetail.Statistics.LikeCount.Value,
                ViewCount = targetVideoDetail.Statistics.ViewCount.Value,
                VideoId = videoId,
                YoutubeVideoId = vmYoutubeVideoId,
                GetDateTime = DateTime.Now,
            };
        }

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<GetYoutubeVideoServiceRes> GetVideo(string videoId)
        {
            //既に登録されている動画でないか確認
            var isExitsVideo = await _videoDataService.GetByVideoID(videoId);
            if(isExitsVideo != null)
            {
                return new GetYoutubeVideoServiceRes()
                {
                    Success = false,
                    ErrKinds = RegistVideoErrKinds.IsExits
                };
            }

            var youtubeService = this.CreateYoutubeSearvice();

            //ネストされているプロパティを取得する場合はsnippet,そうでなければid
            var searchReq = youtubeService.Search.List("snippet");
            //youtube動画IDで検索
            var searchId = videoId;
            if(searchId.Substring(0, 1) == "-")
            {
                searchId = searchId.Substring(1, searchId.Length - 1);
            }
            searchReq.Q = searchId;

            var searchRes = await searchReq.ExecuteAsync();

            if (searchRes.Items == null || searchRes.Items.Count == 0)
                return null;

            var searchResult = searchRes.Items[0];

            //動画情報をアップロードリクエストとしてDBに保存
            var upReqData = new UpReqYoutubeVideo()
            {
                ID = Guid.NewGuid().ToString(),
                VideoId = videoId,
                ChanelId = searchResult.Snippet.ChannelId,
                ChanelTitle = searchResult.Snippet.ChannelTitle,
                Description = searchResult.Snippet.Description,
                VideoTitle = searchResult.Snippet.Title,
                ThumbnailLink = searchResult.Snippet.Thumbnails.High.Url,
                PublishDateTime = searchResult.Snippet.PublishedAt.Value
            };

            await _upReqYoutubeVieoDataService.Regist(upReqData);

            return new GetYoutubeVideoServiceRes()
            {
                Success = true,
                ThumbnailLink = searchResult.Snippet.Thumbnails.High.Url,
                VideoTitle = searchResult.Snippet.Title,
                ChanelTitle = searchResult.Snippet.ChannelTitle,
                Description = searchResult.Snippet.Description,
                VideoLink = this.CreateVideoLink(videoId),
                PublishDate = searchResult.Snippet.PublishedAt.Value,
                UpReqYoutubeVideoToken = upReqData.ID,
            };
        }

        /// <summary>
        /// 動画情報の登録
        /// </summary>
        /// <returns></returns>
        public async Task<RegistYoutubeVideoServiceRes> RegistVideo(RegistYoutubeVideoServiceReq req)
        {
            //リクエストデータのチェック
            if (string.IsNullOrEmpty(req.upReqVideoId))
                throw new ArgumentException("リクエストIDが空です");

            if (req.Genre == Enum.VideoGenreKinds.UnKnown)
                throw new ArgumentException("ジャンルの設定は必須です");

            if (req.Langes == null || req.Langes.Length == 0)
                throw new ArgumentException("言語の選択は必須です");

            if (req.IsTranslation && req.LangForTranslation == null || req.LangForTranslation.Length == 0)
                throw new ArgumentException("『翻訳あり』の場合、翻訳言語の設定は必須です");

            //動画アップロードリクエスト情報を検索
            var upReqVideo = await _upReqYoutubeVieoDataService.GetById(req.upReqVideoId);

            if (upReqVideo == null)
                throw new ArgumentException("IDが間違っています");

            //既にアップロードされていないかチェック
            var exitVideo = await _videoDataService.GetByVideoID(upReqVideo.VideoId);
            if (exitVideo != null)
            {
                return new RegistYoutubeVideoServiceRes()
                {
                    Success = false,
                    ErrKinds = Enum.RegistVideoErrKinds.IsExits
                };
            }

            //動画の統計情報を取得
            var youtubeVideoId = Guid.NewGuid().ToString();
            var stistics = await this.CreateVideoStatistics(youtubeVideoId, upReqVideo.VideoId);

            //動画アップロードリクエスト情報を動画情報に保存
            var video = new YoutubeVideo()
            {
                ID = youtubeVideoId,
                VideoId = upReqVideo.VideoId,
                ChanelId = upReqVideo.ChanelId,
                ChanelTitle = upReqVideo.ChanelTitle,
                Description = upReqVideo.Description,
                VideoTitle = upReqVideo.VideoTitle,
                ThumbnailLink = upReqVideo.ThumbnailLink,
                PublishDateTime = upReqVideo.PublishDateTime,
                Genre = req.Genre,
                Tags = req.Tags.ToList(),
                Langes = req.Langes.ToList(),
                IsTranslation = req.IsTranslation,
                LangForTranslation = req.LangForTranslation.ToList(),
                RegistDateTime = DateTime.Now
            };

            using (var tx = _db.Database.BeginTransaction())
            {
                try
                {
                    //動画情報を登録
                    await _videoDataService.Regist(video, _db);

                    //動画統計情報を登録
                    await _stisticsDataService.Regist(stistics, _db);
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return new RegistYoutubeVideoServiceRes()
                    {
                        Success = false,
                        ErrKinds = Enum.RegistVideoErrKinds.None
                    };
                }
            }

            return new RegistYoutubeVideoServiceRes()
            {
                Success = true
            };
        }

        /// <summary>
        /// VideoIDから動画リンクを生成
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        private string CreateVideoLink(string videoId)
        {
            return YOUTUBE_DOMAIN + "/watch?v=" + videoId;
        }

        /// <summary>
        /// YoutubeServiceの生成
        /// </summary>
        /// <returns></returns>
        private YouTubeService CreateYoutubeSearvice()
        {
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _youtubeConfig.ApiKey
            });
        }
    }
}
