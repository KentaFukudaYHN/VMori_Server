using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// チャンネル情報検索条件
    /// </summary>
    public class OutsourceVideoChannelSpecification: BaseSpecification<OutsourceVideoChannel>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OutsourceVideoChannelSpecification(): base(null)
        {

        }

        /// <summary>
        /// チャンネルIDの検索条件追加
        /// </summary>
        /// <param name="channelId"></param>
        public void AddCredentialByChannelId(string channelId)
        {
            base.AddCriteria(x => x.ChannelId == channelId);
        }
    }
}
