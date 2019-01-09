using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tibos.Domain;

namespace Tibos.Repository.Dhm
{
    public class DhmDbContext : BaseDbContext
    {
        public DhmDbContext(string connType, string connName) : base(connType, connName)
        {

        }

        public DbSet<Manager> Manager { get; set; }
    }
}
