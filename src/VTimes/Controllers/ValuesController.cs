using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMori.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly IMailService _mailService;
        public ValuesController(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task<string> Get()
        {
            await _mailService.SendMail("kenterta0@gmail.com", "テスト", "こんにちは");
            return "Hello World 1";
        }
    }
}
