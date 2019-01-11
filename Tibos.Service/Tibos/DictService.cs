using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tibos.Domain;
using Tibos.IRepository;
using Tibos.IService.Tibos;

namespace Tibos.Service.Tibos
{
    public class DictService:BaseService<Dict>,IDictService
    {
        private readonly IBaseRepository<Dict> dao;
        public DictService(IBaseRepository<Dict> dao):base(dao)
        {
            this.dao = dao;
        }

        public string GetTest()
        {
            return "666";
        }
    }
}
