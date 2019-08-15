using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator.Models
{
    [Table(Name = "SqlConnect")]
    public class SqlConnect
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public string Id { get; set; }

        public FreeSql.DataType SqlType { get; set; }

        public string Name { get; set; }



        public string DbName { get; set; }

        public string Address { get; set; }

        public string FullName
        {
            get
            {
                if (!string.IsNullOrEmpty(DbName))
                {
                    return $"{Name}({SqlType})({DbName})";
                }
                else
                {
                    return $"{Name}({SqlType})";
                }
            }
        }
    }
}
