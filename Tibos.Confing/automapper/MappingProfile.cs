using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Domain;

namespace Tibos.Confing.automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Manager, ManagerDto>();
            CreateMap<Dict, DictDto>();
            CreateMap<Navigation, NavigationDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<Manager, ManagerDto>();
        }
    }
}
