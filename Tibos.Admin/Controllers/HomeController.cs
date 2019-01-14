using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tibos.Admin.Annotation;
using Tibos.Admin.Models;
using Tibos.Common;
using Tibos.Service.Tibos;

namespace Tibos.Admin.Controllers
{
    [AlwaysAccessible]
    public class HomeController : Controller
    {
        public INavigationService _NavigationService { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Menu()
        {
            var list = _NavigationService.GetList();
            return PartialView("_Menu", list.ToList());
        }

        [HttpGet]
        public ActionResult GetAuthCode()
        {
            string pic_code = "";
            byte[] b = new VerifyCode().GetVerifyCode(ref pic_code);
            HttpContext.Session.SetString("pic_code", pic_code);//存入session
            return File(b, @"image/Gif");
        }

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
            if (pic_code != code)
            {
                json.status = -1;
                json.msg = "验证码不正确";
            }

            if (!String.IsNullOrEmpty(returnUrl))
            {
                json.returnUrl = returnUrl;
            }
            else
            {
                json.returnUrl = "/home/index";
            }
            return Json(json);
        }

    }
}
