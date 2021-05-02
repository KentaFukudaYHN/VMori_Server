using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces._DataServices;
using ApplicationCore.Specifications;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    public class AccountDataService : IAccountDataService
    {
        private readonly IAsyncRepository<Account> _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public AccountDataService(IAsyncRepository<Account> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// アカウント情報の取得
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Account> GetAsync(string mail, string password)
        {
            var list = await _repository.ListAsync(new AccountSpecification(mail, password));
            return list.FirstOrDefault();
        }
    }
}
