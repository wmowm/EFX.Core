using System;

//EFX Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//SysLog
	public class SysLogDto:BaseDto
	{
	
      	/// <summary>
		/// 主键标识
        /// </summary>
        public virtual string Id
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 管理员编号
        /// </summary>
        public virtual string MId
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
		/// 权限编号
        /// </summary>
        public virtual string RoleId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 登陆IP
        /// </summary>
        public virtual string LoginIp
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 创建时间
        /// </summary>
        public virtual DateTime? CreateTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 执行时间
        /// </summary>
        public virtual decimal? ExecuteTime
        {
            get; 
            set; 
        }
        public string FromBady { get; set; }

        public string UrlParam { get; set; }

        public string ManagerName { get; set; }

        public string DictName { get; set; }

        public string NavName { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
	
}