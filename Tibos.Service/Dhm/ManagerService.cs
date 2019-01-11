using System;
using System.Collections.Generic;
using System.Text;
using Tibos.Domain;
using Tibos.IRepository;
using Tibos.IService.Dhm;

namespace Tibos.Service.Dhm
{
    public class ManagerService : BaseService<Manager>,IManagerService
    {
        private readonly IBaseRepository<Manager> dao;
        public ManagerService(IBaseRepository<Manager> dao) : base(dao)
        {
            this.dao = dao;
        }
    }
}
