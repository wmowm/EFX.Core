using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Test
{
    public class DhmDbContext:BaseDbContext
    {
        public static string ConnName = "server=193.112.104.103;database=dhm;uid=root;pwd=123456;port=3308;Charset=utf8;";
        public DhmDbContext() : base("mysql", ConnName)
        {

        }

        public DbSet<Manager> Manager { get; set; }
    }
}
