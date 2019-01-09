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

    }
}
