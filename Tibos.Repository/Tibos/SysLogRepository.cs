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
	public partial class SysLogRepository:TibosRepository<SysLog>
	{
        public SysLogRepository()
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
            var dto = (SysLogDto)basedto;
            var query = base.Table.AsQueryable();
            //条件查询
            if (dto.StartTime.HasValue)
            {
                query = query.Where(p => p.CreateTime >= dto.StartTime);
            }
            if (dto.EndTime.HasValue)
            {
                query = query.Where(p => p.CreateTime <= dto.EndTime);
            }
            if (!string.IsNullOrEmpty(dto.RoleId))
            {
                query = query.Where(p => p.RoleId == dto.RoleId);
            }
            var db = (TibosDbContext)base.DbContent;
            var queryList = from m in query
                      join j in db.Manager on m.MId equals j.Id
                      join n in db.Navigation on m.NId equals n.Id
                      join d in db.Dict on m.RoleId equals d.Id
                      select new SysLogDto
                      {
                          CreateTime = m.CreateTime,
                          ExecuteTime = m.ExecuteTime,
                          FromBady = m.FromBady,
                          Id = m.Id,
                          LoginIp = m.LoginIp,
                          MId = m.MId,
                          ManagerName = j.UserName,
                          NId = m.NId,
                          RoleId = m.RoleId,
                          UrlParam = m.UrlParam,
                          DictName = d.Name,
                          NavName = n.Name
                      };
            response.total = queryList.Count();
            if (response.total > 0)
            {
                if (dto.pageIndex.HasValue && dto.pageSize.HasValue)
                {
                    queryList = queryList.Skip((dto.pageIndex.Value - 1) * dto.pageSize.Value).Take(dto.pageSize.Value);
                }
                //根据参数进行排序
                queryList = queryList.OrderByDescending(p => p.CreateTime);
            }
            response.status = 0;
            response.code = StatusCodeDefine.Success;
            response.data = queryList.ToList();
            return response;
        }

    }
}