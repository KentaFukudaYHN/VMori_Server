﻿
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
    public class OutsourceVideoDataService : IOutsourceVideoDataService
    {
        private readonly IAsyncRepository<OutsourceVideo> _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public OutsourceVideoDataService(IAsyncRepository<OutsourceVideo> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 動画情報を取得
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="includeStastics"></param>
        /// <returns></returns>
        public async Task<OutsourceVideo> Get(string videoId, bool includeStastics)
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
        public async Task<List<OutsourceVideo>> GetList(int page, int displayNum)
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
        public async Task<List<OutsourceVideo>> GetList(int page, int displayNum,
            string text, VideoGenreKinds? genre,List<VideoLanguageKinds>? langs, bool? isTranslatioon,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<OutsourceVideo, object>> sortExpression, bool isDesc)
        {
            if (page == 0 || displayNum == 0)
                throw new ArgumentException("pageとdisplayNumは0で指定できません");

            try
            {
                var result = await _repository.ListAsync(new OutsourceVideoListSpecifications(page, displayNum,
                            text, genre, langs, isTranslatioon, translationLangs, sortExpression, isDesc));

                return result.ToList();
            }catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// チャンネルIDで動画を取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<List<OutsourceVideo>> GetListByChannelId(string channelId, int page, int take)
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
        public async Task<List<OutsourceVideo>> GetListOrderBy(int page, int take, Expression<Func<OutsourceVideo, object>> orderBy, bool isDesc)
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
        public async Task<bool> Regist(OutsourceVideo video, IDbContext db)
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
        public async Task<bool> UpdateList(List<OutsourceVideo> videos)
        {
            if (videos == null)
                throw new ArgumentException("パラメーターが不正です");

            await _repository.UpdateListAsync(videos);

            return true;
        }

        /// <summary>
        /// ビデオIDで動画を検索
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<OutsourceVideo> GetByVideoID( string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentException("IDが設定されていません");

            return (await _repository.ListAsync(new OutsourceVideoWithVideoIdSpecification(videoId))).FirstOrDefault();
        }

    }
}
