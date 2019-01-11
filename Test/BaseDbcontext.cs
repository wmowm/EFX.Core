using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using Tibos.Domain;

namespace Test
{
    class BaseDbContext:DbContext
    {
        private string ConnType { get; set; }
        private string ConnName { get; set; }

        public BaseDbContext()
        {
            this.ConnName = "server=193.112.104.103;database=tibos;uid=root;pwd=123456;port=3308;Charset=utf8;";
            this.ConnType = "mysql";
        }

        public DbSet<Dict> Dict { get; set; }
        public DbSet<DictType> DictType { get; set; }

        public BaseDbContext(string connType, string connName)
        {
            this.ConnName = connName;
            this.ConnType = connType;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(opt => opt.Ignore(RelationalEventId.AmbientTransactionWarning));
            switch (ConnType)
            {
                case "mysql":
                    optionsBuilder.UseMySql(ConnName);
                    break;
                case "sqlserver":
                    break;
                case "sqlite":
                    break;
            }
        }


    }
}
