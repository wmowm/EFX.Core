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
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Api()
        {
            return View();
        }

        public IActionResult Api2()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Html()
        {
            return View();
        }

        List<Wiki> list_wiki = new List<Wiki>();

        public IActionResult Wiki()
        {
            list_wiki = new List<Wiki>();
            list_wiki.Add(new Controllers.Wiki() { title = "Web区域", content = "测试,测试,测试", ClientType = "IOS", id = 1 });
            list_wiki.Add(new Controllers.Wiki() { title = "IOS区域", content = "测试,测试,测试", ClientType = "Android", id = 2 });
            list_wiki.Add(new Controllers.Wiki() { title = "Android区域", content = "测试,测试,测试", ClientType = "Web", id = 3 });
            ViewData["list_wiki"] = list_wiki;
            return View();
        }

        [HttpPost]
        public JsonResult GetSubWiki(int id)
        {
            list_wiki = new List<Wiki>();
            var res = "<pre class=\"prettyprint lang-cs\">public JsonResult GetWikiList(int limit = 10, int offset = 1)</ pre ><br /> 测试,测试";

            list_wiki.Add(new Controllers.Wiki() { title = "怎么调用API?", content = res, ClientType = "IOS", id = 1 });
            list_wiki.Add(new Controllers.Wiki() { title = "注册", content = "测试,测试,测试", ClientType = "Android", id = 2 });
            list_wiki.Add(new Controllers.Wiki() { title = "登录", content = "测试,测试,测试", ClientType = "Web", id = 3 });
            return Json(list_wiki);
        }

        public IActionResult UpImg()
        {
            return View();
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
            Json json = new Tibos.Common.Json();
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public async Task<IActionResult> KKK()
        {
            var result = await _viewRenderService.RenderToStringAsync("Home/index", "");
            await WriteViewAsync(result);
            return Content(result);
        }




        public async Task<IActionResult> UeditorUpload()
        {
            var files = Request.Form.Files;
            string callback = Request.Query["callback"];
            string editorId = Request.Query["editorid"];
            if (files != null && files.Count > 0)
            {
                var file = files[0];
                string contentPath = hostingEnv.WebRootPath;
                string fileDir = Path.Combine(contentPath, "upload");
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }
                string fileExt = Path.GetExtension(file.FileName);
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt;
                string filePath = Path.Combine(fileDir, newFileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                var fileInfo = getUploadInfo("../../../upload/" + newFileName, file.FileName,
                    Path.GetFileName(filePath), file.Length, fileExt);
                string json = BuildJson(fileInfo);

                Response.ContentType = "text/html";
                if (callback != null)
                {
                    await Response.WriteAsync(String.Format("<script>{0}(JSON.parse(\"{1}\"));</script>", callback, json));
                }
                else
                {
                    await Response.WriteAsync(json);
                }
                return View();
            }
            return NoContent();
        }
        private string BuildJson(Hashtable info)
        {
            List<string> fields = new List<string>();
            string[] keys = new string[] { "originalName", "name", "url", "size", "state", "type" };
            for (int i = 0; i < keys.Length; i++)
            {
                fields.Add(String.Format("\"{0}\": \"{1}\"", keys[i], info[keys[i]]));
            }
            return "{" + String.Join(",", fields) + "}";
        }
        /**
       * 获取上传信息
       * @return Hashtable
       */
        private Hashtable getUploadInfo(string URL, string originalName, string name, long size, string type, string state = "SUCCESS")
        {
            Hashtable infoList = new Hashtable();

            infoList.Add("state", state);
            infoList.Add("url", URL);
            infoList.Add("originalName", originalName);
            infoList.Add("name", Path.GetFileName(URL));
            infoList.Add("size", size);
            infoList.Add("type", Path.GetExtension(originalName));

            return infoList;
        }





        /// <summary>
        /// 将视图写入文件
        /// </summary>
        /// <param name="info">路由信息</param>
        /// <returns></returns>
        public async Task WriteViewAsync(string html)
        {
            //创建文件流  
            FileStream myfs = new FileStream(@"E:\GitProject\nh.core\Tibos.Web\temp\aaa.html", FileMode.Create);
            //打开方式  
            //1:Create  用指定的名称创建一个新文件,如果文件已经存在则改写旧文件  
            //2:CreateNew 创建一个文件,如果文件存在会发生异常,提示文件已经存在  
            //3:Open 打开一个文件 指定的文件必须存在,否则会发生异常  
            //4:OpenOrCreate 打开一个文件,如果文件不存在则用指定的名称新建一个文件并打开它.  
            //5:Append 打开现有文件,并在文件尾部追加内容.  

            //创建写入器  
            StreamWriter mySw = new StreamWriter(myfs);//将文件流给写入器  
            //将录入的内容写入文件  
            mySw.Write(html);
            //关闭写入器  
            mySw.Close();
            //关闭文件流  
            myfs.Close();
        }



    }
    public class Wiki
    {

        public int id { get; set; }
        public string title { get; set; }

        public string ClientType { get; set; }

        public string content { get; set; }
    }




}
