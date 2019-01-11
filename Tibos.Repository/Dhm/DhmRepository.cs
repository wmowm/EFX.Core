using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tibos.Domain;
using Tibos.IRepository;
using Tibos.Repository.Dhm;

namespace Tibos.Repository.Tibos
{
    public class DhmRepository<T>: BaseRepository<T>,IBaseRepository<T> where T : BaseEntity
    {
        public DhmRepository()
        {
            BaseDbContext dbContext = new DhmDbContext("mysql", "server=193.112.104.103;database=dhm;uid=root;pwd=123456;port=3308;Charset=utf8;");
            base.DbContent = dbContext;
            base.Table = this.DbContent.Set<T>();
        }
    }
}
