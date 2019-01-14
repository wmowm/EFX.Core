using System;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//Dict
	public class DictDto:BaseDto
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
		/// 类型编号
        /// </summary>
        public virtual string Tid
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 描述
        /// </summary>
        public virtual string Description
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
		/// 状态
        /// </summary>
        public virtual int? Status
        {
            get; 
            set; 
        }
        /// <summary>
        /// 额外字段
        /// </summary>
        public virtual string Extra
        {
            get;
            set;
        }
    }
}