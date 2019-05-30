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
            NavigationDto dto = new NavigationDto();
            var res_nav = _NavigationService.GetList(dto);
            var list_navdto = new List<NavigationDto>();
            foreach (var model in (List<Navigation>)res_nav.data)
            {
                var dto_nav = _IMapper.Map<NavigationDto>(model);
                dto_nav.DictList = GetDictRole(model.Id);
                foreach (var item in dto_nav.DictList)
                {
                    item.Status = 0;
                }
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
            NavigationDto request = new NavigationDto();
            var list_nav = _NavigationService.GetList(request);
            var list_navdto = new List<NavigationDto>();
            foreach (var model in (List<Navigation>)list_nav.data)
            {
                var dto = _IMapper.Map<NavigationDto>(model);
                dto.DictList = GetDictRole(model.Id);
                //判断该权限是否被选中
                var rnd_list = _RoleNavDictService.GetList(m => m.RId == Id).ToList();
                foreach (var item in dto.DictList)
                {
                    var m_dict = rnd_list.Find(m => m.DId == item.Id && m.NId == model.Id);
                    item.Status = (int)m_dict.Status;
                    item.Extra = m_dict.Id;
                }
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
            //添加角色权限
            List<RoleNavDict> rnd_list = new List<RoleNavDict>();
            foreach (var item in request.RoleNavDict)
            {
                _RoleNavDictService.Delete(item.Id,false);
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