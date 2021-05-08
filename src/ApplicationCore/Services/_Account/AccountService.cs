using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ReqRes;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataService _accountDataService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccountService(IAccountDataService accountDataService)
        {
            _accountDataService = accountDataService;
        }

        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<bool> Regist(RegistAccountReq req)
        {
            //メールアドレスのチェック ※形式が正しくない場合例外が発生
            new MailAddress(req.Mail);

            var account = new Account();
            account.ID = Guid.NewGuid().ToString();
            account.Mail = req.Mail;
            account.Name = req.Name;
            account.Password = req.Password; //パスワードはデータサービスでHash化される
            account.Birthday = req.BirthDay.ToString("yyyyMMdd");
            account.RegistDateTime = DateTime.Now;

            return await _accountDataService.RegistAsync(account);
        }

        /// <summary>
        /// メールアドレスの使用チェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<bool> NotExistsMail(string mail)
        {
            return await _accountDataService.GetAsync(mail) == null;
        }

    }
}
