using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator.Models
{
    public class TableConfig
    {

        

        public string Id { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; }


        /// <summary>
        /// 库名
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        public List<ColumnConfig> ColumnConfig { get; set; }
    }


}
