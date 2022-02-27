
using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    /// <summary>
    /// 動画サービス
    /// </summary>
    public class VideoService : IVideoService
    {
        private readonly IVideoDataService _videoDataService;
        private readonly IUpReqOutsourceVideoDataService _upReqOutsourceVieoDataService;
        private readonly IYoutubeService _youtubeService;
        private readonly IOutsouceVideoChannelDataService _channelDataService;
        private readonly IChannelTransitionDataService _channelTransitionDataService;
        private readonly IDbContext _db;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IVideoHistoryDataService _videoHistoryDataService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="OutsourceConfig"></param>
        public VideoService(IVideoDataService videoDataService,
            IUpReqOutsourceVideoDataService upReqDataService, IYoutubeService youtubeService, IOutsouceVideoChannelDataService channelDataService,  
            IChannelTransitionDataService channelTransitionDataService, IDbContext db, IServiceScopeFactory serviceScopeFactory, IVideoHistoryDataService videoHistoryDataService)
        {
            _videoDataService = videoDataService;
            _upReqOutsourceVieoDataService = upReqDataService;
            _youtubeService = youtubeService;
            _channelDataService = channelDataService;
            _channelTransitionDataService = channelTransitionDataService;
            _db = db;
            _serviceScopeFactory = serviceScopeFactory;
            _videoHistoryDataService = videoHistoryDataService;
        }

        /// <summary>
        /// チャンネルの推移データ取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<List<ChannelTrantisionServiceRes>> GetChannelTransitions(string channelId)
        {
            if (string.IsNullOrEmpty(channelId))
                throw new ArgumentException("パラメーターが不正です");

            var result =  await _channelTransitionDataService.GetListByChannelId(channelId);
            if (result == null)
                return null;

            return result.ConvertAll(x => new ChannelTrantisionServiceRes(x));
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

            return new OutsourceVideoServiceRes(result);
        }

        /// <summary>
        /// 動画情報の取得
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<OutsourceVideoGetListRes> GetList(int page, int displayNum)
        {
            try
            {
                if (page <= 0 || displayNum <= 0)
                    throw new ArgumentException("pageと表示数を0以下にすることはできません");

                var result = await _videoDataService.GetList(page, displayNum);

                //更新するべき動画があれば更新する
                var checkTask = CeckAndUpdateVideoList(result);

                if (result == null)
                    return null;

                return new OutsourceVideoGetListRes()
                {
                    Items = result.ConvertAll(x => CreateOutsourceVideoServiceRes(x)),
                    TotalCount = await _videoDataService.GetCount()
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<OutsourceVideoGetListRes> GetList(SearchCriteriaVideoServiceReq req)
        {
            if (req.Page <= 0 || req.DisplayNum <= 0)
                throw new ArgumentException("pageと表示数を0以下にすることはできません");

            //並び順の設定
            var sortFunc = this.GetSortFunc(req.SortKinds);

            var result = await _videoDataService.GetList(req.Page, req.DisplayNum, req.Text, req.Genre, req.Detail.Langs, req.Detail.IsTranslation, req.Detail.TransrationLangs, sortFunc, req.IsDesc, req.Start, req.End, req.IsPublish);

            if (result == null)
                return null;

            //更新するべき動画があれば更新する
            var checkTask = CeckAndUpdateVideoList(result);

            return new OutsourceVideoGetListRes()
            {
                Items = result.ConvertAll(x => CreateOutsourceVideoServiceRes(x)),
                TotalCount = await _videoDataService.GetCount(req.Text, req.Genre, req.Detail.Langs, req.Detail.IsTranslation, req.Detail.TransrationLangs, sortFunc, req.IsDesc, req.Start, req.End, req.IsPublish)
            };
        }

        /// <summary>
        /// 並び替えの関数を取得
        /// </summary>
        /// <param name="kinds"></param>
        /// <returns></returns>
        private Expression<Func<Video, object>> GetSortFunc(SortKinds kinds)
        {
            switch (kinds)
            {
                case SortKinds.RegistDateTime:
                    return x => x.RegistDateTime;
                case SortKinds.ViewCount:
                    return x => x.ViewCount;
                case SortKinds.CommentCount:
                    return x => x.CommentCount;
                case SortKinds.LikeCount:
                    return x => x.LikeCount;
                case SortKinds.VMoriViewCount:
                    return x => x.VMoriViewCount;
                case SortKinds.PublishDateTime:
                    return x => x.PublishDateTime;
                default:
                    throw new ArgumentException("不明なSortKindsです:" + kinds);
            }

        }

        /// <summary>
        /// 複数ジャンルごとの動画情報を取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<OutsourceGetListByGenresRes> GetListByGenres(SearchCriteriaVideoServiceReq req, List<VideoGenreKinds> genres)
        {
            if (req.Page <= 0 || req.DisplayNum <= 0)
                throw new ArgumentException("pageと表示数を0以下にすることはできません");


            //並び順の設定
            var sortFunc = this.GetSortFunc(req.SortKinds);

            var videos = await _videoDataService.GetList(req.Page, req.DisplayNum, req.Text, genres, req.Detail.Langs, req.Detail.IsTranslation, req.Detail.TransrationLangs, sortFunc, req.IsDesc, req.Start, req.End, req.IsPublish);

            if (videos == null)
                return null;

            var dic = new Dictionary<VideoGenreKinds, List<OutsourceVideoSummaryServiceRes>>();
            genres.ForEach(x =>
            {
                dic.Add(x, new List<OutsourceVideoSummaryServiceRes>());
            });

            videos.ForEach(x =>
            {
                dic[x.Genre].Add(CreateOutsourceVideoServiceRes(x));
            });

            //動画の総レコード数を取得
            var total = await _videoDataService.GetCount(req.Text, VideoGenreKinds.All, req.Detail.Langs, req.Detail.IsTranslation, req.Detail.TransrationLangs, sortFunc, req.IsDesc, req.Start, req.End, req.IsPublish);

            return new OutsourceGetListByGenresRes()
            {
                Items = dic
            };

        }

        /// <summary>
        /// 更新期間のきた動画を更新する
        /// </summary>
        /// <param name="videos"></param>
        /// <returns></returns>
        private async Task CeckAndUpdateVideoList(List<Video> videos)
        {
            //更新の必要の有無をチェック
            bool CheckUpdate(Video v, DateTime now)
            {
                //現在と前回更新日時の日数の差を計算
                var interval = (now - v.UpdateDateTime).TotalDays;

                //更新回数が13回を超えていたらもう更新はしない
                if (v.UpdateCount > 13)
                    return false;

                //1回目と2回目の更新は前回更新時から3日後 ※3日目、6日目
                if (v.UpdateCount < 2 && interval > 3)
                {
                    return true;
                }
                //3回目の更新は前回更新時から4日後 ※ 10日目
                else if (v.UpdateCount < 3 && interval > 4)
                {
                    return true;
                }
                //4回~5回目目の更新は前回更新から5日後 15日目 20日目
                else if (v.UpdateCount < 5 && interval > 5)
                {
                    return true;
                }
                //6回目の更新は前回更新から10日後 30日目
                else if(v.UpdateCount < 6 && interval > 10)
                {
                    return true;
                }
                //7回目~8回目は前回更新から20日後  50日目 70日目
                else if(v.UpdateCount < 8 && interval > 20)
                {
                    return true;
                }
                //9回目は前回更新から30日後 100日目
                else if(v.UpdateCount < 9 && interval > 30)
                {
                    return true;
                }
                //10~11回目は前回更新から50日後 150日目 200日目
                else if(v.UpdateCount < 11 && interval > 50)
                {
                    return true;
                }
                //12回目は前回更新から100日後
                else if(v.UpdateCount < 12 && interval > 100)
                {
                    return true;
                }
                //13回目は前回更新から65日後
                else if(v.UpdateCount < 13 && interval > 65)
                {
                    return true;
                }

                return false;
            }

            var updateTargetVideoIds = new List<string>();

            //更新の必要のある動画のVideoIdを抽出
            var now = DateTime.Now;
            videos.ForEach(x =>
            {
                if (CheckUpdate(x, now))
                    updateTargetVideoIds.Add(x.VideoId);
            });

            if (updateTargetVideoIds.Count == 0)
                return;

            //更新対象の最新情報をyoutubeAPIから取得
            var newDatas = await _youtubeService.GetVideos(updateTargetVideoIds);

            if(newDatas == null || newDatas.Count < updateTargetVideoIds.Count)
            {
                //動画が削除されている可能性があるので、Availableをfalseに更新する
                var targetIds = new List<string>();
                var newDataIds = newDatas.ConvertAll(x => x.VideoId);
                for (int i = 0; i < updateTargetVideoIds.Count; i++)
                {
                    if (newDataIds.Contains(updateTargetVideoIds[i]) == false)
                    {
                        targetIds.Add(videos.Find(x => x.VideoId == updateTargetVideoIds[i]).ID);
                        continue;
                    }
                }

                if(targetIds.Count > 0)
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var dataService = scope.ServiceProvider.GetService<IVideoDataService>();
                        dataService.UpdateAvailableById(targetIds, false);
                    }
                }
            }

            //更新データの生成
            var updateDatas = new List<Video>();
            newDatas.ForEach(newData =>
            {
                var target = videos.Find(oldData => newData.VideoId == oldData.VideoId);
                if(target != null)
                {
                    target.VideoId = newData.VideoTitle;
                    target.ChannelId = newData.ChannelTitle;
                    target.Description = newData.Description;
                    target.ThumbnailLink = newData.ThumbnailLink;
                    target.ViewCount = newData.ViewCount;
                    target.CommentCount = newData.CommentCount;
                    target.LikeCount = newData.LikeCount;
                    target.UpdateCount++;
                    target.UpdateDateTime = now;
                    updateDatas.Add(target);
                }
            });

            //更新 ※バックグラウンドで実行するとDbContextが閉じて更新できないので新たにサービスを生成する
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var newVideoDataService = scope.ServiceProvider.GetService<IVideoDataService>();
                await newVideoDataService.UpdateList(updateDatas);
            }
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
        private OutsourceVideoSummaryServiceRes CreateOutsourceVideoServiceRes(Video entity)
        {
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
                ViewCount = entity.ViewCount,
                VMoriViewCount = entity.VMoriViewCount,
                LikeCount = entity.LikeCount,
                CommentCount = entity.CommentCount
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
                ViewCount = res.ViewCount,
                ChanelTitle = res.ChannelTitle,
                Description = res.Description,
                VideoTitle = res.VideoTitle,
                ThumbnailLink = res.ThumbnailLink,
                PublishDateTime = res.PublishDateTime,
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

            if (req.Genre == Enum.VideoGenreKinds.All)
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

            var video = new Video()
            {
                ID = OutsourceVideoId,
                VideoId = upReqVideo.VideoId,
                ChanelId = upReqVideo.ChanelId,
                ChanelTitle = upReqVideo.ChanelTitle,
                Description = upReqVideo.Description,
                VideoTitle = upReqVideo.VideoTitle,
                ViewCount = upReqVideo.ViewCount,
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
                RegistDateTime = DateTime.Now,
                Available = true
            };

            //①既に登録されているチャンネルか確認
            //②無ければチャンネル情報を登録
            var channel = await _channelDataService.GetByChannelId(video.ChanelId);
            var registChannel = false;
            if(channel == null)
            {
                channel = await _youtubeService.GetChannel(video.ChanelId);
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

        /// <summary>
        /// 再生回数のカウントアップ
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CountUpViewCount(string videoId, string ipAddress)
        {
            if (string.IsNullOrEmpty(videoId) || string.IsNullOrEmpty(ipAddress))
                throw new ArgumentException("不正なパラメーターです");

            //1日以内に履歴があればカウントアップはしない
            var targetHistyory = await _videoHistoryDataService.Get(videoId, ipAddress);
            if(targetHistyory == null)
            {
                //履歴の登録
                var history = new VideoHistory()
                {
                    ID = Guid.NewGuid().ToString(),
                    VideoId = videoId,
                    IpAddress = ipAddress,
                    RegistDateTime = DateTime.Now
                };
                await _videoHistoryDataService.Regist(history);
                await this._videoDataService.CountUpViewCount(videoId);
            }
            else
            {
                var calcDate = DateTime.Now - targetHistyory.RegistDateTime;
                if(calcDate.TotalDays < 1)
                {
                    return false;
                }
                else
                {
                    targetHistyory.RegistDateTime = DateTime.Now;
                    await this._videoHistoryDataService.Update(targetHistyory);
                    await this._videoDataService.CountUpViewCount(videoId);
                }
            }


            return true;
        }

        /// <summary>
        /// タグの更新
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTags(string videoId, List<string> tags)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("パラメーターが不正です");

            return await _videoDataService.UpdateTagById(videoId, tags);
        }

        /// <summary>
        /// 話している言語の更新
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="speakJp"></param>
        /// <param name="speakEnglish"></param>
        /// <param name="speakOther"></param>
        /// <returns></returns>
        public async Task<bool> UpdateLangs(string videoId, bool speakJp, bool speakEnglish, bool speakOther)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("パラメーターが不正です");

            return await _videoDataService.UpdateLangsById(videoId, speakJp, speakEnglish, speakOther);
        }

        /// <summary>
        /// 翻訳している言語の更新
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="speakJp"></param>
        /// <param name="speakEnglish"></param>
        /// <param name="speakOther"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTranslationLangs(string videoId, bool translationJP, bool translationglish, bool translationOther)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("パラメーターが不正です");

            return await _videoDataService.UpdateTranslationLangsById(videoId, translationJP, translationglish, translationOther);
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

        public enum SortKinds
        {
            /// <summary>
            /// 登録日時順
            /// </summary>
            RegistDateTime = 0,
            /// <summary>
            /// 再生回数順
            /// </summary>
            ViewCount = 10,
            /// <summary>
            /// いいね数順
            /// </summary>
            LikeCount = 20,
            /// <summary>
            /// コメント順
            /// </summary>
            CommentCount = 30,
            /// <summary>
            /// VMori再生回数
            /// </summary>
            VMoriViewCount = 40,
            /// <summary>
            /// Youtube登録日時
            /// </summary>
            PublishDateTime = 50,
        }

    }
}
