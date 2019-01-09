using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Common;
using Tibos.Service.Contract;

namespace Tibos.Web.Filters
{
    public class ActionFilterAttribute : Attribute, IActionFilter
    {
        

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
