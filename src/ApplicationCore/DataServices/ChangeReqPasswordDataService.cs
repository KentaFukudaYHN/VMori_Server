using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// パスワード変更要求DataService
    /// </summary>
    public class ChangeReqPasswordDataService : IChangeReqPasswordDataService
    {
        private readonly IAsyncRepository<ChangeReqPassword> _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="asyncRepository"></param>
        public ChangeReqPasswordDataService(IAsyncRepository<ChangeReqPassword> asyncRepository)
        {
            _repository = asyncRepository;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Regist(ChangeReqPassword entity)
        {
            if (string.IsNullOrEmpty(entity.ID))
                throw new ArgumentException("IDが設定されていません");

            await _repository.AddAsync(entity);

            return true;
        }

        /// <summary>
        /// Tokenで検索
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ChangeReqPassword> GetByAccountID(string accountid)
        {
            if (string.IsNullOrEmpty(accountid))
                throw new ArgumentException("idが空です");

            return (await _repository.ListAsync(new ChangeReqPasswordWithAccountIDSpecification(accountid))).FirstOrDefault();
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(string id, IDbContext db)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("idが空です");

            await _repository.DeleteByIdAsync(id, db);

            return true;
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("idが空です");

            await _repository.DeleteByIdAsync(id);

            return true;
        }
    }
}
