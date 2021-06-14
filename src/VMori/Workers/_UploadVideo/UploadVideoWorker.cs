using ApplicationCore.Enum;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using System;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ReqRes;

namespace VMori.Workers
{
    public class UploadVideoWorker : IUploadVideoWorker
    {
        private readonly IOutsourceVideoService _OutsourceVideoService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="OutsourceVideoService"></param>
        public UploadVideoWorker(IOutsourceVideoService OutsourceVideoService)
        {
            _OutsourceVideoService = OutsourceVideoService;
        }

        /// <summary>
        /// 動画の登録
        /// </summary>
        public async Task<RegistVideoRes> RegistVideo(RegistVideoReq req)
        {
            var serviceReq = new RegistOutsourceVideoServiceReq()
            {
                upReqVideoId = req.UpReqVideoId,
                Tags = req.Tags,
                Genre = req.Genre,
                Langes = req.Langes,
                IsTranslation = req.IsTranslation,
                LangForTranslation = req.LangForTranslation,
            };

            var serviceRes = await _OutsourceVideoService.RegistVideo(serviceReq);

            return new RegistVideoRes()
            {
                Success = serviceRes.Success,
                ErrKinds = serviceRes.ErrKinds
            };
        }

        /// <summary>
        /// アップロード予定の動画情報を取得
        /// </summary>
        /// <param name="req"></param>
        public  async Task<GetUploadVideoInfoRes> GetUploadVideoInfo(string url)
        {
            var serviceRes = await _OutsourceVideoService.GetVideoByLink(url);

            if (serviceRes.Success)
            {
                return new GetUploadVideoInfoRes()
                {
                    VideoTitle = serviceRes.VideoTitle,
                    ChanelTitle = serviceRes.ChanelTitle,
                    VideoLink = serviceRes.VideoLink,
                    ThumbnailLink = serviceRes.ThumbnailLink,
                    Description = serviceRes.Description,
                    UpReqOutsourceVideoToken = serviceRes.UpReqOutsourceVideoToken,
                    PublishDate = serviceRes.PublishDate,
                    PlatFormKinds = serviceRes.PlatFormKinds,
                    ErrKinds = serviceRes.ErrKinds,
                    Success = true,
                };
            }
            else
            {
                return new GetUploadVideoInfoRes()
                {
                    Success = false,
                    ErrKinds = serviceRes.ErrKinds
                };
            }
        }
    }
}
