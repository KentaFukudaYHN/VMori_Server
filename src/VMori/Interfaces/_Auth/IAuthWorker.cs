using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using VMori.ReqRes;

namespace VMori.Interfaces
{
    /// <summary>
    /// 認証Worker
    /// </summary>
    public interface IAuthWorker
    {
        /// <summary>
        /// ログイン
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        Task<string> Login(LoginViewModel vModel, HttpContext context);

        /// <summary>
        /// パスワード変更の認証コードチェック
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<LetterAndSuccessRes> CheckChangeReqPassword(CheckChangeReqPasswordReq req);

        /// <summary>
        /// メールアドレス本人認証の最中かどうか
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> InMiddleAppReqMail(string token);

        /// <summary>
        /// メールアドレスの本人認証のメール要求
        /// </summary>
        /// <param name="adc"></param>
        /// <returns></returns>
        Task<bool> CreateAppReqmail(ApplicationDataContainer adc);

        /// <summary>
        /// メールアドレスの本人認証
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>

        Task<AppReqMailRes> AppReqMail(AppReqMailReq req);

        /// <summary>
        /// パスワードの変更要求作成
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<bool> CreateChangeReqPassword(CreateChangeReqPasswordReq req);
        
        /// <summary>
        /// パスワードの変更要求
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<LetterAndSuccessRes> ChangeReqPassword(ChangeReqPasswordReq req);
    }
}
