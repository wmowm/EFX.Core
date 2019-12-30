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
                switch (_csType)
                {
                    case "System.Boolean":
                        _csType = "bool";
                        break;
                    case "System.Byte":
                        _csType = "byte";
                        break;
                    case "System.SByte":
                        _csType = "bool";
                        break;
                    case "System.Char":
                        _csType = "char";
                        break;
                    case "System.Decimal":
                        _csType = "decimal";
                        break;
                    case "System.Double":
                        _csType = "double";
                        break;
                    case "System.Single":
                        _csType = "float";
                        break;
                    case "System.Int32":
                        _csType = "int";
                        break;
                    case "System.UInt32":
                        _csType = "uint";
                        break;
                    case "System.Int64":
                        _csType = "ulong";
                        break;
                    case "System.Object":
                        _csType = "object";
                        break;
                    case "System.Int16":
                        _csType = "short";
                        break;
                    case "System.UInt16":
                        _csType = "ushort";
                        break;
                    case "System.String":
                        _csType = "string";
                        break;
                    default:
                        break;
                }
                return _csType;
            }
            set
            {
                _csType = value;
            }
        }
    }
}
