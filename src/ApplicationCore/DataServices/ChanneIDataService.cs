using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces._DataServices;
using ApplicationCore.Specifications.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// チャンネルDataService
    /// </summary>
    public class ChannelDataService : IChannelDataService
    {
        private readonly IAsyncRepository<OutsourceVideoChannel> _repository;

        /// <summary>
        /// チャンネルリストの取得
        /// </summary>
        /// <param name="page"></param>
        /// <param name="displayNum"></param>
        /// <returns></returns>
        public async Task<List<OutsourceVideoChannel>> GetList(int page, int displayNum, Expression<Func<OutsourceVideoChannel, object>> sortFunc, bool isDesc)
        {
            if(page == 0 || displayNum == 0)
                throw new ArgumentException("パラメーターが不正です");

            var spec = new ChannelSpecifications();
            spec.ApplyPaging(page, displayNum);
            spec.ApplySort(isDesc, sortFunc);

            var result = await _repository.ListAsync(spec);

            return result != null ? result.ToList() : null;
        }

        /// <summary>
        /// 総レコード数を取得
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCount()
        {
            return await _repository.CountAsync();
        }

        public ChannelDataService(IAsyncRepository<OutsourceVideoChannel> repository)
        {
            _repository = repository;
        }
    }
}
