using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces._DataServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    /// <summary>
    /// 認証Service
    /// </summary>
    public class AuthService : IAuthService
    {
        IAccountDataService _accountDataService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="accountDataService"></param>
        public AuthService(IAccountDataService accountDataService)
        {
            _accountDataService = accountDataService;
        }

        /// <summary>
        /// ログイン
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public async Task<bool> Login(string mail, string ps, HttpContext context)
        {
            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(ps))
                throw new ArgumentException();

            var account = await _accountDataService.GetAsync(mail, ps);
            if (account == null)
                return false;

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Mail), //ユニーク
                new Claim(ClaimTypes.Name, account.Name)
            };

            //一意のID情報
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                //認証セッションの更新許可
                AllowRefresh = true,
                //認証セッションの有効期限を1日に設定
                ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                //認証セッションの要求間の永続化
                IsPersistent = true
            };

            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity),
                authProperties
                );

            return true;
        }
    }
}
