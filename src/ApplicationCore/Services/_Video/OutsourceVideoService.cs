
using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationCore.Services
{
    /// <summary>
    /// ユーチューブサービス
    /// </summary>
    public class OutsourceVideoService : IOutsourceVideoService
    {
        private readonly IOutsourceVideoDataService _videoDataService;
        private readonly IOutsourceVideoStatisticsDataService _stisticsDataService;
        private readonly IUpReqOutsourceVideoDataService _upReqOutsourceVieoDataService;
        private readonly IYoutubeService _youtubeService;
        private readonly INikoNikoService _nikonikoService;
        private readonly IDbContext _db;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="OutsourceConfig"></param>
        public OutsourceVideoService(IOutsourceVideoDataService videoDataService, IOutsourceVideoStatisticsDataService statisticsDataService, IUpReqOutsourceVideoDataService upReqDataService, IYoutubeService youtubeService, INikoNikoService nikonikoService, IDbContext db)
        {
            _videoDataService = videoDataService;
            _stisticsDataService = statisticsDataService;
            _upReqOutsourceVieoDataService = upReqDataService;
            _youtubeService = youtubeService;
            _nikonikoService = nikonikoService;
            _db = db;
        }

        /// <summary>
        /// 動画の統計情報取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        private async Task<OutsourceVideoStatistics> CreateVideoStatistics(string vmOutsourceVideoId, string videoId, VideoPlatFormKinds kinds)
        {
            IOutsourceVideoStatisticsServiceRes res;
            switch (kinds)
            {
                case VideoPlatFormKinds.Youtube:
                    res = await _youtubeService.GetVideoStatistics(videoId);
                    break;
                default:
                    throw new ArgumentException("想定されてない動画プラットフォームです");
            }

            return new OutsourceVideoStatistics()
            {
                ID = Guid.NewGuid().ToString(),
                CommentCount = res.CommentCount,
                LikeCount = res.LikeCount,
                ViewCount = res.ViewCount,
                VideoId = videoId,
                OutsourceVideoId = vmOutsourceVideoId,
                GetDateTime = DateTime.Now,
            };
        }


        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<GetOutsourceVideoServiceRes> GetVideo(string url)
        {
            Uri uri;
            try
            {
                uri = new Uri(url);
            }
            catch (Exception e)
            {
                return new GetOutsourceVideoServiceRes()
                {
                    Success = false,
                    ErrKinds = RegistVideoErrKinds.UrlFormat
                };
            }

            //動画のプラットフォーム判定
            var platformKinds = this.GetPlatformKinds(uri);

            if(platformKinds == VideoPlatFormKinds.UnKnown)
            {
                return new GetOutsourceVideoServiceRes()
                {
                    Success = false,
                    ErrKinds = RegistVideoErrKinds.UnSupportPlatform
                };
            }

            //動画プラットフォームのサービスを取得
            var outsourtcePlatFormVideoService = this.GetOutsourcePlatFormVideoService(platformKinds);

            //動画IDの取得
            var videoId = outsourtcePlatFormVideoService.GetVideoId(uri);

            if (string.IsNullOrEmpty(videoId))
            {
                return new GetOutsourceVideoServiceRes()
                {
                    Success = false,
                    ErrKinds = RegistVideoErrKinds.NotIdByYoutube,
                };
            }

            //既に登録されている動画でないか確認
            var isExitsVideo = await _videoDataService.GetByVideoID(videoId);
            if (isExitsVideo != null)
            {
                return new GetOutsourceVideoServiceRes()
                {
                    Success = false,
                    ErrKinds = RegistVideoErrKinds.IsExits
                };
            }

            //動画情報の取得
            var res = await outsourtcePlatFormVideoService.GetVideo(videoId);

            //動画情報をアップロードリクエストとしてDBに保存
            var upReqData = new UpReqOutsourceVideo()
            {
                ID = Guid.NewGuid().ToString(),
                PlatFormKinds = platformKinds,
                VideoId = videoId,
                ChanelId = res.ChannelId,
                ChanelTitle = res.ChannelTitle,
                Description = res.Description,
                VideoTitle = res.VideoTitle,
                ThumbnailLink = res.ThumbnailLink,
                PublishDateTime = res.PublishDateTime
            };

            await _upReqOutsourceVieoDataService.Regist(upReqData);

            return new GetOutsourceVideoServiceRes()
            {
                Success = true,
                ThumbnailLink = res.ThumbnailLink,
                PlatFormKinds = platformKinds,
                VideoTitle = res.VideoTitle,
                ChanelTitle = res.ChannelTitle,
                Description = res.Description,
                VideoLink = res.VideoLink,
                PublishDate = res.PublishDateTime,
                UpReqOutsourceVideoToken = upReqData.ID,
            };
        }

        /// <summary>
        /// 動画情報の登録
        /// </summary>
        /// <returns></returns>
        public async Task<RegistOutsourceVideoServiceRes> RegistVideo(RegistOutsourceVideoServiceReq req)
        {
            //リクエストデータのチェック
            if (string.IsNullOrEmpty(req.upReqVideoId))
                throw new ArgumentException("リクエストIDが空です");

            if (req.Genre == Enum.VideoGenreKinds.UnKnown)
                throw new ArgumentException("ジャンルの設定は必須です");

            if (req.Langes == null || req.Langes.Length == 0)
                throw new ArgumentException("言語の選択は必須です");

            if (req.IsTranslation && (req.LangForTranslation == null || req.LangForTranslation.Length == 0))
                throw new ArgumentException("『翻訳あり』の場合、翻訳言語の設定は必須です");

            //動画アップロードリクエスト情報を検索
            var upReqVideo = await _upReqOutsourceVieoDataService.GetById(req.upReqVideoId);

            if (upReqVideo == null)
                throw new ArgumentException("IDが間違っています");

            //既にアップロードされていないかチェック
            var exitVideo = await _videoDataService.GetByVideoID(upReqVideo.VideoId);
            if (exitVideo != null)
            {
                return new RegistOutsourceVideoServiceRes()
                {
                    Success = false,
                    ErrKinds = Enum.RegistVideoErrKinds.IsExits
                };
            }

            //動画の統計情報を取得
            var OutsourceVideoId = Guid.NewGuid().ToString();
            var outsourceVideoService = this.GetOutsourcePlatFormVideoService(upReqVideo.PlatFormKinds);
            var res = await outsourceVideoService.GetVideoStatistics(upReqVideo.VideoId);

            var stistics = new OutsourceVideoStatistics()
            {
                ID = Guid.NewGuid().ToString(),
                CommentCount = res.CommentCount,
                LikeCount = res.LikeCount,
                ViewCount = res.ViewCount,
                VideoId = upReqVideo.VideoId,
                OutsourceVideoId = OutsourceVideoId,
                GetDateTime = DateTime.Now,
            };

            //動画アップロードリクエスト情報を動画情報に保存
            var video = new OutsourceVideo()
            {
                ID = OutsourceVideoId,
                VideoId = upReqVideo.VideoId,
                ChanelId = upReqVideo.ChanelId,
                ChanelTitle = upReqVideo.ChanelTitle,
                Description = upReqVideo.Description,
                VideoTitle = upReqVideo.VideoTitle,
                ThumbnailLink = upReqVideo.ThumbnailLink,
                PublishDateTime = upReqVideo.PublishDateTime,
                PlatFormKinds = upReqVideo.PlatFormKinds,
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
                    return new RegistOutsourceVideoServiceRes()
                    {
                        Success = false,
                        ErrKinds = Enum.RegistVideoErrKinds.None
                    };
                }
            }

            return new RegistOutsourceVideoServiceRes()
            {
                Success = true
            };
        }

        /// <summary>
        /// 該当するサービスを返す
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private IOutsourcePlatFormVideoService GetOutsourcePlatFormVideoService(VideoPlatFormKinds kinds)
        {
            switch (kinds)
            {
                case VideoPlatFormKinds.Youtube:
                    return _youtubeService as IOutsourcePlatFormVideoService;
                case VideoPlatFormKinds.NikoNiko:
                    return _nikonikoService as IOutsourcePlatFormVideoService;
                default:
                    throw new ArgumentException("想定されてないPlatFormkindsです" + kinds);
            }

        }

        /// <summary>
        /// Urlから動画プラットフォームを判定
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private VideoPlatFormKinds GetPlatformKinds(Uri uri)
        {
            var host = uri.Host;

            //wwwがついていた場合削除
            var subDomain = host.Split(".")[0];
            if (subDomain == "www")
            {
                host = host.Replace("www.", "");
            }

            host = host.ToLower();
            switch (host)
            {
                case "youtube.com":
                    return VideoPlatFormKinds.Youtube;
                case "nicovideo.jp":
                    return VideoPlatFormKinds.NikoNiko;
                default:
                    return VideoPlatFormKinds.UnKnown;
            }

        }

    }
}
