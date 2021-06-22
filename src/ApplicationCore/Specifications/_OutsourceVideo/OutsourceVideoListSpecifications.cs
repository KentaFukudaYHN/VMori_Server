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
            //ページのスキップ数を計算
            var skip = this.CalcSkip(page, displayNum);

            ApplyPaging(skip, displayNum);

            //登録日時順
            ApplyOrderByDescending(x => x.RegistDateTime);

            //統計情報をリレーション
            base.AddIncludes(x => x.Statistics);
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
            List<VideoLanguageKinds>? translationLangs) : this(page, displayNum)
        {
            //タイトルのフルテキスト検索条件追加
            if (string.IsNullOrEmpty(text) == false)
            {
                base.AddFullTextCriteria(x => EF.Functions.FreeText(x.VideoTitle, text) || EF.Functions.FreeText(x.Description, text) || EF.Functions.FreeText(x.TagsData, text));
            }

            //ジャンルの設定
            if (genre != null)
            {
                base.AddCriteria(x => x.Genre == genre);
            }

            //言語の設定
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

            //翻訳の有無設定
            if (isTranslation != null)
            {
                base.AddCriteria(x => x.IsTranslation == isTranslation);
            }

            //翻訳言語の設定
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
        /// チャンネルIDで検索条件追加
        /// </summary>
        /// <param name="channelId"></param>
        public void AddCriteriaByChannelId(string channelId)
        {
            base.AddCriteria(x => x.ChanelId == channelId);
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
        /// 統計情報をリレーションする
        /// </summary>
        public void AddIncludeStatistics()
        {
            base.AddIncludes(x => x.Statistics);
        }

        /// <summary>
        /// 降順で並び替え
        /// </summary>
        public void ApplyOrderByDesc(Expression<Func<OutsourceVideo, object>> expression)
        {
            base.ApplyOrderByDescending(expression);
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
    }
}
