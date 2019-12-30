using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptionless;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWeb.Models;

namespace TestWeb.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            try
            {
                var t = "123xxx";
                var m = Convert.ToInt32(t);
            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
            }
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadFiles(string fileName)
        {
            try
            {
                IFormFileCollection cols = Request.Form.Files;
                if (cols == null || cols.Count == 0)
                {
                    return Json(new { status = -1, message = "没有上传文件" });
                }
                foreach (IFormFile file in cols)
                {
                    //定义图片数组后缀格式
                    string[] LimitPictureType = { ".JPG", ".JPEG", ".GIF", ".PNG", ".BMP" };
                    //获取图片后缀是否存在数组中
                    string currentPictureExtension = Path.GetExtension(file.FileName).ToUpper();
                    if (LimitPictureType.Contains(currentPictureExtension))
                    {
                        var path = Path.Combine("upload/images/", file.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {

                            await file.CopyToAsync(stream);
                        }
                    }
                    else
                    {
                        return Json(new { status = -2, message = "请上传指定格式的图片" });
                    }
                }

                return Json(new { status = 0, message = "上传成功" });
            }
            catch (Exception ex)
            {

                return Json(new { status = -3, message = "上传失败", data = ex.Message });
            }
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
