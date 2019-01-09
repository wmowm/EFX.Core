using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Tibos.Common
{

    [Serializable]
    public class SearchTemplate
    {
        /// <summary>
        /// 要查询的属性(对应Model里的属性)
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 要查询的属性的值(对应Model里的属性得值)
        /// </summary>
        public object value { get; set; }
        /// <summary>
        /// 查询类型(>,=,In.....)
        /// </summary>
        public Common.EnumBase.SearchType searchType { get; set; }

    }

    [Serializable]
    public class SortOrder 
    {
        /// <summary>
        /// 排序方式(Asc,Desc)
        /// </summary>
        public Common.EnumBase.OrderType searchType { get; set; }

        /// <summary>
        /// 要排序的属性(对应Model里的属性)
        /// </summary>
        public string value { get; set; }
    }
    /// <summary>
    /// 分页
    /// </summary>
    [Serializable]
    public class Pagination
    {
        public int pageSize { get; set; }

        public int pageIndex { get; set; }
    }



































    [Serializable]
    public class SortOrder<T>
    {
        /// <summary>
        /// 要排序的属性(对应Model里的属性)
        /// </summary>
        public Expression<Func<T, object>> value { get; set; }

        /// <summary>
        /// 排序方式(Asc,Desc)
        /// </summary>
        public Common.EnumBase.OrderType searchType { get; set; }
    }
}