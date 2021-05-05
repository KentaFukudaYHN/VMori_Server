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
    public abstract class VMoriBaseController : Controller
    {

    }
}
