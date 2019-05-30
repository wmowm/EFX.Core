using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tibos.Api.Annotation;
using Tibos.Api.Common;
using WebSnapshots.Models;

namespace Tibos.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PhantomJSController : ControllerBase
    {

        [AlwaysAccessible]
        [HttpPost]
        public JsonResult Snapshot([FromBody]M_Phantom model)
        {
            Console.WriteLine(model.url);
            BaseResponse response = new BaseResponse();
            try
            {
                var picpath = $"upload/{Guid.NewGuid()}.png";
                var res = WebHelp.GetHtmlImage(model.url, picpath);
                if (res == "ok\n")
                {
                    response.code = 200;
                    response.msg = "成功";
                    response.data = picpath;
                }
                else
                {
                    response.code = 501;
                    response.msg = $"转换图片失败:{res}";
                }
            }
            catch (Exception)
            {
                response.code = 500;
                response.msg = "服务端错误";
            }
            return new JsonResult(response);
        }
    }
}