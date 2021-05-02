using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// 認証IService
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// ログイン
        /// </summary>
        /// <returns></returns>
        Task<bool> Login(string mail, string ps, HttpContext context);
    }
}
