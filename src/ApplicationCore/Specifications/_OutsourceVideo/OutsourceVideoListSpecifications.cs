using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// ページング用検索条件
    /// </summary>
    public class OutsourceVideoListSpecifications : BaseSpecification<OutsourceVideo>
    {
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
