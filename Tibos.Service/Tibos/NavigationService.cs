using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tibos.Common;
using Tibos.Domain;
using Tibos.IRepository;
using Tibos.IService.Tibos;


//EFX Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Service.Tibos
{
    public class NavigationService : BaseService<Navigation>, INavigationService
    {
        private readonly IBaseRepository<Navigation> dao;

        private readonly IMemoryCache _Cache;
        private readonly IRoleNavDictService _RoleNavDictService;

        public NavigationService(IBaseRepository<Navigation> dao,IMemoryCache memoryCache, IRoleNavDictService RoleNavDictService) : base(dao)
        {
            this.dao = dao;
            this._Cache = memoryCache;
            this._RoleNavDictService = RoleNavDictService;
        }

        /// <summary>
        /// 获取后台用户菜单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Navigation> GetList(string userId)
        {
            var m_manager = _Cache.Get<Manager>(userId);
            var list_roleid = m_manager.RoleId.Split(new char[] { ',' }).ToList();
            List<RoleNavDict> list_rnd = new List<RoleNavDict>();
            foreach (var item in list_roleid)
            {
                var list = _RoleNavDictService.GetList(m => m.RId == item && m.Status == 1).ToList();
                list_rnd = list_rnd.Union(list).ToList(); //并集
            }
            list_rnd = list_rnd.Distinct().ToList();

            //菜单列表
            var nav_list = dao.GetList().Where(p=> list_rnd.Select(m=>m.NId).Contains(p.Id)).ToList();
            return nav_list;
        }
    }
}