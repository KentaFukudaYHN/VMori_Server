using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ViewModel;

namespace VMori.Controllers
{
    [ApiController]
    public class AuthController : Controller
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
        public async Task<IActionResult> Login(LoginViewModel vModel)
        {
            var isValid = await _authWorker.Login(vModel, HttpContext);

            if (isValid == false)
                return StatusCode((int)HttpStatusCode.Unauthorized);

            return StatusCode((int)HttpStatusCode.OK);
        } 
    }
}
