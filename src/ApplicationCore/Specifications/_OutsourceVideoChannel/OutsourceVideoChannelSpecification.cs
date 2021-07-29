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
    public class OutsourceVideoChannelSpecification: BaseSpecification<Entities.Channel>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OutsourceVideoChannelSpecification(): base(null)
        {

        }
    }
}
