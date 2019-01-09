using System;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//NavigationRole
	public class NavigationRoleDto
	{
	
      	/// <summary>
		/// 编号
        /// </summary>
        public virtual string Id
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 菜单编号
        /// </summary>
        public virtual string NId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 字典编号
        /// </summary>
        public virtual string DId
        {
            get; 
            set; 
        }
        public virtual int Status { get; set; }

    }
	public class NavigationRoleRequest : NavigationRole
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