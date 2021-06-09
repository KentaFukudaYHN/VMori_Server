using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    public class UpReqOutsourceVideoDataService : IUpReqOutsourceVideoDataService
    {
        private readonly IAsyncRepository<UpReqOutsourceVideo> _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public UpReqOutsourceVideoDataService(IAsyncRepository<UpReqOutsourceVideo> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// IDで取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UpReqOutsourceVideo> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("IDが空です");

            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Regist(UpReqOutsourceVideo entity)
        {
            if (string.IsNullOrEmpty(entity.ID))
                throw new ArgumentException("IDが空です");

            await _repository.AddAsync(entity);

            return true;
        }
    }
}
