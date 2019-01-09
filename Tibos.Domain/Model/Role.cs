using System;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Domain
{
    //Role
    public class Role
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
        /// 描述
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

    }
}