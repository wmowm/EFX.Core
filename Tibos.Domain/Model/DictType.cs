using System;

//EFX Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 //DictType
	public class DictType:BaseEntity
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
		/// Sort
        /// </summary>
        public virtual int? Sort
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Mark
        /// </summary>
        public virtual string Mark
        {
            get; 
            set; 
        }        
		   
	}
}