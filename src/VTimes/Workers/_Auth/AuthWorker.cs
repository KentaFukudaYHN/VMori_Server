using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ViewModel;

namespace VMori.Workers
{
    internal class AuthWorker : IAuthWorker
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="authService"></param>
        public AuthWorker(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// ログイン
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public async Task<bool> Login(LoginViewModel vModel, HttpContext context)
        {
            return await _authService.Login(vModel.Mail, vModel.Password, context);
        }
    }
}
