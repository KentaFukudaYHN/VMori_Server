using ApplicationCore.Enum;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
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
        private IOutsourceVideoService _outsourceVideoService;
        private IVideoCommentService _videoCommentService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outsourceVideoService"></param>
        public VideoWorker(IOutsourceVideoService outsourceVideoService, IVideoCommentService videoCommentService)
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

            if (result == null)
            {
                return new VideoSummaryInfoRes()
                {
                    Items = new List<VideoSummaryItem>()
                };
            }

            return new VideoSummaryInfoRes()
            {
                Items = result.ConvertAll(x =>
                {
                    return new VideoSummaryItem(x);
                })
            };
        }

        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<VideoSummaryInfoRes> GetList(SearchCriteriaVideoReq req)
        {
            var result = await GetVideoSummaryServiceRes(req);

            return CreateVideoSummayIngoRes(result);
        }

        /// <summary>
        /// 各ジャンルの動画情報を取得
        /// </summary>
        /// <param name="req"></param>
        /// <param name="genres"></param>
        /// <returns></returns>
        public async Task<VideoSummaryInfoByGenreRes> GetListByGenre(SearchCriteriaVideoReq req, List<VideoGenreKinds> genres)
        {
            //各ジャンルの動画情報を取得
            var resDic = new Dictionary<VideoGenreKinds,List<OutsourceVideoSummaryServiceRes>>();
            var tasks = new List<Task>();
            for (int i = 0; i < genres.Count; i++)
            {
                req.Genre = genres[i];
                resDic.Add(req.Genre.Value, await GetVideoSummaryServiceRes(req));
            }


            var result = new VideoSummaryInfoByGenreRes();
            result.Items = new List<VideoSummaryByGenreRes>();
            genres.ForEach(x =>
            {
                var targetItems = resDic[x];
                var genreItems = targetItems.ConvertAll(x => new VideoSummaryItem(x));
                result.Items.Add(new VideoSummaryByGenreRes()
                {
                    GenreKinds = x,
                    Items = genreItems
                });
            });

            return result;
        }

        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private async Task<List<OutsourceVideoSummaryServiceRes>> GetVideoSummaryServiceRes(SearchCriteriaVideoReq req)
        {
            var detail = new SearchDetailCriteriaVideoReq()
            {
                IsTranslation = req.IsTranslation,
                Langs = req.Langs,
                TransrationLangs = req.TransrationLangs
            };

            var serviceReq = new SearchCriteriaVideoServiceReq()
            {
                Page = req.Page,
                DisplayNum = req.DisplayNum,
                Text = req.Text,
                Genre = req.Genre == VideoGenreKinds.All ? null : req.Genre,
                Detail = detail,
                SortKinds = req.SortKinds
            };

            return await _outsourceVideoService.GetList(serviceReq);
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
        /// OutsourceVideoServiceResの生成
        /// </summary>
        /// <param name="resVideoList"></param>
        /// <returns></returns>
        private VideoSummaryInfoRes CreateVideoSummayIngoRes(List<OutsourceVideoSummaryServiceRes> resVideoList)
        {
            if (resVideoList == null)
            {
                return new VideoSummaryInfoRes()
                {
                    Items = new List<VideoSummaryItem>()
                };
            }

            return new VideoSummaryInfoRes()
            {
                Items = resVideoList.ConvertAll(x =>
                {
                    return new VideoSummaryItem(x);
                })
            };
        }
    }
}
