using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMori.Attribute;

namespace VMori.Controllers
{
    /// <summary>
    /// Vtuberの森のBaseController
    /// </summary>
    [PreflightRequest]
    [Consumes("application/json")]
    [Route("[controller]/{action}")]
    [AuthenticatedAction]
    [ApiController]
    public abstract class VMoriBaseController : Controller
    {
        public ApplicationDataContainer ADC;
    }
}
