using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VMori.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IOutsourceVideoService _OutsourceVideoService;
        public ValuesController(IMailService mailService, IOutsourceVideoService OutsourceVideoService)
        {
            _mailService = mailService;
            _OutsourceVideoService = OutsourceVideoService;
        }

        public async Task<string> Get()
        {
            return "Hello World 1";
        }
    }
}
