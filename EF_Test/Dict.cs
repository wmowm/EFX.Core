using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EF_Test
{
    public class Dict:BaseEntity
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
        [MaxLength(200)]
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

    }
}
