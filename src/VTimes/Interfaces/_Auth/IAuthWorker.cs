using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VMori.ViewModel;

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
        Task<bool> Login(LoginViewModel vModel, HttpContext context);
    }
}
