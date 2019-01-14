using System;

//EFX Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//DictType
	public class DictTypeDto:BaseDto
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
    public class zTree
    {
        public string id { get; set; }

        public string pId { get; set; }

        public string name { get; set; }

        public bool open { get; set; }

        public bool noRemoveBtn { get; set; }

        public bool noEditBtn { get; set; }
    }
}