using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tibos.Common;
using Tibos.Domain;
using Tibos.Service.Contract;

namespace Tibos.Admin.Areas.SYS.Controllers
{
    [Area("SYS")]
    public class RoleController : Controller
    {
        public RoleIService _RoleIService { get; set; }

        public RoleNavDictIService _RoleNavDictIService { get; set; }
        public NavigationIService _NavigationIService { get; set; }

        public NavigationRoleIService _NavigationRoleIService { get; set; }

        public DictTypeIService _DictTypeIService { get; set; }

        public DictIService _DictIService { get; set; }

        public IMapper _IMapper { get; set; }

        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Create()
        {
            NavigationRequest request = new NavigationRequest();
            request.sortKey = "Sort";
            request.sortType = 0;
            var list_nav = _NavigationIService.GetList(request);
            var list_navdto = new List<NavigationDto>();
            foreach (var model in list_nav)
            {
                var dto = _IMapper.Map<NavigationDto>(model);
                dto.DictList = GetDictRole(model.Id);
                foreach (var item in dto.DictList)
                {
                    item.Status = 0;
                }
                list_navdto.Add(dto);
            }
            ViewData["list_navdto"] = list_navdto;
            return View();
        }
        public IActionResult Edit(string Id)
        {
            var m_role = new Role();
            if (!string.IsNullOrEmpty(Id))
            {
                m_role = _RoleIService.Get(Id);
            }
            NavigationRequest request = new NavigationRequest();
            request.sortKey = "Sort";
            request.sortType = 0;
            var list_nav = _NavigationIService.GetList(request);
            var list_navdto = new List<NavigationDto>();
            foreach (var model in list_nav)
            {
                var dto = _IMapper.Map<NavigationDto>(model);
                dto.DictList = GetDictRole(model.Id);
                //判断该权限是否被选中
                var rnd_list = _RoleNavDictIService.GetList(m => m.RId == Id, null, null).ToList();
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
        public JsonResult List(RoleRequest request)
        {

            var list = _RoleIService.GetList(request);
            var count = _RoleIService.GetCount(request);
            Json reponse = new Json();
            reponse.code = 200;
            reponse.total = count;
            reponse.data = list;
            return Json(reponse);
        }


        [HttpPost]
        public JsonResult Create(RoleDto request)
        {
            Role model = _IMapper.Map<Role>(request);
            model.Id = Guid.NewGuid().GuidTo16String();
            var id = _RoleIService.Save(model);
            //添加角色权限
            foreach (var item in request.RoleNavDict)
            {
                item.Id = Guid.NewGuid().GuidTo16String();
                item.RId = model.Id;
                _RoleNavDictIService.Save(item);
            }
            Json reponse = new Json();
            reponse.code = 200;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Edit(RoleDto request)
        {
            Json reponse = new Json();
            Role model = _IMapper.Map<Role>(request);
            _RoleIService.Update(model);
            //添加角色权限
            foreach (var item in request.RoleNavDict)
            {
                _RoleNavDictIService.Delete(item.Id);
                item.Id = Guid.NewGuid().GuidTo16String();
                item.RId = model.Id;
                _RoleNavDictIService.Save(item);
            }
            reponse.code = 200;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            //删除该角色所有的权限
            var list_rnd = _RoleNavDictIService.GetList(m => m.RId == Id, null, null);
            foreach (var item in list_rnd)
            {
                _RoleNavDictIService.Delete(item.Id);
            }
            _RoleIService.Delete(Id);
            Json reponse = new Json();
            reponse.code = 200;
            return Json(reponse);
        }


        private List<DictDto> GetDictRole(string NId)
        {
            List<SortOrder<Dict>> expressionOrder = new List<SortOrder<Dict>>()
            {
                new SortOrder<Dict>() { value = m => m.Sort, searchType = EnumBase.OrderType.Asc }
            };

            var list = _NavigationRoleIService.GetList(m => m.NId == NId && m.Status == 1, null, null);
            var dictType = _DictTypeIService.GetList(m => m.Mark == "Role", null, null).FirstOrDefault();
            var dict = _DictIService.GetList(m => m.Tid == dictType.Id && m.Status == 1, expressionOrder, null).ToList();
            List<DictDto> res = new List<DictDto>();
            foreach (var item in list)
            {
                var model = dict.Find(m => m.Id == item.DId);
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