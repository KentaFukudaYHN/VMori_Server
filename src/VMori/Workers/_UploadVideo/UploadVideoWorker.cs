using ApplicationCore.Enum;
using ApplicationCore.Interfaces;
using ApplicationCore.ServiceReqRes;
using System;
using System.Threading.Tasks;
using System.Web;
using VMori.Interfaces;
using VMori.ReqRes;

namespace VMori.Workers
{
    public class UploadVideoWorker : IUploadVideoWorker
    {
        private readonly IYoutubeVideoService _youtubeVideoService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="youtubeVideoService"></param>
        public UploadVideoWorker(IYoutubeVideoService youtubeVideoService)
        {
            _youtubeVideoService = youtubeVideoService;
        }

        /// <summary>
        /// 動画の登録
        /// </summary>
        public async Task<RegistVideoRes> RegistVideo(RegistVideoReq req)
        {
            switch (req.PlatFormKinds)
            {
                case VideoPlatFormKinds.Youtube:
                    return await this.RegistYoutubeVideo(req);
                default:
                    throw new ArgumentException("不正なパラメータです");
            }
        }

        /// <summary>
        /// Youtube動画の登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private async Task<RegistVideoRes> RegistYoutubeVideo(RegistVideoReq req)
        {
            var serviceReq = new RegistYoutubeVideoServiceReq()
            {
                upReqVideoId = req.UpReqVideoId,
                Tags = req.Tags,
                Genre = req.Genre,
                Langes = req.Langes,
                IsTranslation = req.IsTranslation,
                LangForTranslation = req.LangForTranslation,
            };

            var serviceRes = await _youtubeVideoService.RegistVideo(serviceReq);

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
            Uri uri;
            try
            {
                uri = new Uri(url);
            }catch(Exception e)
            {
                return new GetUploadVideoInfoRes()
                {
                    Success = false,
                    ErrMsg = "読み込めない形式のUrlです",
                    ErrKinds = RegistVideoErrKinds.UrlFormat
                };
            }

            //動画のプラットフォームを判定
            var platformKinds = this.GetPlatformKinds(uri);

            switch (platformKinds)
            {
                case VideoPlatFormKinds.Youtube:
                    return await this.CreateUploadYoutubeVideoRes(uri);
                default:
                    return new GetUploadVideoInfoRes()
                    {
                        Success = false,
                        ErrMsg = "対応してないサイトのURLです",
                        ErrKinds = RegistVideoErrKinds.UnSupportPlatform
                    };
            }
        }

        /// <summary>
        /// Youtube動画の取得
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private async Task<GetUploadVideoInfoRes> CreateUploadYoutubeVideoRes(Uri uri)
        {

            //動画IDの取得
            var videoId = this.GetYoutubeVideoId(uri);

            if (string.IsNullOrEmpty(videoId))
            {
                return new GetUploadVideoInfoRes()
                {
                    Success = false,
                    ErrMsg = "Youtubeの動画IDを抽出することができませんでした",
                    ErrKinds = RegistVideoErrKinds.NotIdByYoutube
                };
            }

            //Youtube動画情報を取得
            var youtubeVideoInfo = await _youtubeVideoService.GetVideo(videoId);

            if(youtubeVideoInfo.Success == false && youtubeVideoInfo.ErrKinds == RegistVideoErrKinds.IsExits)
            {
                return new GetUploadVideoInfoRes()
                {
                    Success = false,
                    ErrMsg = "こちらは既に登録されているのでアップロードすることはできません",
                    ErrKinds = RegistVideoErrKinds.IsExits
                };
            }
            else if(youtubeVideoInfo.Success == false)
            {
                return new GetUploadVideoInfoRes()
                {
                    Success = false,
                    ErrMsg = "原因不明のエラーです",
                    ErrKinds = RegistVideoErrKinds.None
                };
            }

            return new GetUploadVideoInfoRes()
            {
                VideoTitle = youtubeVideoInfo.VideoTitle,
                ChanelTitle = youtubeVideoInfo.ChanelTitle,
                VideoLink = youtubeVideoInfo.VideoLink,
                ThumbnailLink = youtubeVideoInfo.ThumbnailLink,
                Description = youtubeVideoInfo.Description,
                UpReqYoutubeVideoToken = youtubeVideoInfo.UpReqYoutubeVideoToken,
                PublishDate = youtubeVideoInfo.PublishDate,
                PlatFormKinds = VideoPlatFormKinds.Youtube,
                ErrKinds = RegistVideoErrKinds.None,
                Success = true,
                ErrMsg = string.Empty
            };
        }

        /// <summary>
        /// Youtube動画のURLからVideoIDを抽出
        /// </summary>
        /// <param name="youtubeVideoUrl"></param>
        /// <returns></returns>
        private string GetYoutubeVideoId(Uri youtubeVideoUri)
        {
            try
            {
                var videoId = HttpUtility.ParseQueryString(youtubeVideoUri.Query).Get("v");

                if (string.IsNullOrEmpty(videoId))
                    return string.Empty;

                return videoId;

            }catch(Exception e)
            {
                return string.Empty;
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
            if(subDomain == "www")
            {
                host = host.Replace("www.", "");
            }

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
