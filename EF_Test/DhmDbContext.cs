using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Test
{
    public class DhmDbContext:BaseDbContext
    {
        public static string ConnName = "server=132.232.4.73;database=dhm;uid=root;pwd=As123456;port=3307;Charset=utf8;";
        public DhmDbContext() : base("mysql", ConnName)
        {

        }

        public DbSet<Manager> Manager { get; set; }
    }
}
