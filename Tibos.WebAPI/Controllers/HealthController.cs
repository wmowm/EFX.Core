using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tibos.WebAPI.Controllers
{


    [Produces("application/json")]
    [Route("api/Health")]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Ok("ok");
    }
}