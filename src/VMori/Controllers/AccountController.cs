using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ReqRes;
using VMori.ReqRes._Account;

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
        /// ログイン中のアカウント情報を取得
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<AccountRes> Get()
        {
            return await _accountWorker.Get(base.ADC);
        }

        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Regist(RegistAccountReq vm)
        {
            return await _accountWorker.Regist(vm, HttpContext);
        }

        /// <summary>
        /// ユーザーアイコンの登録
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<bool> RegistIcon(ChangeIconReq req)
        {
            return await _accountWorker.RegistIcon(req, ADC);
        }

        /// <summary>
        /// 名前の更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<bool> UpdateName(ChangeNameReq req)
        {
            return await _accountWorker.UpdateName(req, ADC);
        }

        /// <summary>
        /// 誕生日の更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<bool> UpdateBirthday(ChangebBrthdayReq req)
        {

            return await _accountWorker.UpdateBirthday(req, ADC);
        }

        /// <summary>
        /// メールアドレスの更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<bool> UpdateMail(ChangeMailReq req)
        {
            return await _accountWorker.UpdateMail(req, ADC);
        }

        /// <summary>
        /// パスワードの更新
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<bool> UpdatePassword(ChangeReqPasswordReq req)
        {
            return await _accountWorker.UpdatePassword(req.Password, ADC);
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
