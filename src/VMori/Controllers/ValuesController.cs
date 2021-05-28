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
        private readonly IYoutubeVideoService _youtubeVideoService;
        public ValuesController(IMailService mailService, IYoutubeVideoService youtubeVideoService)
        {
            _mailService = mailService;
            _youtubeVideoService = youtubeVideoService;
        }

        public async Task<string> Get()
        {
            await _youtubeVideoService.GetVideo("OoZKiYZEO9c");
            await _mailService.SendMail("kenterta0@gmail.com", "テスト", "こんにちは");
            return "Hello World 1";
        }
    }
}
