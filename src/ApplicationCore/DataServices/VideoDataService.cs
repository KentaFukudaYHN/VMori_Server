
using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// Outsource動画DataService
    /// </summary>
    public class VideoDataService : IVideoDataService
    {
        private readonly IAsyncRepository<Video> _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public VideoDataService(IAsyncRepository<Video> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="includeStastics"></param>
        /// <returns></returns>
        public async Task<Video> Get(string videoId, bool includeStastics)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("videoIdが空になっています");

            return (await _repository.ListAsync(new OutsourceVideoSpecifications(videoId, includeStastics))).FirstOrDefault();
        }

        /// <summary>
        /// 動画を取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <returns></returns>
        public async Task<List<Video>> GetList(int page, int displayNum)
        {
            if (page == 0 || displayNum == 0)
                throw new ArgumentException("pageとdisplayNumは0で指定できません");

            var spec = new OutsourceVideoListSpecifications(page, displayNum);
            spec.ApplyOrderBy(x => x.RegistDateTime);

            var result = await _repository.ListAsync(spec);
            if (result == null)
                return null;

            return result.ToList();
        }

        /// <summary>
        /// 動画を取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <param name="text"></param>
        /// <param name="genre"></param>
        /// <param name="langs"></param>
        /// <param name="isTranslatioon"></param>
        /// <param name="translationLangs"></param>
        /// <returns></returns>
        public async Task<List<Video>> GetList(int page, int displayNum,
            string text, VideoGenreKinds? genre,List<VideoLanguageKinds>? langs, bool? isTranslatioon,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<Video, object>> sortExpression, bool isDesc, DateTime? start, DateTime? end, bool? isPublishdate)
        {
            if (page == 0 || displayNum == 0)
                throw new ArgumentException("pageとdisplayNumは0で指定できません");

            try
            {
                var spec = (new OutsourceVideoListSpecifications(page, displayNum,
                            text, genre, langs, isTranslatioon, translationLangs, sortExpression, isDesc));

                if(start !=  null && end != null)
                {
                    if(isPublishdate != null && isPublishdate.Value)
                    {
                        spec.AddCriteriaByPublishDateTime(start.Value, end.Value);
                    }
                    else
                    {
                        spec.AddCriteriaByRegistDateTime(start.Value, end.Value);
                    }
                }

                var result = await _repository.ListAsync(spec);

                return result.ToList();
            }catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 動画を取得 ※複数ジャンル
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <param name="text"></param>
        /// <param name="genre"></param>
        /// <param name="langs"></param>
        /// <param name="isTranslatioon"></param>
        /// <param name="translationLangs"></param>
        /// <returns></returns>
        public async Task<List<Video>> GetList(int page, int displayNum,
            string text, List<VideoGenreKinds> genres, List<VideoLanguageKinds>? langs, bool? isTranslatioon,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<Video, object>> sortExpression, bool isDesc, DateTime? start, DateTime? end, bool? isPublishdate)
        {
            if (page == 0 || displayNum == 0)
                throw new ArgumentException("pageとdisplayNumは0で指定できません");

            try
            {
                var spec = new OutsourceVideoListSpecifications(page, displayNum,
                            text, genres, langs, isTranslatioon, translationLangs, sortExpression, isDesc);

                if (start != null && end != null)
                {
                    if (isPublishdate != null && isPublishdate.Value)
                    {
                        spec.AddCriteriaByPublishDateTime(start.Value, end.Value);
                    }
                    else
                    {
                        spec.AddCriteriaByRegistDateTime(start.Value, end.Value);
                    }
                }
                var result = await _repository.ListAsync(spec);

                return result.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// レコード数をカウント
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCount()
        {
            return await _repository.CountAsync();
        }
        /// <summary>
        /// レコード数をカウント
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCount(string text, VideoGenreKinds? genre, List<VideoLanguageKinds>? langs, bool? isTranslatioon,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<Video, object>> sortExpression, bool isDesc, DateTime? start, DateTime? end, bool? isPublishdate)
        {
            var spec = new OutsourceVideoListSpecifications(text, genre, langs, isTranslatioon, translationLangs, sortExpression, isDesc);

            if (start != null && end != null)
            {
                if (isPublishdate != null && isPublishdate.Value)
                {
                    spec.AddCriteriaByPublishDateTime(start.Value, end.Value);
                }
                else
                {
                    spec.AddCriteriaByRegistDateTime(start.Value, end.Value);
                }
            }

            return await _repository.CountAsync(spec);
        }

        /// <summary>
        /// レコード数をカウント ※複数ジャンル
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCount(string text, List<VideoGenreKinds> genres, List<VideoLanguageKinds>? langs, bool? isTranslatioon,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<Video, object>> sortExpression, bool isDesc, DateTime? start, DateTime? end, bool? isPublishdate)
        {
            var spec = new OutsourceVideoListSpecifications(text, genres, langs, isTranslatioon, translationLangs, sortExpression, isDesc);

            if (start != null && end != null)
            {
                if (isPublishdate != null && isPublishdate.Value)
                {
                    spec.AddCriteriaByPublishDateTime(start.Value, end.Value);
                }
                else
                {
                    spec.AddCriteriaByRegistDateTime(start.Value, end.Value);
                }
            }
            
            return await _repository.CountAsync(spec);
        }

        /// <summary>
        /// チャンネルIDで動画を取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<List<Video>> GetListByChannelId(string channelId, int page, int take)
        {
            if (string.IsNullOrEmpty(channelId))
                throw new ArgumentException("channelIDが空です");

            //検索条件
            var spec = new OutsourceVideoListSpecifications();
            spec.AddCriteriaByChannelId(channelId);
            spec.ApplyPaging(page, take);
            spec.ApplyOrderByDesc(x => x.PublishDateTime);

            var result = await _repository.ListAsync(spec);

            if (result == null)
                return null;

            return result.ToList();
        }

        /// <summary>
        /// 並び替えた動画リストを取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <param name="orderBy"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        public async Task<List<Video>> GetListOrderBy(int page, int take, Expression<Func<Video, object>> orderBy, bool isDesc)
        {
            if (page == 0 || take == 0 || orderBy == null)
                throw new ArgumentException("パラメーターが不正です");

            //検索条件
            var spec = new OutsourceVideoListSpecifications();
            spec.ApplyPaging(page, take);

            if (isDesc)
                spec.ApplyOrderByDesc(orderBy);
            else
                spec.ApplyOrderBy(orderBy);

            var result = await _repository.ListAsync(spec);

            return result != null ? result.ToList() : null;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="video"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<bool> Regist(Video video, IDbContext db)
        {
            if (string.IsNullOrEmpty(video.ID))
                throw new ArgumentException("IDが設定されていません");

            await _repository.AddAsync(video, db);

            return true;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="videos"></param>
        /// <returns></returns>
        public async Task<bool> UpdateList(List<Video> videos)
        {
            if (videos == null)
                throw new ArgumentException("パラメーターが不正です");

            await _repository.UpdateListAsync(videos);

            return true;
        }

        /// <summary>
        /// 再生回数をカウントアップ
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<bool> CountUpViewCount(string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("パラメーターが不正です");

            //対象の動画情報を取得
            var target = await this.GetByVideoID(videoId);

            if (target == null)
                throw new ArgumentException("対象の動画がありません");

            target.VMoriViewCount++;

            await this._repository.UpdateAsyncOnlyClumn(target, new List<string>() { nameof(target.VMoriViewCount) });

            return true;
        }

        /// <summary>
        /// ビデオIDで動画を検索
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<Video> GetByVideoID( string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("IDが設定されていません");

            return (await _repository.ListAsync(new OutsourceVideoWithVideoIdSpecification(videoId))).FirstOrDefault();
        }

        /// <summary>
        /// タグの更新
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTagById(string videoId, List<string> tags)
        {
            if (string.IsNullOrEmpty(videoId) || tags == null)
                throw new ArgumentException("不正なパラメーターです");

            var video = new Video() { ID = videoId, Tags = tags };
            await _repository.UpdateAsyncOnlyClumn(video, new List<string>() { nameof(Video.TagsData) });

            return true;
        }

        /// <summary>
        /// 言語の更新
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="speakJp"></param>
        /// <param name="speakEnglish"></param>
        /// <param name="speakOther"></param>
        /// <returns></returns>
        public async Task<bool> UpdateLangsById(string videoId, bool speakJp, bool speakEnglish, bool speakOther)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("パラメーターが不正です");

            var video = new Video() { ID = videoId, SpeakJP = speakJp, SpeakEnglish = speakEnglish, SpeakOther = speakOther };
            await _repository.UpdateAsyncOnlyClumn(video, new List<string>() { nameof(Video.SpeakJP), nameof(Video.SpeakEnglish), nameof(Video.SpeakOther) });

            return true;
        }

        /// <summary>
        /// 翻訳している言語の更新
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="speakJp"></param>
        /// <param name="speakEnglish"></param>
        /// <param name="speakOther"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTranslationLangsById(string videoId, bool speakJp, bool speakEnglish, bool speakOther)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("パラメーターが不正です");

            var video = new Video() { ID = videoId, TranslationJP = speakJp, TranslationEnglish = speakEnglish, TranslationOther = speakOther };
            await _repository.UpdateAsyncOnlyClumn(video, new List<string>() { nameof(Video.TranslationJP), nameof(Video.TranslationEnglish), nameof(Video.TranslationOther) });

            return true;
        }

        /// <summary>
        /// 有効無効の更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="available"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAvailableById(string id, bool available)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("パラメーターが不正です");

            var video = new Video() { ID = id, Available = available };
            await _repository.UpdateAsyncOnlyClumn(video, new List<string>() { nameof(Video.Available) });

            return true;
        }

        /// <summary>
        /// 有効無効の更新
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="available"></param>
        /// <returns></returns>
        public bool UpdateAvailableById(List<string> ids, bool available)
        {
            if (ids == null || ids.Count == 0)
                throw new ArgumentException("パラメーターが不正です");

            var idStr = string.Empty;
            ids.ForEach(x =>
            {
                idStr += "'" + x + "'" + ",";
            });

            idStr = idStr.TrimEnd(',');

            var availableVal = available ? 1 : 0;

            var sql = "UPDATE video set available = " + availableVal +  " WHERE ID IN (" + idStr + ")";

            try
            {
                _repository.ExecuteSqlRaw(sql);
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

    }
}
