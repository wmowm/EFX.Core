using System;
using System.Collections.Generic;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//Manager
	public class ManagerDto
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
		/// 用户名
        /// </summary>
        public virtual string UserName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 密码
        /// </summary>
        public virtual string Password
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 手机号
        /// </summary>
        public virtual string Mobile
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 邮箱
        /// </summary>
        public virtual string Email
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 状态(0:正常,-1禁用)
        /// </summary>
        public virtual int Status
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 最后登录时间
        /// </summary>
        public virtual DateTime LoginTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 登录IP
        /// </summary>
        public virtual string LoginIp
        {
            get; 
            set; 
        }

        private IList<Role> _roles;
        /// <summary>
        /// 角色列表
        /// </summary>
        public virtual IList<Role> Roles
        {
            set
            {
                _roles = value;
            }
            get
            {
                if (_roles == null) return new List<Role>();
                return _roles;
            }
        }
    }
	public class ManagerRequest : Manager
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