using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tibos.Web.Filters
{
    public class ResultFilterAttribute : Attribute, IResultFilter
    {
        private readonly ILogger<ResultFilterAttribute> logger;
 
         public ResultFilterAttribute(ILoggerFactory loggerFactory)
         {
             logger = loggerFactory.CreateLogger<ResultFilterAttribute>();
         }
 
         public void OnResultExecuted(ResultExecutedContext context)
         {

         }
 
         public void OnResultExecuting(ResultExecutingContext context)
         {

         }
    }
}
