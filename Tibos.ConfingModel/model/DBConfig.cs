using System;
using System.Collections.Generic;
using System.Text;

namespace Tibos.ConfingModel.model
{
    public class DBConfig
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string ConnType { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnName { get; set; }
    }
}
