using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tibos.ConfingModel;
using Tibos.ConfingModel.model;
using Tibos.Domain;
using Tibos.IRepository;
using Tibos.Repository.Dhm;

namespace Tibos.Repository.Tibos
{
    public class DhmRepository<T>: BaseRepository<T>,IBaseRepository<T> where T : BaseEntity
    {
        public DhmRepository()
        {
            BaseDbContext dbContext = new DhmDbContext(base.config.DhmDB.ConnType, base.config.DhmDB.ConnName);
            base.DbContent = dbContext;
            base.Table = this.DbContent.Set<T>();
        }
    }
}
