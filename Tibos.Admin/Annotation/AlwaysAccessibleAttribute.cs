using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tibos.Admin.Annotation
{
    /// <summary>
    /// 允许匿名访问
    /// </summary>
    public class AlwaysAccessibleAttribute: Attribute
    {
    }
}
