
using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IOutsouceVideoChannelDataService _channelDataService;
        private readonly IDbContext _db;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="OutsourceConfig"></param>
        public OutsourceVideoService(IOutsourceVideoDataService videoDataService, IOutsourceVideoStatisticsDataService statisticsDataService,
            IUpReqOutsourceVideoDataService upReqDataService, IYoutubeService youtubeService, IOutsouceVideoChannelDataService channelDataService,  IDbContext db)
        {
            _videoDataService = videoDataService;
            _stisticsDataService = statisticsDataService;
            _upReqOutsourceVieoDataService = upReqDataService;
            _youtubeService = youtubeService;
            _channelDataService = channelDataService;
            _db = db;
        }

        /// <summary>
        /// チャンネル情報を取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<OutsourceVideoChannelServiceRes> GetChannel(string channelTableId)
        {
            if (string.IsNullOrEmpty(channelTableId))
                throw new ArgumentException("チャンネルIDが空です");

            var result = await _channelDataService.Get(channelTableId);

            if (result == null)
                return null;

            return new OutsourceVideoChannelServiceRes(result);
        }

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<OutsourceVideoServiceRes> Get(string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("VideoIdが空になっています");

            var result = await _videoDataService.Get(videoId, false);

            if (result == null)
                return null;

            var statistics = await _stisticsDataService.Get(result.ID, true);
            if(statistics == null)
                statistics = new OutsourceVideoStatistics();

            return new OutsourceVideoServiceRes(result, statistics);
        }

        /// <summary>
        /// 動画情報の取得
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<List<OutsourceVideoSummaryServiceRes>> GetList(int page, int displayNum)
        {
            if (page <= 0 || displayNum <= 0)
                throw new ArgumentException("pageと表示数を0以下にすることはできません");

            var result = await _videoDataService.GetList(page, displayNum);

            if (result == null)
                return null;

            return result.ConvertAll(x => CreateOutsourceVideoServiceRes(x));
        }

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<List<OutsourceVideoSummaryServiceRes>> GetList(SearchCriteriaVideoServiceReq req)
        {
            if (req.Page <= 0 || req.DisplayNum <= 0)
                throw new ArgumentException("pageと表示数を0以下にすることはできません");

            var result = await _videoDataService.GetList(req.Page, req.DisplayNum, req.Text, req.Genre, req.Detail.Langs, req.Detail.IsTranslation, req.Detail.TransrationLangs);

            if (result == null)
                return null;

            return result.ConvertAll(x => CreateOutsourceVideoServiceRes(x));
        }

        /// <summary>
        /// チャンネルIDで動画のリスト取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<List<OutsourceVideoSummaryServiceRes>> GetListByChannelId(string channelId, int page, int take)
        {
            if (page == 0 || take == 0)
                throw new ArgumentException("パラメーターが不正です");

            var result = await _videoDataService.GetListByChannelId(channelId, page, take);

            if (result == null)
                return null;

            return result.ConvertAll(x => CreateOutsourceVideoServiceRes(x));
        }

        /// <summary>
        /// OutsourceVideoServiceResの生成
        /// </summary>
        /// <returns></returns>
        private OutsourceVideoSummaryServiceRes CreateOutsourceVideoServiceRes(OutsourceVideo entity)
        {
            ulong viewCount = 0;
            var latestStatics = entity.Statistics.OrderByDescending(entity => entity.GetDateTime).FirstOrDefault();
            if (latestStatics != null)
                viewCount = latestStatics.ViewCount;

            return new OutsourceVideoSummaryServiceRes()
            {
                VideoId = entity.VideoId,
                VideoTitle = entity.VideoTitle,
                VideoLink = _youtubeService.CreateVideoLink(entity.VideoId),
                ThumbnailLink = entity.ThumbnailLink,
                ChannelId = entity.ChanelId,
                ChannelTitle = entity.ChanelTitle,
                Description = entity.Description,
                PlatFormKinds = entity.PlatFormKinds,
                PublishDateTime = entity.PublishDateTime,
                RegistDateTime = entity.RegistDateTime,
                ViewCount = viewCount,
            };
        }

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<GetOutsourceVideoServiceRes> GetVideoByLink(string url)
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

            //動画IDの取得
            var videoId = _youtubeService.GetVideoId(uri);

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
            var res = await _youtubeService.GetVideo(videoId);

            if(res == null)
            {
                RegistVideoErrKinds notFoundErrKinds = RegistVideoErrKinds.NotFound;
                switch (platformKinds)
                {
                    case VideoPlatFormKinds.Youtube:
                        notFoundErrKinds = RegistVideoErrKinds.NotFoundByYoutube;
                        break;
                    case VideoPlatFormKinds.NikoNiko:
                        notFoundErrKinds = RegistVideoErrKinds.NotFoundByNikoNiko;
                        break;
                }

                return new GetOutsourceVideoServiceRes()
                {
                    Success = false,
                    ErrKinds = notFoundErrKinds
                };
            }

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

            if (req.Langes == null || req.Langes.Count == 0)
                throw new ArgumentException("言語の選択は必須です");

            if (req.IsTranslation && (req.LangForTranslation == null || req.LangForTranslation.Count == 0))
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
            var res = await _youtubeService.GetVideoStatistics(upReqVideo.VideoId);

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
            var speakJp = false;
            var speakEnglish = false;
            var speakOther = false;
            if(req.Langes != null)
            {
                req.Langes.ForEach(x =>
                {
                    switch (x)
                    {
                        case VideoLanguageKinds.JP:
                            speakJp = true;
                            break;
                        case VideoLanguageKinds.English:
                            speakEnglish = true;
                            break;
                        case VideoLanguageKinds.Other:
                            speakOther = true;
                            break;
                    }
                });
            }
            var transitionJp = false;
            var transitionEnglish = false;
            var transtionOther = false;
            if (req.LangForTranslation != null)
            {
                req.LangForTranslation.ForEach(x =>
                {
                    switch (x)
                    {
                        case VideoLanguageKinds.JP:
                            transitionJp = true;
                            break;
                        case VideoLanguageKinds.English:
                            transitionEnglish = true;
                            break;
                        case VideoLanguageKinds.Other:
                            transtionOther = true;
                            break;
                    }
                });
            }

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
                SpeakJP = speakJp,
                SpeakEnglish = speakEnglish,
                SpeakOther = speakOther,
                IsTranslation = req.IsTranslation,
                TranslationJP = transitionJp,
                TranslationEnglish = transitionEnglish,
                TranslationOther = transtionOther,
                RegistDateTime = DateTime.Now
            };

            //①既に登録されているチャンネルか確認
            //②無ければチャンネル情報を登録
            var channel = await _channelDataService.GetByChannelId(video.ChanelId);
            var registChannel = false;
            if(channel == null)
            {
                channel = await _youtubeService.GetChannel(video.ChanelId);
                channel.ID = Guid.NewGuid().ToString();
                registChannel = true;
            }

            if (channel == null)
                video.ChanelId = "";
            else
                video.ChanelId = channel.ID;

            using (var tx = _db.Database.BeginTransaction())
            {
                try
                {
                    //動画情報を登録
                    await _videoDataService.Regist(video, _db);

                    //動画統計情報を登録
                    await _stisticsDataService.Regist(stistics, _db);

                    //チャンネル情報を登録
                    if(registChannel)
                        await _channelDataService.Regist(channel, _db);

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

        ///// <summary>
        ///// 該当するサービスを返す
        ///// </summary>
        ///// <param name="uri"></param>
        ///// <returns></returns>
        //private IOutsourcePlatFormVideoService GetOutsourcePlatFormVideoService(VideoPlatFormKinds kinds)
        //{
        //    switch (kinds)
        //    {
        //        case VideoPlatFormKinds.Youtube:
        //            return _youtubeService as IOutsourcePlatFormVideoService;
        //        case VideoPlatFormKinds.NikoNiko:
        //            return _nikonikoService as IOutsourcePlatFormVideoService;
        //        default:
        //            throw new ArgumentException("想定されてないPlatFormkindsです" + kinds);
        //    }

        //}

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
                //case "nicovideo.jp":
                //    return VideoPlatFormKinds.NikoNiko;
                default:
                    return VideoPlatFormKinds.UnKnown;
            }

        }

    }
}
