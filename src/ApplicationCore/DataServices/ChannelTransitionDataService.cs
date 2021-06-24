using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// チャンネル推移データDataService
    /// </summary>
    public class ChannelTransitionDataService : IChannelTransitionDataService
    {
        private readonly IAsyncRepository<ChannelTransition> _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public ChannelTransitionDataService(IAsyncRepository<ChannelTransition> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// チャンネルIDで取得
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<List<ChannelTransition>> GetListByChannelId(string channelId)
        {
            if (string.IsNullOrEmpty(channelId))
                throw new ArgumentException("パラメーターが不正です");

            var spec = new ChannelTransitionSpecifications();
            spec.AddCriteriaByChannelId(channelId);

            var result = await _repository.ListAsync(spec);

            if (result == null)
                return null;

            return result.ToList();
        }
    }
}
