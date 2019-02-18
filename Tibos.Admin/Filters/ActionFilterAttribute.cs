using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
        private readonly Token _Token;
        public ActionFilterAttribute(ILoggerFactory loggerFactory, IMemoryCache memoryCache, IManagerService Managerervice)
        {
            logger = loggerFactory.CreateLogger<ActionFilterAttribute>();
            _Cache = memoryCache;
            _ManagerService = Managerervice;
            _Token = new Token(_Cache);
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
            #region 记录日志(所有的请求)
            MonitorLog MonLog = new MonitorLog();
            MonLog.RequestID = requestId;
            MonLog.UID = context.HttpContext.User.Identity.Name;
            MonLog.ExecuteStartTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff", DateTimeFormatInfo.InvariantInfo));
            MonLog.ControllerName = context.RouteData.Values["controller"] as string;
            MonLog.ActionName = context.RouteData.Values["action"] as string;
            MonLog.QueryCollections = context.HttpContext.Request.QueryString;//Url 参数
            MonLog.BodyCollections = parms; //Form参数
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
            var actionAttributes = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(AlwaysAccessibleAttribute),true);
            if (actionAttributes != null && actionAttributes.Length > 0)
            {
                return;
            }
            #endregion

            #region 权限验证
            //1.获取用户实体对象
            //2.用户->职位->角色->是否具备操作权限
            PageResponse json = new PageResponse();
            var userId = context.HttpContext.User.Identity.Name;
            //未登录
            if(userId == null)
            {
                context.HttpContext.Response.Headers.Add("TibosFilter", HttpStatusCode.Unauthorized.ToString());
                json.code = StatusCodeDefine.Unauthorized;
                json.status = -1;
                json.msg = "登录超时！";
                context.Result = new JsonResult(json);
                return;
            }
            //获取用户对象
            var m_manager = _Cache.Get<Manager>(userId);
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
                    //添加到缓存
                    _Cache.GetOrCreate(m_manager.Id, entry =>
                    {
                        entry.SetSlidingExpiration(TimeSpan.FromSeconds(15 * 60)); //15分钟
                    return (m_manager);
                    });
                }
                return;
            }
            //权限验证
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
                MonLog.TimeConsuming = (MonLog.ExecuteEndTime - MonLog.ExecuteStartTime).TotalSeconds;
                logger.LogInformation(MonLog.GetLoginfo());
            }
        }
    }
}
