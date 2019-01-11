using System;

//EFX Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 //Position
	public class Position:BaseEntity
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
		/// OrgId
        /// </summary>
        public virtual string OrgId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// RoleIds
        /// </summary>
        public virtual string RoleIds
        {
            get; 
            set; 
        }        
		   
	}
}