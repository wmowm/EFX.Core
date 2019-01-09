using System;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//Position
		public class Position
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
		/// 组织机构编号
        /// </summary>
        public virtual string OrgId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Position
        /// </summary>
        public virtual string RoleIds
        {
            get; 
            set; 
        }        
		   
	}
}