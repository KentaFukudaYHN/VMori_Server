using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using VMori.Interfaces;
using VMori.ViewModel;

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

        public IActionResult LoginTest()
        {
            return StatusCode((int)HttpStatusCode.OK);
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
    }
}
