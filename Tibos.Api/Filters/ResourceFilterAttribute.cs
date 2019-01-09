using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tibos.Api.Filters
{
    public class ResourceFilterAttribute : Attribute, IResourceFilter
    {
        private readonly ILogger<ResourceFilterAttribute> logger;
 
         public ResourceFilterAttribute(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
         {
             logger = loggerFactory.CreateLogger<ResourceFilterAttribute>();
         }
 
         public void OnResourceExecuted(ResourceExecutedContext context)
         {

         }
 
         public void OnResourceExecuting(ResourceExecutingContext context)
         {

         }
    }
}
