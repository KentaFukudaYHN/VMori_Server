using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// 動画統計情報検索条件
    /// </summary>
    class OutsourceVideoStatisticsSpecifications : BaseSpecification<OutsourceVideoStatistics>
    {
        /// <summary>
        /// OutsouceVideoIDで取得
        /// </summary>
        /// <param name="outrouceVideoId"></param>
        /// <param name="onlylatest"></param>
        public OutsourceVideoStatisticsSpecifications(string outrouceVideoId, bool onlylatest): base(x => x.OutsourceVideoId == outrouceVideoId)
        {
            //登録日時で並び替え
            base.ApplyOrderByDescending(x => x.GetDateTime);

            if (onlylatest)
            {
                //1件のみ取得
                base.ApplyTake(1);
            }
        }
    }
}
