using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF_Test
{
    public class BaseDbContext:DbContext
    {
        private string ConnType { get; set; }
        private string ConnName { get; set; }

        public BaseDbContext()
        {

        }

        public BaseDbContext(string connType,string connName)
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
                    optionsBuilder.UseSqlServer(ConnName);
                    break;
                case "sqlite":
                    break;
            }
        }
    }
}
