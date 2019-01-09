using System;
using System.Collections.Generic;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//Navigation
	public class NavigationDto
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
		/// 上级编号
        /// </summary>
        public virtual string ParentId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 菜单等级
        /// </summary>
        public virtual int Level
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 菜单名称
        /// </summary>
        public virtual string Name
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 菜单图标
        /// </summary>
        public virtual string Icon
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 菜单路径
        /// </summary>
        public virtual string Link
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 排序
        /// </summary>
        public virtual int Sort
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 是否为系统菜单
        /// </summary>
        public virtual int IsSys
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 控制器名称
        /// </summary>
        public virtual string ControllerName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 区域
        /// </summary>
        public virtual string Areas
        {
            get; 
            set; 
        }

        public virtual List<DictDto> DictList
        {
            get;
            set;
        }
    }
	public class NavigationRequest : Navigation
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

    public class zTree
    {
        public string id { get; set; }

        public string pId { get; set; }

        public string name { get; set; }

        public bool open { get; set; }

        public bool noRemoveBtn { get; set; }

        public bool noEditBtn { get; set; }
    }
}