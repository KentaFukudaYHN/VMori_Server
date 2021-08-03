using ApplicationCore.Entities;
using ApplicationCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Outsource動画Dataservice
    /// </summary>
    public interface IVideoDataService
    {

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="includeStastics"></param>
        /// <returns></returns>
        Task<Video> Get(string videoId, bool includeStastics);
        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <returns></returns>
        Task<List<Video>> GetList(int page, int displayNum);

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <param name="text"></param>
        /// <param name="genre"></param>
        /// <param name="langs"></param>
        /// <param name="isTranslatioon"></param>
        /// <param name="translationLangs"></param>
        /// <returns></returns>
        Task<List<Video>> GetList(int page, int displayNum,
            string text, VideoGenreKinds? genre, List<VideoLanguageKinds>? langs, bool? isTranslatioon,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<Video, object>> sortExpression, bool isDesc, DateTime? start, DateTime? end, bool? isPublishdate);

        /// <summary>
        /// 複数ジャンル
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <param name="text"></param>
        /// <param name="genres"></param>
        /// <param name="langs"></param>
        /// <param name="isTranslatioon"></param>
        /// <param name="translationLangs"></param>
        /// <param name="sortExpression"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        Task<List<Video>> GetList(int page, int displayNum,
            string text, List<VideoGenreKinds> genres, List<VideoLanguageKinds>? langs, bool? isTranslatioon,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<Video, object>> sortExpression, bool isDesc, DateTime? start, DateTime? end, bool? isPublishdate);

        /// <summary>
        /// チャンネルIDで動画のリスト取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<Video>> GetListByChannelId(string channelId, int page, int take);

        /// <summary>
        /// 動画情報を登録
        /// </summary>
        /// <param name="video"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public Task<bool> Regist(Video video, IDbContext db);

        /// <summary>
        /// 動画IDで検索
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        Task<Video> GetByVideoID(string videoId);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="videos"></param>
        /// <returns></returns>
        Task<bool> UpdateList(List<Video> videos);

        /// <summary>
        /// 再生回数のカウントアップ
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        Task<bool> CountUpViewCount(string videoId);

        /// <summary>
        /// レコード数をカウント
        /// </summary>
        /// <returns></returns>
        Task<int> GetCount();
        /// <summary>
        /// レコード数をカウント
        /// </summary>
        /// <param name="text"></param>
        /// <param name="genre"></param>
        /// <param name="langs"></param>
        /// <param name="isTranslatioon"></param>
        /// <param name="translationLangs"></param>
        /// <param name="sortExpression"></param>
        /// <param name="isDesc"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="isPublishdate"></param>
        /// <returns></returns>
        Task<int> GetCount(string text, VideoGenreKinds? genre, List<VideoLanguageKinds>? langs, bool? isTranslatioon,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<Video, object>> sortExpression, bool isDesc, DateTime? start, DateTime? end, bool? isPublishdate);

        /// <summary>
        /// レコード数をカウント ※複数ジャンル
        /// </summary>
        /// <param name="text"></param>
        /// <param name="genres"></param>
        /// <param name="langs"></param>
        /// <param name="isTranslatioon"></param>
        /// <param name="translationLangs"></param>
        /// <param name="sortExpression"></param>
        /// <param name="isDesc"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="isPublishdate"></param>
        /// <returns></returns>
        Task<int> GetCount(string text, List<VideoGenreKinds> genres, List<VideoLanguageKinds>? langs, bool? isTranslatioon,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<Video, object>> sortExpression, bool isDesc, DateTime? start, DateTime? end, bool? isPublishdate);

        /// <summary>
        /// タグの更新
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<bool> UpdateTagById(string videoId, List<string> tags);

        /// <summary>
        /// 有効無効の更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="available"></param>
        /// <returns></returns>
        Task<bool> UpdateAvailableById(string id, bool available);

        /// <summary>
        /// 有効無効の更新
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="available"></param>
        /// <returns></returns>
        bool UpdateAvailableById(List<string> ids, bool available);
    }
}
