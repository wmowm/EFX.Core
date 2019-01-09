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
using Tibos.Service.Contract;

namespace Tibos.Api.Areas.User.Controllers
{
    [AlwaysAccessibleAttribute]
    [Produces("application/json")]
    [Route("api/User/")]
    [EnableCors("any")]
    public class UserController : Controller
    {


        private IMemoryCache _Cache;

        //属性注入
        public ManagerIService _ManagerService { get; set; }


        //构造函数注入
        public UserController(IMemoryCache memoryCache)
        {
            _Cache = memoryCache;
        }

        [AlwaysAccessibleAttribute]
        [HttpGet]
        public async Task<JsonResult> Get(string id)
        {
            return await Task.Run<JsonResult>(() =>
            {
                Common.Json json = new Common.Json();
                var model = _ManagerService.Get(id);
                json.data = model;
                return Json(json);
            });
        }

        [Route("gettoken"), HttpGet]
        public async Task<JsonResult> GetToken(string user_name, string password)
        {
            return await Task.Run<JsonResult>(() =>
            {
                //自定义返回json对象
                Json json = new Json();

                //1.开始参数验证
                if (string.IsNullOrEmpty(user_name))
                {
                    json.status = -1;
                    json.msg = "用户名不能为空!";
                    return Json(json);
                }
                if (string.IsNullOrEmpty(password))
                {
                    json.status = -1;
                    json.msg = "密码不能为空!";
                    return Json(json);
                }
                //自定义参数模板
                List<SearchTemplate> st = new List<SearchTemplate>();
                st.Add(new SearchTemplate() { key = "user_name", value = user_name, searchType = EnumBase.SearchType.Eq });
                st.Add(new SearchTemplate() { key = "password", value = password, searchType = EnumBase.SearchType.Eq });
                var list = _ManagerService.GetList(m => m.UserName == user_name && m.Password == password, null, null);
                if (list.Count > 0)
                {
                    //获取token
                    var tokenconfig = JsonConfigurationHelper.GetAppSettings<TokenConfig>("TokenConfig.json", "TokenConfig");
                    Token tokenHelper = new Token();
                    var token = tokenHelper.CreateToken(user_name, password, tokenconfig.Sign);

                    //登录成功,设置用户token
                    _Cache.GetOrCreate(token, entry =>
                    {
                        entry.SetSlidingExpiration(TimeSpan.FromSeconds(15 * 60)); //15分钟
                        return (list[0].Id);
                    });
                    //var result = _Cache.Get(token);
                    json.msg = "登录成功!";
                    json.data = token;
                }
                else
                {
                    json.status = -1;
                    json.msg = "用户名或密码不正确!";
                }
                return Json(json);
            });
        }

        [Route("checktoken"), HttpPost]
        public async Task<JsonResult> CheckToken(string token)
        {
            return await Task.Run<JsonResult>(() =>
            {
                Json json = new Json();
                json = new Token(_Cache).CheckToken(token);
                return Json(json);
            });
        }

        [AlwaysAccessible]
        [Route("getlist"), HttpPost]
        public async Task<JsonResult> GetList(ManagerRequest request)
        {
            return await Task.Run<JsonResult>(() =>
            {
                Json json = new Common.Json();
                var list = _ManagerService.GetList(request);
                var list_count = _ManagerService.GetCount(request);
                json.data = list;
                json.total = list_count;




                return Json(json);
            });
        }

        [HttpPost]
        public async Task<JsonResult> Add(Manager user)
        {
            return await Task.Run<JsonResult>(() =>
            {
                Json json = new Common.Json();
                if (string.IsNullOrEmpty(user.UserName))
                {
                    json.msg = "用户名不能为空!";
                    json.status = -1;
                    return Json(json);
                }
                var id = _ManagerService.Save(user);
                json.data = id;
                json.msg = "添加成功!";
                return Json(json);
            });
        }

        [HttpPut]
        public async Task<JsonResult> Edit(Manager user)
        {
            return await Task.Run<JsonResult>(() =>
            {
                Json json = new Common.Json();
                _ManagerService.Update(user);
                json.msg = "修改成功!";
                return Json(json);
            });
        }
    }
}
