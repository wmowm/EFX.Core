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
using Tibos.Api.Annotation;
using Tibos.Common;
using Tibos.ConfingModel;
using Tibos.ConfingModel.model;
using Tibos.Domain;
using Tibos.IRepository;
using Tibos.IService.Dhm;
using Tibos.IService.Tibos;
using Tibos.Repository.Tibos;
using Tibos.Service;
using Tibos.Service.Tibos;

namespace Tibos.Api.Areas.User.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    [AlwaysAccessibleAttribute]
    public class UserController : Controller
    {


        private IMemoryCache _Cache;

        //属性注入
        public IDictService _DictService { get; set; }
        public IManagerService _ManagerService { get; set; }

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
                Common.Json json = new Common.Json();
                //获取dhm库的数据
                var list_manager = _ManagerService.GetList();
                //获取tibos库数据
                var list_dict = _DictService.GetList();


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
        public JsonResult Test()
        {
            _DictService.SaveChanges();
            return Json("");
        }
    }
}
