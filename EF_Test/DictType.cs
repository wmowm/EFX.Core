using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Test
{
    public class DictType:BaseEntity
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
