using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tibos.Domain;

namespace Tibos.IRepository
{
    public interface IDhmRepository<T>:IBaseRepository<T> where T : BaseEntity
    {
        
    }
}
