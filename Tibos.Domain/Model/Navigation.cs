using System;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//Navigation
		public class Navigation
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
        public virtual int? Level
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
		   
	}
}