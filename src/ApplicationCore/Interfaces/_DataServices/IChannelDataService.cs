using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces._DataServices
{
    /// <summary>
    /// チャンネル情報IDataSerivce
    /// </summary>
    public interface IChannelDataService
    {

        /// <summary>
        /// チャンネルリストの取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <returns></returns>
        Task<List<OutsourceVideoChannel>> GetList(int page, int displayNum, Expression<Func<OutsourceVideoChannel, object>> sortFunc, bool isDesc);

        /// <summary>
        /// 総レコード数取得
        /// </summary>
        /// <returns></returns>
        Task<int> GetCount();
    }
}
