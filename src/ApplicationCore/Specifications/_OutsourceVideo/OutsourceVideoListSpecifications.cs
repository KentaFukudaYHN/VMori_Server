using ApplicationCore.Entities;
using ApplicationCore.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// 動画リスト取得条件
    /// </summary>
    public class OutsourceVideoListSpecifications : BaseSpecification<OutsourceVideo>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OutsourceVideoListSpecifications(): base(null)
        {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="page"></param>
        /// <param name=""></param>
        /// <param name=""></param>
        public OutsourceVideoListSpecifications(int page, int displayNum): base(null)
        {
            ApplyPaging(page, displayNum);

            //登録日時順
            ApplyOrderByDescending(x => x.RegistDateTime);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <param name="text"></param>
        public OutsourceVideoListSpecifications( int page, int displayNum,
            string text, VideoGenreKinds? genre,
            List<VideoLanguageKinds>? langs, bool? isTranslation,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<OutsourceVideo, object>> sortExpression, bool isDesc) : this(page, displayNum)
        {
            ApplyPaging(page, displayNum);

            //タイトルのフルテキスト検索条件追加
            settingFullText(text);

            //言語の設定
            settingLangs(langs);

            //翻訳の有無設定
            settingIsTranslation(isTranslation);

            //翻訳言語の設定
            settingTransrationLangs(translationLangs);

            //並び替えの設定
            settingOrder(isDesc, sortExpression);

            //ジャンルの設定
            if (genre != null)
            {
                base.AddCriteria(x => x.Genre == genre);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <param name="text"></param>
        public OutsourceVideoListSpecifications(int page, int displayNum,
        string text, List<VideoGenreKinds> genres,
        List<VideoLanguageKinds>? langs, bool? isTranslation,
        List<VideoLanguageKinds>? translationLangs, Expression<Func<OutsourceVideo, object>> sortExpression, bool isDesc) : this(page, displayNum)
        {
            ApplyPaging(page, displayNum);

            //タイトルのフルテキスト検索条件追加
            settingFullText(text);

            //言語の設定
            settingLangs(langs);

            //翻訳の有無設定
            settingIsTranslation(isTranslation);

            //翻訳言語の設定
            settingTransrationLangs(translationLangs);

            //並び替えの設定
            settingOrder(isDesc, sortExpression);

            //ジャンルの設定 
            if (genres != null && genres.Count > 0)
            {
                base.AddCriteria(x => genres.Contains(x.Genre));
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="text"></param>
        /// <param name="genre"></param>
        /// <param name="langs"></param>
        /// <param name="isTranslation"></param>
        /// <param name="translationLangs"></param>
        /// <param name="sortExpression"></param>
        /// <param name="isDesc"></param>
        public OutsourceVideoListSpecifications(string text, VideoGenreKinds? genre,List<VideoLanguageKinds>? langs, bool? isTranslation,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<OutsourceVideo, object>> sortExpression, bool isDesc) : base(null)
        {
            //タイトルのフルテキスト検索条件追加
            settingFullText(text);

            //言語の設定
            settingLangs(langs);

            //翻訳の有無設定
            settingIsTranslation(isTranslation);

            //翻訳言語の設定
            settingTransrationLangs(translationLangs);

            //並び替えの設定
            settingOrder(isDesc, sortExpression);

            //ジャンルの設定
            if (genre != null)
            {
                base.AddCriteria(x => x.Genre == genre);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="text"></param>
        /// <param name="genres"></param>
        /// <param name="langs"></param>
        /// <param name="isTranslation"></param>
        /// <param name="translationLangs"></param>
        /// <param name="sortExpression"></param>
        /// <param name="isDesc"></param>
        public OutsourceVideoListSpecifications(string text, List<VideoGenreKinds> genres, List<VideoLanguageKinds>? langs, bool? isTranslation,
            List<VideoLanguageKinds>? translationLangs, Expression<Func<OutsourceVideo, object>> sortExpression, bool isDesc): base (null)
        {
            //タイトルのフルテキスト検索条件追加
            settingFullText(text);

            //言語の設定
            settingLangs(langs);

            //翻訳の有無設定
            settingIsTranslation(isTranslation);

            //翻訳言語の設定
            settingTransrationLangs(translationLangs);

            //並び替えの設定
            settingOrder(isDesc, sortExpression);

            //ジャンルの設定 
            if (genres != null && genres.Count > 0)
            {
                base.AddCriteria(x => genres.Contains(x.Genre));
            }
        }


        /// <summary>
        /// チャンネルIDで検索条件追加
        /// </summary>
        /// <param name="channelId"></param>
        public void AddCriteriaByChannelId(string channelId)
        {
            base.AddCriteria(x => x.ChanelId == channelId);
        }

        /// <summary>
        /// 検索条件に期間を追加
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AddCriteriaByRegistDateTime(DateTime start, DateTime end)
        {
            base.AddCriteria(x => x.RegistDateTime >= start && x.RegistDateTime <= end);
        }

        /// <summary>
        /// 検索条件に期間を追加
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AddCriteriaByPublishDateTime(DateTime start, DateTime end)
        {
            base.AddCriteria(x => x.PublishDateTime >= start && x.RegistDateTime <= end);
        }
        /// <summary>
        /// ページングの設定
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        public void ApplyPaging(int page, int take)
        {
            var skip = CalcSkip(page, take);

            base.ApplyPaging(skip, take);
        }
        
        /// <summary>
        /// 降順で並び替え
        /// </summary>
        public void ApplyOrderByDesc(Expression<Func<OutsourceVideo, object>> expression)
        {
            base.ApplyOrderByDescending(expression);
        }

        /// <summary>
        /// 昇順で並び替え
        /// </summary>
        /// <param name="expression"></param>
        public new void ApplyOrderBy(Expression<Func<OutsourceVideo, object>> expression)
        {
            base.ApplyOrderBy(expression);
        }

        /// <summary>
        /// ページングのスキップ数を計算
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <returns></returns>
        private int CalcSkip(int page, int displayNum)
        {
            var skip = 0;
            if (page > 1)
                skip = (page - 1) * displayNum;

            return skip;
        }

        /// <summary>
        /// フルテキスト検索の設定
        /// </summary>
        /// <param name="text"></param>
        private void settingFullText(string text)
        {
            if (string.IsNullOrEmpty(text) == false)
            {
                base.AddFullTextCriteria(x => EF.Functions.FreeText(x.VideoTitle, text) || EF.Functions.FreeText(x.Description, text) || EF.Functions.FreeText(x.TagsData, text));
            }
        }

        /// <summary>
        /// 言語の設定
        /// </summary>
        /// <param name="langs"></param>
        private void settingLangs(List<VideoLanguageKinds> langs)
        {
            if (langs != null)
            {
                langs.ForEach(x =>
                {
                    switch (x)
                    {
                        case VideoLanguageKinds.JP:
                            base.AddCriteria(x => x.SpeakJP == true);
                            break;
                        case VideoLanguageKinds.English:

                            base.AddCriteria(x => x.SpeakEnglish == true);
                            break;
                        case VideoLanguageKinds.Other:
                            base.AddCriteria(x => x.SpeakOther == true);
                            break;
                    }
                });
            }
        }

        /// <summary>
        /// 翻訳の有無設定
        /// </summary>
        /// <param name="isTranslation"></param>
        private void settingIsTranslation(bool? isTranslation)
        {
            if (isTranslation != null)
            {
                base.AddCriteria(x => x.IsTranslation == isTranslation);
            }
        }

        /// <summary>
        /// 翻訳言語の設定
        /// </summary>
        /// <param name="translationLangs"></param>
        private void settingTransrationLangs(List<VideoLanguageKinds>? translationLangs)
        {
            if (translationLangs != null)
            {
                translationLangs.ForEach(x =>
                {
                    switch (x)
                    {
                        case VideoLanguageKinds.JP:
                            base.AddCriteria(x => x.TranslationJP == true);
                            break;
                        case VideoLanguageKinds.English:
                            base.AddCriteria(x => x.TranslationEnglish == true);
                            break;
                        case VideoLanguageKinds.Other:
                            base.AddCriteria(x => x.TranslationOther == true);
                            break;
                    }
                });
            }
        }

        /// <summary>
        /// 並び替え設定
        /// </summary>
        /// <param name="isDesc"></param>
        /// <param name="sortExpression"></param>
        private void settingOrder(bool isDesc, Expression<Func<OutsourceVideo, object>> sortExpression)
        {
            if (isDesc)
                ApplyOrderByDesc(sortExpression);
            else
                ApplyOrderBy(sortExpression);
        }
    }
}
