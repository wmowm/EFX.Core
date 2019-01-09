using System;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 //DictType
	public class DictType
	{
      	/// <summary>
		/// 编号
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
		/// 排序
        /// </summary>
        public virtual int Sort
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 标记
        /// </summary>
        public virtual string Mark
        {
            get; 
            set; 
        }        
		   
	}
}