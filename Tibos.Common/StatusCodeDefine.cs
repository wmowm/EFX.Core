using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Tibos.Common
{
    public enum StatusCodeDefine
    {
        /// <summary>
        /// 处理成功
        /// </summary>
        [Description("处理成功")]
        Success = 200,


        /// <summary>
        /// 未授权
        /// </summary>
        [Description("处理成功")]
        Unauthorized = 401,
    }
}
