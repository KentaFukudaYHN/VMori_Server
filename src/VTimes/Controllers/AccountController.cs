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
        public async Task<bool> Regist(RegistAccountViewModel vm)
        {
            return await _accountWorker.Regist(vm);
        }

        /// <summary>
        /// メールの使用チェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<bool> NotExitsMail(string mail)
        {
            return await _accountWorker.NotExitsMail(mail);
        }
    }
}
