using ApplicationCore.Entities;
using ApplicationCore.ReqRes;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// アカウントInterface
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// ログイン中のアカウントを取得
        /// </summary>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<AccountRes> GetLoginAccount(ApplicationDataContainer adc);

        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<bool> Regist(RegistAccountReq req);

        /// <summary>
        /// ユーザーアイコンの登録
        /// </summary>
        /// <param name="file"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<string> RegistIcon(byte[] base64, string fileName, ApplicationDataContainer adc);

        /// <summary>
        /// 名前の更新
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdateName(string name, ApplicationDataContainer adc);

        /// <summary>
        /// 誕生日の更新
        /// </summary>
        /// <param name="birthday"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdateBirthday(DateTime birthday, ApplicationDataContainer adc);

        /// <summary>
        /// メールアドレスの更新
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdateMail(string mail, ApplicationDataContainer adc);

        /// <summary>
        /// パスワードの更新
        /// </summary>
        /// <param name="password"></param>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> UpdatePassword(string password, ApplicationDataContainer adc);

        /// <summary>
        /// 使用可能なメールアドレスかチェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<bool> CanRegistMail(string mail);

        /// <summary>
        /// 使用可能な名前かチェック
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> CanRegistName(string name);
    }
}
