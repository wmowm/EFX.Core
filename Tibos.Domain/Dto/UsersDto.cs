using System;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//Users
	public class UsersDto
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
		/// UserName
        /// </summary>
        public virtual string UserName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Password
        /// </summary>
        public virtual string Password
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Mobile
        /// </summary>
        public virtual string Mobile
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Email
        /// </summary>
        public virtual string Email
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Status
        /// </summary>
        public virtual int? Status
        {
            get; 
            set; 
        }
        public virtual int? Sex { get; set; }
        /// <summary>
        /// LoginTime
        /// </summary>
        public virtual DateTime? LoginTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// LoginIp
        /// </summary>
        public virtual string LoginIp
        {
            get; 
            set; 
        }        
		   
	}
	public class UsersRequest : Users
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