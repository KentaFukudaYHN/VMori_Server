using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// チャンネル推移データ条件
    /// </summary>
    public class  ChannelTransitionSpecifications : BaseSpecification<ChannelTransition>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChannelTransitionSpecifications(): base(null)
        {

        }

        public void AddCriteriaByChannelId(string channelId)
        {
            base.AddCriteria(x => x.ChannelId == channelId);
        }
    }
}
