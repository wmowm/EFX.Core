using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Test
{
    public class TibosDbContext : BaseDbContext
    {
        public static string ConnName = "server=47.104.247.70;database=tibos;uid=root;pwd=Root123.;port=3306;Charset=utf8;";
        //public static string ConnName = "server=.;database=test001;uid=sa;pwd=123456;";
        public TibosDbContext() : base("mysql", ConnName)
        {

        }

        public DbSet<Dict> Dict { get; set; }
        public DbSet<DictType> DictType { get; set; }

        public DbSet<Manager> Manager { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
