using System;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//Organization
		public class Organization
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
		/// 类型(1.区域,2.公司,3.组)
        /// </summary>
        public virtual int OrgType
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
		   
	}
}