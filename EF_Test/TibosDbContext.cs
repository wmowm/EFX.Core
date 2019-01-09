using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Test
{
    public class TibosDbContext:BaseDbContext
    {
        public static string ConnName = "server=193.112.104.103;database=tibos;uid=root;pwd=123456;port=3308;Charset=utf8;";
        public TibosDbContext():base("mysql", ConnName)
        {

        }

        public DbSet<Dict> Dict { get; set; }
        public DbSet<DictType> DictType { get; set; }
    }
}
