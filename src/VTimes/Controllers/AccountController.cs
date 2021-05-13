using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ViewModel;

namespace VMori.Controllers
{
    public class AccountController : VMoriBaseController
    {
        private readonly IAccountWorker _accountWorker;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccountController(IAccountWorker accountWorker)
        {
            _accountWorker = accountWorker;
        }

        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Regist(RegistAccountViewModel vm)
        {
            return await _accountWorker.Regist(vm);
        }

        /// <summary>
        /// メールの使用チェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<bool> CanRegistMail(string mail)
        {
            return await _accountWorker.CanRegistMail(mail);
        }

        /// <summary>
        /// 登録可能な名前かチェック
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> CanRegistname(string name)
        {
            return await _accountWorker.CanRegistName(name);
        }
    }
}
