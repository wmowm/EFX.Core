using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator.Models
{
    [Table(Name = "TemplateConfig")]
    public class TemplateConfig
    {

        [Column(IsIdentity = true, IsPrimary = true)]
        public string Id { get; set; }


        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模板路径
        /// </summary>
        public string TempatePaht { get; set; }

        /// <summary>
        /// 生成文件后缀
        /// </summary>
        public string FileSuffix { get; set; } = ".cs";

        /// <summary>
        /// 生成文件名称(I{TableName}Server)
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 生成文件保存路径
        /// </summary>
        public string FilePath { get; set; }

    }
}
