using System;
using System.Collections.Generic;
using System.Text;

namespace Tibos.Domain
{
    public class BaseDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int? pageIndex { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int? pageSize { get; set; }
    }
}
