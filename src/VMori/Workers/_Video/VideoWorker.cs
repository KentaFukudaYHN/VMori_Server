using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outsourceVideoService"></param>
        public VideoWorker(IOutsourceVideoService outsourceVideoService)
        {
            _outsourceVideoService = outsourceVideoService;
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
                Genre = req.Genre,
                Detail = detail
            };

            var result = await _outsourceVideoService.GetList(serviceReq);

            return CreateVideoSummayIngoRes(result);
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
