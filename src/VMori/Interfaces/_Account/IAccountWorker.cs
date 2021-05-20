using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
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
        /// ユーザーアイコン登録
        /// </summary>
        /// <param name="file"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<string> RegistIcon(string base64, string fileName, ApplicationDataContainer adc);

        /// <summary>
        /// 名前の更新
        /// </summary>
        /// <param name="req"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdateName(ChangeNameReq req, ApplicationDataContainer adc);

        /// <summary>
        /// 誕生日の更新
        /// </summary>
        /// <param name="req"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdateBirthday(ChangebBrthdayReq req, ApplicationDataContainer adc);

        /// <summary>
        /// メールアドレスの更新
        /// </summary>
        /// <param name="req"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdateMail(ChangeMailReq req, ApplicationDataContainer adc);

        /// <summary>
        /// パスワードの更新
        /// </summary>
        /// <param name="password"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdatePassword(string password, ApplicationDataContainer adc);

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
