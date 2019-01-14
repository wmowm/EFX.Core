using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tibos.Domain;
using Tibos.IRepository;

namespace Tibos.Repository.Tibos
{
    public class TibosDbContext : BaseDbContext
    {
        public TibosDbContext(string connType,string connName) : base(connType,connName)
        {

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
