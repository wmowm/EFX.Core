using System;

//EFX Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 //Organization
	public class Organization:BaseEntity
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
		/// Name
        /// </summary>
        public virtual string Name
        {
            get; 
            set; 
        }        
		/// <summary>
		/// OrgType
        /// </summary>
        public virtual int? OrgType
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
		   
	}
}