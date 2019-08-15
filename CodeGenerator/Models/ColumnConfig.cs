using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator.Models
{
    public class ColumnConfig
    {
        /// <summary>
        /// 列名前缀
        /// </summary>
        public string ColumnNamePrefix { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropName
        {
            get
            {
                if (!string.IsNullOrEmpty(ColumnNamePrefix))
                {
                    var i = ColumnNamePrefix.Length > ColumnName.Length ? 0 : ColumnNamePrefix.Length - 1;
                    return ColumnName.Substring(i);
                }
                return ColumnName;
            }
        }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 类型
        /// </summary>

        private string _csType;
        public string CsType
        {
            get
            {
                return _csType;
            }
            set
            {
                _csType = value;
            }
        }
    }
}
