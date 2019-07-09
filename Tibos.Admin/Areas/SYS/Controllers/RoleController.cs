using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tibos.Common;
using Tibos.Domain;
using Tibos.IService.Tibos;

namespace Tibos.Admin.Areas.SYS.Controllers
{
    [Area("SYS")]
    public class RoleController : Controller
    {
        public IRoleService _RoleService { get; set; }

        public IRoleNavDictService _RoleNavDictService { get; set; }
        public INavigationService _NavigationService { get; set; }

        public INavigationRoleService _NavigationRoleService { get; set; }

        public IDictTypeService _DictTypeService { get; set; }

        public IDictService _DictService { get; set; }

        public IMapper _IMapper { get; set; }

        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Create()
        {

            var respose = _NavigationRoleService.GetList(new NavigationRoleDto() { Status = 1 });
            var AllNRList = respose.data as List<NavigationRoleDto>;
            NavigationDto dto = new NavigationDto();
            var res_nav = _NavigationService.GetList(dto);
            var list_navdto = new List<NavigationDto>();
            foreach (var model in (List<Navigation>)res_nav.data)
            {
                var dto_nav = _IMapper.Map<NavigationDto>(model);
                dto_nav.NRList = AllNRList.Where(m => m.NId == model.Id).ToList();
                dto_nav.NRList.ForEach(m => { m.Status = 0; });
                list_navdto.Add(dto_nav);
            }
            ViewData["list_navdto"] = list_navdto;
            return View();
        }
        public IActionResult Edit(string Id)
        {
            var m_role = new Role();
            if (!string.IsNullOrEmpty(Id))
            {
                m_role = _RoleService.Get(Id);
            }
            //获取所有的角色按钮权限
            var list_rnd =_RoleNavDictService.GetList(m => m.RId == Id && m.Status == 1);
            var respose = _NavigationRoleService.GetList(new NavigationRoleDto() { Status = 1 });
            var AllNRList = respose.data as List<NavigationRoleDto>;
            NavigationDto request = new NavigationDto();
            var list_nav = _NavigationService.GetList(request);
            var list_navdto = new List<NavigationDto>();
            foreach (var model in (List<Navigation>)list_nav.data)
            {
                var dto = _IMapper.Map<NavigationDto>(model);
                dto.NRList = AllNRList.Where(m => m.NId == model.Id).ToList();
                dto.NRList.ForEach(m =>
                {
                    if (m.NavName == "广告弹窗")
                    {

                    }
                    var index = list_rnd.FindIndex(p => p.NId == m.NId && p.DId == m.DId);
                    if (index > -1)
                    {
                        m.Status = 1;
                    }
                    else
                    {
                        m.Status = 0;
                    }
                });
                list_navdto.Add(dto);
            }
            ViewData["list_navdto"] = list_navdto;
            return View(m_role);
        }

        public IActionResult Detail()
        {
            return View();
        }


        public ActionResult GetFont()
        {
            return PartialView("~/Areas/SYS/Views/Role/_Font.cshtml");
        }





        [HttpPost]
        public JsonResult List(RoleDto dto)
        {

            var response = _RoleService.GetList(dto);
            return Json(response);
        }


        [HttpPost]
        public JsonResult Create(RoleDto request)
        {
            Role model = _IMapper.Map<Role>(request);
            model.Id = Guid.NewGuid().GuidTo16String();
            var id = _RoleService.Add(model,false);
            //添加角色权限
            List<RoleNavDict> rnd_list = new List<RoleNavDict>();
            foreach (var item in request.RoleNavDict)
            {
                item.Id = Guid.NewGuid().GuidTo16String();
                item.RId = model.Id;
                rnd_list.Add(item);
            }
            _RoleNavDictService.Add(rnd_list);
            _RoleService.SaveChanges();
            PageResponse response = new PageResponse();
            response.code = StatusCodeDefine.Success;
            response.status = 0;
            return Json(response);
        }

        [HttpPost]
        public JsonResult Edit(RoleDto request)
        {
            PageResponse response = new PageResponse();
            Role model = _IMapper.Map<Role>(request);
            _RoleService.Update(model, false);
            //删除角色所有权限
            var list_rnd = _RoleNavDictService.GetList(m => m.RId == request.Id);
            _RoleNavDictService.Delete(list_rnd, false);
            //添加角色权限
            List<RoleNavDict> rnd_list = new List<RoleNavDict>();
            foreach (var item in request.RoleNavDict)
            {
                item.Id = Guid.NewGuid().GuidTo16String();
                item.RId = model.Id;
                rnd_list.Add(item);
            }
            _RoleNavDictService.Add(rnd_list);
            _RoleService.SaveChanges(); //不同模块,必须单独提交事务
            response.code = StatusCodeDefine.Success;
            response.status = 0;
            return Json(response);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            PageResponse response = new PageResponse();
            //删除该角色所有的权限
            var list_rnd = _RoleNavDictService.GetList(m => m.RId == Id);
            List<RoleNavDict> rnd_list = new List<RoleNavDict>();
            foreach (var item in list_rnd)
            {
                rnd_list.Add(item);
            }
            _RoleNavDictService.Delete(rnd_list);
            _RoleService.Delete(Id);
            response.code = StatusCodeDefine.Success;
            response.status = 0;
            return Json(response);
        }


        private List<DictDto> GetDictRole(string NId)
        {
            List<SortOrder<Dict>> expressionOrder = new List<SortOrder<Dict>>()
            {
                new SortOrder<Dict>() { value = m => m.Sort, searchType = EnumBase.OrderType.Asc }
            };

            var list = _NavigationRoleService.GetList(m => m.NId == NId && m.Status == 1);
            var dictType = _DictTypeService.GetList(m => m.Mark == "Role").FirstOrDefault();
            var dict_list = _DictService.GetList(new DictDto() { Tid = dictType.Id, Status = 1 });
            List<DictDto> res = new List<DictDto>();
            foreach (var item in list)
            {
                var temp = (List<Dict>)dict_list.data;
                var model = temp.Find(m => m.Id == item.DId);
                if (model != null)
                {
                   var dto =  _IMapper.Map<DictDto>(model);
                    res.Add(dto);
                }
            }
            return res.OrderBy(m=>m.Sort).ToList();
        }

    }
}