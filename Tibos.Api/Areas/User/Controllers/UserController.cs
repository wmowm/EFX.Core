using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Tibos.Common;
using Tibos.ConfingModel;
using Tibos.ConfingModel.model;
using Tibos.Domain;
using Tibos.IService.Dhm;
using Tibos.IService.Tibos;

namespace Tibos.Api.Areas.User.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    [Annotation.AlwaysAccessibleAttribute]
    public class UserController : Controller
    {


        private IMemoryCache _Cache;

        //属性注入
        public IDictService _DictService { get; set; }
        public IService.Dhm.IManagerService _ManagerService { get; set; }

        public IDictTypeService _DictTypeService { get; set; }

        //构造函数注入
        public UserController(IMemoryCache memoryCache)
        {
            _Cache = memoryCache;
        }

        [HttpGet]
        public async Task<JsonResult> Get(string id)
        {
            return await Task.Run<JsonResult>(() =>
            {
                PageResponse json = new Tibos.Common.PageResponse();
                //获取dhm库的数据
                var list_manager = _ManagerService.GetList();
                //获取tibos库数据
                var list_dict = _DictService.GetList();

                var list_dy = _DictTypeService.GetList();

                Dict dict = new Dict()
                {
                    Id = "testtest",
                    Description = "test",
                    Name = "test",
                    Sort = 99,
                    Status = 0,
                    Tid = "test"
                };

                //var res =_DictService.Add(dict, false);
                //_DictService.SaveChanges(id=="1");

                //测试自定义业务
                //var test = _DictService.GetTest();
                //json.data = list_dict;
                return Json(json);
            });
        }

        [HttpPost]
        public JsonResult Test(Domain.Dict dict)
        {
            return Json("");
        }

        [HttpPost]
        public JsonResult Test2(Annotation.Dict dict)
        {
            return Json("");
        }

    }
}
