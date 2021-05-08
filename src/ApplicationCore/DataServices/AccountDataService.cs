using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.DataServices
{
    public class AccountDataService : IAccountDataService
    {
        private readonly IAsyncRepository<Account> _repository;
        private readonly IHashService _hashService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public AccountDataService(IAsyncRepository<Account> repository, IHashService hashService)
        {
            _repository = repository;
            _hashService = hashService;
        }

        /// <summary>
        /// アカウント情報の取得
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Account> GetAsync(string mail, string password)
        {

            var account = await this.GetAsync(mail);
            if (account == null)
                return null;

            //パスワードの照合
            if (_hashService.Verify(account.Password, password) == false)
                return null;

            return account;
        }

        /// <summary>
        /// アカウント情報の取得
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<Account> GetAsync(string mail)
        {
            return (await _repository.ListAsync(new AccountSpecification(mail))).FirstOrDefault();
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> RegistAsync(Account account)
        {
            //パスワードのハッシュ化
            account.Password = _hashService.Hashing(account.Password);

            await _repository.AddAsync(account);

            return true;
        }
    }
}
