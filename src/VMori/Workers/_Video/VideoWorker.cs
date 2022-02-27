using ApplicationCore.Enum;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ReqRes;

namespace VMori.Workers._Video
{
    /// <summary>
    /// 動画情報Worker
    /// </summary>
    public class VideoWorker : IVideoWorker
    {
        private IVideoService _outsourceVideoService;
        private IVideoCommentService _videoCommentService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outsourceVideoService"></param>
        public VideoWorker(IVideoService outsourceVideoService, IVideoCommentService videoCommentService)
        {
            _outsourceVideoService = outsourceVideoService;
            _videoCommentService = videoCommentService;
        }

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<VideoRes> Get(string videoId)
        {
            var result = await _outsourceVideoService.Get(videoId);

            if(result == null)
                return null;

            return new VideoRes(result);
        }

        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<VideoSummaryInfoRes> GetList(GetVideoSummaryReq req)
        {
            var result = await _outsourceVideoService.GetList(req.Page, req.DisplayNum);

            if (result.Items == null)
            {
                return new VideoSummaryInfoRes()
                {
                    Items = new List<VideoSummaryItem>(),
                    TotalCount = 0
                };
            }

            return new VideoSummaryInfoRes()
            {
                Items = result.Items.ConvertAll(x =>
                {
                    return new VideoSummaryItem(x);
                }),
                TotalCount = result.TotalCount
            };
        }

        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<VideoSummaryInfoRes> GetList(SearchCriteriaVideoReq req)
        {
            var serviceReq = CreateSearchCriteriaVideoServiceReq(req);


            var res = await _outsourceVideoService.GetList(serviceReq);

            return CreateVideoSummayIngoRes(res);
        }

        /// <summary>
        /// 各ジャンルの動画情報を取得
        /// </summary>
        /// <param name="req"></param>
        /// <param name="genres"></param>
        /// <returns></returns>
        public async Task<VideoSummaryInfoByGenreRes> GetListByGenre(SearchCriteriaVideoReq req, List<VideoGenreKinds> genres)
        {
            var serviceSearchReq = CreateSearchCriteriaVideoServiceReq(req);

            List<OutsourceVideoSummaryServiceRes> allVideos = null;
            if (genres.Contains(VideoGenreKinds.All))
            {
                allVideos = (await this._outsourceVideoService.GetList(serviceSearchReq)).Items;
                genres.Remove(VideoGenreKinds.All);
            }


            var res = await this._outsourceVideoService.GetListByGenres(serviceSearchReq, genres);

            var result = new VideoSummaryInfoByGenreRes();
            result.Items = new List<VideoSummaryByGenreRes>();
            genres.ForEach(x =>
            {
                var targetItems = res.Items[x];
                var genreItems = targetItems.ConvertAll(x => new VideoSummaryItem(x));
                result.Items.Add(new VideoSummaryByGenreRes()
                {
                    GenreKinds = x,
                    Items = genreItems
                });
            });

            //全ての動画が含まれていれば追加
            if (allVideos != null)
            {
                var allVideoRes = new VideoSummaryByGenreRes()
                {
                    GenreKinds = VideoGenreKinds.All,
                    Items = allVideos.ConvertAll(x => new VideoSummaryItem(x)),
                };
                result.Items.Add(allVideoRes);
            }


            //動画数が多い順に並び替え
            result.Items.Sort((a, b) => b.Items.Count - a.Items.Count);

            return result;
        }

        /// <summary>
        /// SearchCriteriaVideoServiceReqの生成
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private SearchCriteriaVideoServiceReq CreateSearchCriteriaVideoServiceReq(SearchCriteriaVideoReq req)
        {
            var detail = new SearchDetailCriteriaVideoReq()
            {
                IsTranslation = req.IsTranslation,
                Langs = req.Langs,
                TransrationLangs = req.TransrationLangs
            };

            var period = CreatePeriodDateTime(req.PeiodKinds);

            var serviceReq = new SearchCriteriaVideoServiceReq()
            {
                Page = req.Page,
                DisplayNum = req.DisplayNum,
                Text = req.Text,
                Genre = req.Genre == VideoGenreKinds.All ? null : req.Genre,
                Detail = detail,
                SortKinds = req.SortKinds,
                IsDesc = req.IsDesc,
                Start = period.Item1,
                End = period.Item2,
                IsPublish = req.IsPublish
            };

            return serviceReq;
        }


        /// <summary>
        /// チャンネルに紐づく動画のリストを取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<List<VideoSummaryItem>> GetChannelVideos(string channelId, int page, int take)
        {
            var result = await _outsourceVideoService.GetListByChannelId(channelId, page, take);

            if (result == null)
                return null;

            return result.ConvertAll(x => 
            {
                var item = new VideoSummaryItem();
                item.Id = x.VideoId;
                item.Title = x.VideoTitle;
                item.ViewCount = x.ViewCount;
                item.ThumbnailLink = x.ThumbnailLink;
                item.PublishDateTime = x.PublishDateTime;
                item.RegistDateTime = x.RegistDateTime;
                item.PlatFormKinds = x.PlatFormKinds;

                return item;
            });
        }

        /// <summary>
        /// チャンネル情報取得
        /// </summary>
        /// <param name="channelTableId"></param>
        /// <returns></returns>
        public async Task<ChannelRes> GetChannel(string channelTableId)
        {
            var result = await _outsourceVideoService.GetChannel(channelTableId);
            if (result == null)
                return null;

            return new ChannelRes(result);
        }

        /// <summary>
        /// チャンネル推移情報の取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<List<ChannelTransitionRes>> GetChannelTransitions(string channelId)
        {
            var result = await _outsourceVideoService.GetChannelTransitions(channelId);
            if (result == null)
                return null;

            return result.ConvertAll(x => new ChannelTransitionRes(x));
        }

        /// <summary>
        /// 動画コメントの取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<List<VideoCommentRes>> GetComments(string videoId)
        {
            var result = await _videoCommentService.GetListByVideoId(videoId);
            if (result == null)
                return null;
            return result.ConvertAll(x => new VideoCommentRes(x));
        }

        /// <summary>
        /// 動画コメント登録
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="text"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task<bool> RegistComment(string videoId, string text, int time)
        {
            return await _videoCommentService.Regist(videoId, text, time);
        }

        /// <summary>
        /// カウントアップ
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> CountUpViewCount(string videoId, HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();
            return await _outsourceVideoService.CountUpViewCount(videoId, ipAddress);
        }

        /// <summary>
        /// タグの更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTags(string id, List<string> tags)
        {
            return await _outsourceVideoService.UpdateTags(id, tags);
        }

        /// <summary>
        /// 話している言語の更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<bool> UpdateLangs(UpdateVideoLangsReq req)
        {
            return await _outsourceVideoService.UpdateLangs(req.Id, req.SpeakJP, req.SpeakEnglish, req.SpeakOther);
        }

        /// <summary>
        /// 翻訳している言語の更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTranslationLangs(UpdateVideoTranslationLangReq req)
        {
            return await _outsourceVideoService.UpdateTranslationLangs(req.Id, req.TranslationJP, req.TranslationEnglish, req.TranslationOther);
        }

        /// <summary>
        /// OutsourceVideoServiceResの生成
        /// </summary>
        /// <param name="resVideoList"></param>
        /// <returns></returns>
        private VideoSummaryInfoRes CreateVideoSummayIngoRes(OutsourceVideoGetListRes res)
        {
            if (res.Items == null)
            {
                return new VideoSummaryInfoRes()
                {
                    Items = new List<VideoSummaryItem>(),
                    TotalCount = 1,
                };
            }

            return new VideoSummaryInfoRes()
            {
                Items = res.Items.ConvertAll(x =>
                {
                    return new VideoSummaryItem(x);
                }),
                TotalCount = res.TotalCount
            };
        }

        /// <summary>
        /// 期間の生成
        /// </summary>
        /// <param name="kinds"></param>
        /// <returns>(開始,終了)</returns>
        private (DateTime?, DateTime?) CreatePeriodDateTime(PeriodKinds kinds)
        {
            var now = DateTime.Now;
            DateTime? start = null;
            DateTime? end = null;
            switch (kinds)
            {
                case PeriodKinds.ToDay:
                    start = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0);
                    end = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, 999);
                    break;
                case PeriodKinds.Week:
                    DateTime tmp = DateTime.Now;
                    tmp.AddDays(DayOfWeek.Monday - tmp.DayOfWeek);

                    start = new DateTime(tmp.Year, tmp.Month, tmp.Day, 0, 0, 0, 0);
                    end = now;
                    break;
                case PeriodKinds.Month:
                    start = new DateTime(now.Year, now.Month, 1, 0, 0, 0, 0);
                    end = now;
                    break;
                case PeriodKinds.All:
                    start = null;
                    end = null;
                    break;
            }

            return (start, end);
        }
    }
}
