using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    /// <summary>
    /// メールアドレス承認要求DataService
    /// </summary>
    public class AppReqMailDataService : IAppReqMailDataService
    {
        private readonly IAsyncRepository<AppReqMail> _asyncRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AppReqMailDataService(IAsyncRepository<AppReqMail> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Regist(AppReqMail entity)
        {
            if (string.IsNullOrEmpty(entity.ID))
                throw new ArgumentException("IDが設定されていません");

            await _asyncRepository.AddAsync(entity);

            return true;
        }

        /// <summary>
        /// Tokenで情報参照
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AppReqMail> GetByToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("Tokenが空です");
            return (await _asyncRepository.ListAsync(new AppReqMailSpecification(token))).FirstOrDefault();
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string id, IDbContext db)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("idが空です");

            await _asyncRepository.DeleteByIdAsync(id, db);

            return true;
        }
    }
}
