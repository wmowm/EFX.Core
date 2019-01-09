using System;
using System.Collections.Generic;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//Role
	public class RoleDto
	{
	
      	/// <summary>
		/// 主键编号
        /// </summary>
        public virtual string Id
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 名称
        /// </summary>
        public virtual string Name
        {
            get; 
            set; 
        }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        public virtual List<RoleNavDict> RoleNavDict
        {
            get;
            set;
        }
    }
	public class RoleRequest : Role
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 需要排序的值
        /// </summary>
        public string sortKey { get; set; }

        /// <summary>
        /// 排序方式 0:正序,1倒序
        /// </summary>
        public int sortType { get; set; }
    }
}