using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Domain;

namespace Tibos.Api.Annotation
{
    public class Dict : BaseEntity
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
        /// 标记
        /// </summary>
        public virtual string Mark
        {
            get;
            set;
        }
    }
}
