using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibos.Common;
using Tibos.Domain;

//EFX Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Repository.Tibos
{
	public partial class NavigationRoleRepository:TibosRepository<NavigationRole>
	{
        public NavigationRoleRepository()
        {

        }

        /// <summary>
        /// 动态查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public override PageResponse GetList(BaseDto basedto)
        {
            PageResponse response = new PageResponse();
            var dto = (NavigationRoleDto)basedto;
            var query = base.Table.AsQueryable();
            //条件查询
            if (!string.IsNullOrEmpty(dto.NId))
            {
                query = query.Where(p => p.NId == dto.NId);
            }
            if (dto.Status.HasValue)
            {
                query = query.Where(p => p.Status == dto.Status);
            }
            response.total = query.Count();
            if (query.Count() > 0)
            {
                if (dto.pageIndex.HasValue && dto.pageSize.HasValue)
                {
                    query = query.Skip((dto.pageIndex.Value - 1) * dto.pageSize.Value).Take(dto.pageSize.Value);
                }
                //根据参数进行排序
                //query = query.OrderBy(p => p.Sort);
            }
            response.status = 0;
            response.code = StatusCodeDefine.Success;
            response.data = query.ToList();
            return response;
        }

    }
}