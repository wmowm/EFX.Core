using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using Tibos.Domain;

namespace EFMigration
{
    public class BaseDbContext : DbContext
    {
        private string ConnType { get; set; }
        private string ConnName { get; set; }

        public BaseDbContext()
        {
            this.ConnName =  Config.connName;
            this.ConnType = Config.connType;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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



        public DbSet<Dict> Dict { get; set; }
        public DbSet<DictType> DictType { get; set; }

        public DbSet<Manager> Manager { get; set; }
        public DbSet<Navigation> Navigation { get; set; }
        public DbSet<NavigationRole> NavigationRole { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Role> Role { get; set; }

        public DbSet<RoleNavDict> RoleNavDict { get; set; }

        public DbSet<Users> Users { get; set; }
    }
}
