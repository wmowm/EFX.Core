using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Tibos.Service;
using Tibos.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections;
using Tibos.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment hostingEnv;
        private IViewRenderService _viewRenderService;
        public HomeController(IHostingEnvironment env, IViewRenderService viewSendeRenderService)
        {
            _viewRenderService = viewSendeRenderService;
            hostingEnv = env;
        }
        public IActionResult Index()
        {
            ViewData["GUID"] = Guid.NewGuid();
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            //删除自己的凭证
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //删除验证服务器上面的凭证
            await HttpContext.SignOutAsync("oidc");

            return View();
        }



    }
}
