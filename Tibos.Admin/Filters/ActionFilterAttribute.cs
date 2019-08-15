using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using Tibos.Admin.Annotation;
using Tibos.Common;
using Tibos.Domain;
using Tibos.IService.Tibos;

namespace Tibos.Admin.Filters
{
    public class ActionFilterAttribute : Attribute, IActionFilter
    {

        private readonly ILogger<ActionFilterAttribute> logger;
        private readonly IMemoryCache _Cache;
        private readonly IManagerService _ManagerService;
        private readonly ISysLogService _SysLogService;
        private readonly INavigationService _NavigationService;
        private readonly IRoleService _RoleService;
        private readonly IRoleNavDictService _RoleNavDictService;
        private readonly IDictService _DictService;
        private readonly IDictTypeService _DictTypeService;

        private readonly Token _Token;
        public ActionFilterAttribute(ILoggerFactory loggerFactory, IMemoryCache memoryCache, IManagerService Managerervice, ISysLogService SysLogService, INavigationService NavigationService
                                     , IRoleService RoleService, IRoleNavDictService RoleNavDictService, IDictService DictService, IDictTypeService DictTypeService
        )
        {
            logger = loggerFactory.CreateLogger<ActionFilterAttribute>();
            _Cache = memoryCache;
            _ManagerService = Managerervice;
            _Token = new Token(_Cache);
            _SysLogService = SysLogService;
            _NavigationService = NavigationService;
            _RoleService = RoleService;
            _RoleNavDictService = RoleNavDictService;
            _DictService = DictService;
            _DictTypeService = DictTypeService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var parms = "";
            // post 请求方式获取请求参数方式
            try
            {
                if (request.Method.ToLower().Equals("post"))
                {
                    var requestForm = request.Form.ToList();
                    parms = JsonConvert.SerializeObject(requestForm);
                }
            }
            catch (Exception e)
            {


            }
            //请求的唯一ID
            var requestId = context.ActionDescriptor.Id;
            //菜单对象
            var m_nav = _NavigationService.Get(m => m.ControllerName.Contains(context.RouteData.Values["controller"].ToString()));



            #region 记录日志(所有的请求)
            MonitorLog MonLog = new MonitorLog();
            MonLog.RequestID = requestId;
            MonLog.UID = context.HttpContext.User.Identity.Name;
            MonLog.ExecuteStartTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff", DateTimeFormatInfo.InvariantInfo));
            MonLog.ControllerName = context.RouteData.Values["controller"] as string;
            MonLog.ActionName = context.RouteData.Values["action"] as string;
            MonLog.QueryCollections = context.HttpContext.Request.QueryString;//Url 参数
            MonLog.BodyCollections = parms; //Form参数
            MonLog.NId = m_nav?.Id;
            _Cache.Set(requestId, MonLog, TimeSpan.FromSeconds(60));
            #endregion

            #region 根据注解允许匿名访问
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            //controller
            var controllerAttributes = actionDescriptor.MethodInfo.DeclaringType.GetCustomAttributes(typeof(AlwaysAccessibleAttribute), true);
            if (controllerAttributes != null && controllerAttributes.Length > 0)
            {
                return;
            }
            //action
            var actionAttributes = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(AlwaysAccessibleAttribute), true);
            if (actionAttributes != null && actionAttributes.Length > 0)
            {
                return;
            }
            #endregion

            #region 权限验证
            //1.获取用户实体对象
            //2.用户->角色->是否具备操作权限
            PageResponse json = new PageResponse();
            var userId = context.HttpContext.User.Identity.Name;
            //未登录
            if (userId == null)
            {
                context.HttpContext.Response.Headers.Add("TibosFilter", HttpStatusCode.Unauthorized.ToString());
                //json.code = StatusCodeDefine.Unauthorized;
                //json.status = -1;
                //json.msg = "未登录！";
                //context.Result = new JsonResult(json);
                //return;
                context.HttpContext.Response.Redirect("/home/login");
                return;
            }
            //获取用户对象
            var m_manager = _Cache.Get<Manager>(userId);

            //15分有效时间
            //if (m_manager == null)
            //{
            //    context.HttpContext.Response.Redirect("/home/login");
            //    return;
            //}
            //永不过期策略
            if (m_manager == null)
            {
                m_manager = _ManagerService.Get(m => m.Id == userId && m.Status == 1);
                if (m_manager == null)
                {
                    context.HttpContext.Response.Headers.Add("TibosFilter", HttpStatusCode.Unauthorized.ToString());
                    json.code = StatusCodeDefine.Unauthorized;
                    json.status = -1;
                    json.msg = "登录超时！";
                    context.Result = new JsonResult(json);
                }
                else
                {
                    _Cache.GetOrCreate(m_manager.Id, entry =>
                    {
                        entry.SetSlidingExpiration(TimeSpan.FromSeconds(15 * 60)); //15分钟
                        return (m_manager);
                    });
                }
                return;
            }
            //登录成功,允许访问首页
            if (MonLog.ControllerName.ToLower() == "home") return;
            //权限验证
            if (m_nav == null)
            {
                context.Result = new ContentResult { Content = @"抱歉,没有找到该操作！" };
                return;
            }
            //获取用户角色(一对多)
            if (string.IsNullOrEmpty(m_manager.RoleId))
            {
                context.Result = new ContentResult { Content = @"抱歉,该用户尚未分配角色！" };
                return;
            }
            var list_roleid = m_manager.RoleId.Split(new char[] { ',' }).ToList();

            //根据actionName获取改操作对应的Mark
            var m_dict = ActionType(MonLog.ActionName);
            foreach (var item in list_roleid)
            {
                var res = _RoleNavDictService.IsExist(m => m.RId == item && m.NId == m_nav.Id && m.DId == m_dict.Id && m.Status == 1);
                if (res) return;
            }
            //没有权限的操作
            if (context.HttpContext.Request.Method.ToLower() == "get")
            {
                context.Result = new ContentResult { Content = @"抱歉,你不具有当前操作的权限！" };
            }
            else
            {
                json.code = StatusCodeDefine.Unauthorized;
                json.status = -1;
                json.msg = "抱歉,你不具有当前操作的权限！";
                context.Result = new JsonResult(json);
            }

            #endregion
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var requestId = context.ActionDescriptor.Id;
            var MonLog = _Cache.Get<MonitorLog>(requestId);
            if (MonLog != null)
            {
                MonLog.ExecuteEndTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff", DateTimeFormatInfo.InvariantInfo));
                var mm = (MonLog.ExecuteEndTime - MonLog.ExecuteStartTime);
                MonLog.TimeConsuming = Convert.ToDecimal((MonLog.ExecuteEndTime - MonLog.ExecuteStartTime).TotalSeconds);
                logger.LogInformation(MonLog.GetLoginfo());
                //插入表
                var m_dict = ActionType(MonLog.ActionName);
                SysLog m_log = new SysLog();
                m_log.Id = Guid.NewGuid().GuidTo16String();
                m_log.MId = MonLog.UID;
                m_log.NId = MonLog.NId;
                m_log.RoleId = m_dict.Id;
                m_log.CreateTime = MonLog.ExecuteEndTime;
                m_log.ExecuteTime = MonLog.TimeConsuming;
                if (context.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    m_log.LoginIp = context.HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                }
                else
                {
                    m_log.LoginIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
                }
                m_log.FromBady = MonLog.BodyCollections;
                m_log.UrlParam = MonLog.QueryCollections.ToString();
                if (m_log.NId != null)
                {
                    _SysLogService.AddAsync(m_log);
                }
            }
        }

        private Dict ActionType(string actionName)
        {
            //获取所有Dict的权限按钮
            var m_dictType = _DictTypeService.Get(m => m.Mark == "Role");
            var list_dict = _DictService.GetList(m => m.Tid == m_dictType.Id);
            foreach (var item in list_dict)
            {
                if (actionName.IndexOf(item.Mark) == 0)
                {
                    return item;
                }
            }
            var model = list_dict.FirstOrDefault(m => m.Mark.ToLower() == "get");
            return model;
        }
    }
}
