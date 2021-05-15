using ApplicationCore.Entities;
using System.Threading.Tasks;
using VMori.ReqRes;
using VMori.ReqRes._Account;

namespace VMori.Interfaces
{
    public interface IAccountWorker
    {
        /// <summary>
        /// ログイン中のアカウント情報取得
        /// </summary>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<AccountRes> Get(ApplicationDataContainer adc);

        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        Task<bool> Regist(RegistAccountReq vm);

        /// <summary>
        /// 登録可能な名前かチェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<bool> CanRegistMail(string mail);

        /// <summary>
        /// 登録可能な名前かチェック
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> CanRegistName(string name);
    }
}
