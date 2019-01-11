using System;

//EFX Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//Navigation
	public class NavigationDto
	{
	
      	/// <summary>
		/// Id
        /// </summary>
        public virtual string Id
        {
            get; 
            set; 
        }        
		/// <summary>
		/// ParentId
        /// </summary>
        public virtual string ParentId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Level
        /// </summary>
        public virtual int? Level
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Name
        /// </summary>
        public virtual string Name
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Icon
        /// </summary>
        public virtual string Icon
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Link
        /// </summary>
        public virtual string Link
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Sort
        /// </summary>
        public virtual int? Sort
        {
            get; 
            set; 
        }        
		/// <summary>
		/// IsSys
        /// </summary>
        public virtual int? IsSys
        {
            get; 
            set; 
        }        
		/// <summary>
		/// ControllerName
        /// </summary>
        public virtual string ControllerName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Areas
        /// </summary>
        public virtual string Areas
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
}