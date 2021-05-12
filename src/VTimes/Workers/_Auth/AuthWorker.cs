using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// メールアドレスの本人認証の最中か
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> InMiddleAppReqMail(string token)
        {
            return await _authService.InMiddleAppReqMail(token);
        }

        /// <summary>
        /// メールアドレスの本人認証
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AppReqMailRes> AppReqMail(AppReqMailReq req)
        {
            var result = await _authService.CertificationMail(req.Password, req.Token);

            return new AppReqMailRes()
            {
                Success = result.Item1,
                ErrMsg = result.Item2
            };
        }

    }
}
