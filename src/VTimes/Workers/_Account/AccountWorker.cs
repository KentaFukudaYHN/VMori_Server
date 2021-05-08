using ApplicationCore.Interfaces;
using ApplicationCore.ReqRes;
using System;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ViewModel;

namespace VMori.Workers
{
    /// <summary>
    /// アカウントWorker
    /// </summary>
    public class AccountWorker : IAccountWorker
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccountWorker(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<bool> Regist(RegistAccountViewModel vm)
        {
            //reqの生成
            var req = new RegistAccountReq()
            {
                Mail = vm.Mail,
                Password = vm.Password,
                BirthDay = new DateTime(int.Parse(vm.Year), int.Parse(vm.Month), int.Parse(vm.Day)),
                Name = vm.Name
            };

            return await _accountService.Regist(req);
        }

        /// <summary>
        /// メールアドレスの使用チェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<bool> NotExitsMail(string mail)
        {
            return await _accountService.NotExistsMail(mail);
        }
    }
}
