using System;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
	 	//Dict
	public class DictDto
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
        public virtual int Status
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
	public class DictRequest : Dict
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