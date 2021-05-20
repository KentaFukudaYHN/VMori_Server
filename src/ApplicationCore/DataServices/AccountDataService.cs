using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
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

        public async Task<Account> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("idが空です");

            return await _repository.GetByIdAsync(id);
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
        /// 名前が一致するアカウントを全件取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<Account>> GetListByNameAsync(string name)
        {
            return (await _repository.ListAsync(new AccountWithNameSpecification(name))).ToList();
        }

        /// <summary>
        /// メールアドレス本人認証の更新
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateAppMail(string id, bool appMail, IDbContext db)
        {
            var account = new Account()
            {
                ID = id,
                AppMail = appMail
            };

            await _repository.UpdateAsyncOnlyClumn(account, new List<string>() { nameof(Account.AppMail) }, db);

            return true;
        }

        /// <summary>
        /// ユーザーアイコンを登録
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<bool> UpdateIcon(string fileName, ApplicationDataContainer adc)
        {
            var account = new Account()
            {
                ID = adc.LoginUser.Id,
                Icon = fileName
            };

            await _repository.UpdateAsyncOnlyClumn(account, new List<string>() { nameof(Account.Icon) });

            return true;
        }

        /// <summary>
        /// 名前の更新
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdateName(string name, ApplicationDataContainer adc)
        {
            var accout = new Account()
            {
                ID = adc.LoginUser.Id,
                Name = name
            };

            await _repository.UpdateAsyncOnlyClumn(accout, new List<string> { nameof(Account.Name) });

            return true;
        }

        /// <summary>
        /// 誕生日の更新
        /// </summary>
        /// <param name="birthday">yyyyMMdd</param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBirthday(string birthday, ApplicationDataContainer adc)
        {
            var account = new Account()
            {
                ID = adc.LoginUser.Id,
                Birthday = birthday
            };

            await _repository.UpdateAsyncOnlyClumn(account, new List<string> { nameof(Account.Birthday) });

            return true;
        }

        /// <summary>
        /// メールアドレスの更新
        /// </summary>
        /// <param name="password"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMail(string mail, ApplicationDataContainer adc)
        {
            var account = new Account()
            {
                ID = adc.LoginUser.Id,
                Mail = mail
            };

            await _repository.UpdateAsyncOnlyClumn(account, new List<string> { nameof(Account.Mail) });

            return true;
        }

        /// <summary>
        /// パスワードの更新
        /// </summary>
        /// <param name="password"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassword(string password, ApplicationDataContainer adc)
        {
            var account = new Account()
            {
                ID = adc.LoginUser.Id,
                Password = _hashService.Hashing(password)
            };

            await _repository.UpdateAsyncOnlyClumn(account, new List<string>() { nameof(Account.Password) });

            return true;
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

        /// <summary>
        /// アカウントの名前で検索した結果の総数を取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<int> CountByName(string name)
        {
            return await _repository.CountAsync(new AccountWithNameSpecification(name));
        }
    }
}
