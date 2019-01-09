using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Api.Annotation;
using Tibos.Common;
using Tibos.Service.Contract;

namespace Tibos.Api.Filters
{
    public class ActionFilterAttribute : Attribute, IActionFilter
    {
        
        private readonly ILogger<ActionFilterAttribute> logger;
        private readonly IMemoryCache _Cache;
        private readonly ManagerIService _ManagerService;
        private readonly Token _Token;
        public ActionFilterAttribute(ILoggerFactory loggerFactory, IMemoryCache memoryCache, ManagerIService Managerervice)
        {
            logger = loggerFactory.CreateLogger<ActionFilterAttribute>();
            _Cache = memoryCache;
            _ManagerService = Managerervice;
            _Token = new Token(_Cache);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            #region 记录日志(所有的请求)
            MonitorLog MonLog = new MonitorLog();
            MonLog.ExecuteStartTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff", DateTimeFormatInfo.InvariantInfo));
            MonLog.ControllerName = context.RouteData.Values["controller"] as string;
            MonLog.ActionName = context.RouteData.Values["action"] as string;
            MonLog.QueryCollections = context.HttpContext.Request.QueryString;//Url 参数
            if (string.IsNullOrEmpty(MonLog.QueryCollections.ToString()) && context.ActionArguments.Count != 0)
            {
                try
                {
                    MonLog.BodyCollections = JsonConvert.SerializeObject(context.ActionArguments["dic"]);
                }
                catch
                {

                }
            }
            logger.LogInformation(MonLog.GetLoginfo());
            #endregion

            #region 根据注解允许匿名访问
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerAttributes = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(AlwaysAccessibleAttribute),true);
            if (controllerAttributes != null && controllerAttributes.Length > 0)
            {
                return;
            }
            #endregion

            #region 权限验证
            //1.忽略权限验证的部分(如果要忽略的部分过多,可以提取成方法)
            if (_Token.PassToken(MonLog.ControllerName, MonLog.ActionName)) return;
            //2.根据token获取用户实体对象
            //3.用户->职位->角色->是否具备操作权限

            var token = context.HttpContext.Request.Headers["token"].ToString();
            Json json = _Token.CheckToken(token);
            if (json.status != 0)
            {
                context.Result = new JsonResult(json);
                return;
            }
            #endregion
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
