using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tibos.Domain;
using Tibos.IRepository;

namespace Tibos.Repository.Tibos
{
    public class TibosRepository<T>: BaseRepository<T>,IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// 通过配置,构建上下文
        /// </summary>
        public TibosRepository():base()
        {
            BaseDbContext dbContext = new TibosDbContext(base.config.TibosDB.ConnType, base.config.TibosDB.ConnName);
            base.DbContent = dbContext;
            base.Table = this.DbContent.Set<T>();
        }
    }
}
