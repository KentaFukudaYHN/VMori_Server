using ApplicationCore.Enum;
using ApplicationCore.ServiceReqRes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IVideoService
    {
        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="includeStastics"></param>
        /// <returns></returns>
        Task<OutsourceVideoServiceRes> Get(string videoId);

        /// <summary>
        /// 動画情報をリストで取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <returns></returns>
        Task<OutsourceVideoGetListRes> GetList(int page, int displayNum);

        /// <summary>
        /// 動画情報をリストで取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        Task<OutsourceVideoGetListRes> GetList(SearchCriteriaVideoServiceReq req);

        /// <summary>
        /// 各ジャンルごとの動画情報取得
        /// </summary>
        /// <param name="req"></param>
        /// <param name="genres"></param>
        /// <returns></returns>
        Task<OutsourceGetListByGenresRes> GetListByGenres(SearchCriteriaVideoServiceReq req, List<VideoGenreKinds> genres);

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<GetOutsourceVideoServiceRes> GetVideoByLink(string url);

        /// <summary>
        /// チャンネルに紐づく動画を取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<OutsourceVideoSummaryServiceRes>> GetListByChannelId(string channelId, int page, int take);

        /// <summary>
        /// チャンネル情報の取得
        /// </summary>
        /// <param name="channelTableId"></param>
        /// <returns></returns>
        Task<OutsourceVideoChannelServiceRes> GetChannel(string channelTableId);


        /// <summary>
        /// チャンネル推移情報の取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        Task<List<ChannelTrantisionServiceRes>> GetChannelTransitions(string channelId);

        /// <summary>
        /// 動画情報の登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RegistOutsourceVideoServiceRes> RegistVideo(RegistOutsourceVideoServiceReq req);

        /// <summary>
        /// 再生回数のカウントアップ
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        Task<bool> CountUpViewCount(string videoId, string ipAddress);

        /// <summary>
        /// タグの更新
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<bool> UpdateTags(string id, List<string> tags);

        /// <summary>
        /// 話している言語の更新
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="speakJp"></param>
        /// <param name="speakEnglish"></param>
        /// <param name="speakOther"></param>
        /// <returns></returns>
        Task<bool> UpdateLangs(string videoId, bool speakJp, bool speakEnglish, bool speakOther);

        /// <summary>
        /// 翻訳している言語の更新
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="translationJP"></param>
        /// <param name="translationglish"></param>
        /// <param name="translationOther"></param>
        /// <returns></returns>
        Task<bool> UpdateTranslationLangs(string videoId, bool translationJP, bool translationglish, bool translationOther);
    }
}
