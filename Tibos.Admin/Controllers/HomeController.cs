using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Tibos.Admin.Annotation;
using Tibos.Admin.Models;
using Tibos.Common;
using Tibos.IService.Tibos;

namespace Tibos.Admin.Controllers
{
    public class HomeController : Controller
    {
        public INavigationService _NavigationService { get; set; }

        public IManagerService _ManagerService { get; set; }

        public IMemoryCache _MemoryCache { get; set; }




        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Index2()
        {
            return View();
        }

        [AlwaysAccessible]
        public IActionResult Login()
        {
            HttpContext.SignOutAsync();
            return View();
        }

        [AlwaysAccessible]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }


        [AlwaysAccessible]
        public IActionResult MineSweeping()
        {
            return View();
        }


        [AlwaysAccessible]
        public IActionResult Sudoku()
        {
            return View();
        }

        [AlwaysAccessible]
        public IActionResult Menu()
        {
            var list = _NavigationService.GetList();
            return PartialView("_Menu", list.ToList());
        }

        [AlwaysAccessible]
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            string pic_code = "";
            byte[] b = new VerifyCode().GetVerifyCode(ref pic_code);
            HttpContext.Session.SetString("pic_code", pic_code);//存入session
            return File(b, @"image/Gif");
        }

        [AlwaysAccessible]
        [HttpPost]
        public JsonResult Login(string user_name, string password, string code, string returnUrl)
        {
            PageResponse json = new Tibos.Common.PageResponse();
            string pic_code = HttpContext.Session.GetString("pic_code");
            using (var md5 = MD5.Create())
            {
                var res = md5.ComputeHash(Encoding.UTF8.GetBytes(code.ToLower()));
                code = BitConverter.ToString(res);
            }

            if (string.IsNullOrEmpty(pic_code))
            {
                json.status = -1;
                json.msg = "请获取验证码";
                return Json(json);
            }
            if (pic_code != code)
            {
                json.status = -1;
                json.msg = "验证码不正确";
                return Json(json);
            }
            var model = _ManagerService.Get(m => m.UserName == user_name && m.Password == password && m.Status == 1);
            if (model != null)
            {
                //只使用授权，认证功能使用自定义认证
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, model.Id));
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                //缓存用户(滑动过期15分钟)
                _MemoryCache.GetOrCreate(model.Id, entry =>
                {
                    entry.SetSlidingExpiration(TimeSpan.FromSeconds(15 * 60)); //15分钟
                    return (model);
                });
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    json.returnUrl = returnUrl;
                }
                else
                {
                    json.returnUrl = "/home/index";
                }
            }
            else
            {
                json.status = -1;
                json.msg = "帐户或者密码不正确";
            }
            return Json(json);
        }


        //该方法会被拦截
        public JsonResult CheckLogin()
        {
            
            PageResponse response = new PageResponse();
            return Json(response);
        }
    }
}
