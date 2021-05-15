using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ReqRes._Account;

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
        /// ログイン中のアカウント情報を取得
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<AccountRes> Get(ApplicationDataContainer adc)
        {
            var res = await _accountService.GetLoginAccount(adc);

            return new AccountRes(res);
        }

        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<bool> Regist(ReqRes.RegistAccountReq vm)
        {
            //reqの生成
            var req = new ApplicationCore.ReqRes.RegistAccountReq()
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
        public async Task<bool> CanRegistMail(string mail)
        {
            return await _accountService.CanRegistMail(mail);
        }

        /// <summary>
        /// 登録可能な名前かチェック
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> CanRegistName(string name)
        {
            return await _accountService.CanRegistName(name);
        }
    }
}
