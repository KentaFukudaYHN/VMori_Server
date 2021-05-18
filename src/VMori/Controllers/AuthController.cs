﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ReqRes;

namespace VMori.Controllers
{
    [Route("[controller]/{action}")]
    public class AuthController : VMoriBaseController
    {
        private readonly IAuthWorker _authWorker;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="authWorker"></param>
        public AuthController(IAuthWorker authWorker)
        {
            _authWorker = authWorker;
        }

        /// <summary>
        /// ログイン
        /// </summary>
        /// <param name="vModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vModel)
        {
            var isValid = await _authWorker.Login(vModel, HttpContext);

            if (isValid == false)
                return StatusCode((int)HttpStatusCode.Unauthorized);

            return StatusCode((int)HttpStatusCode.OK);
        }

        /// <summary>
        /// メールアドレスの本人認証の最中か
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> CheckAppReqMail(string token)
        {
            return await _authWorker.InMiddleAppReqMail(token);
        }

        /// <summary>
        /// メールアドレスの本人認証
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AppReqMailRes> AppReqMail(AppReqMailReq req)
        {
            return await _authWorker.AppReqMail(req);
        }
    }
}