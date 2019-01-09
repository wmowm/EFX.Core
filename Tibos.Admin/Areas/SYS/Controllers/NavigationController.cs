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
    public class NavigationController : Controller
    {
        public NavigationIService _NavigationIService { get; set; }
        public DictTypeIService _DictTypeIService { get; set; }

        public DictIService _DictIService { get; set; }

        public NavigationRoleIService _NavigationRoleIService { get; set; }

        public IMapper _IMapper { get; set; }
        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Create(string Id)
        {
            var model = new Navigation();
            if (!string.IsNullOrEmpty(Id))
            {
               model = _NavigationIService.Get(Id);
            }
            var dto = _IMapper.Map<NavigationDto>(model);
            dto.DictList = GetDictRole();
            foreach (var item in dto.DictList)
            {
                item.Status = 0;
            }
            return View(dto);
        }
        public IActionResult Edit(string Id)
        {
            var model = new Navigation();
            if (!string.IsNullOrEmpty(Id))
            {
                model = _NavigationIService.Get(Id);
            }
            var dto = _IMapper.Map<NavigationDto>(model);
            dto.DictList = GetDictRole();
            //获取菜单下所有选中的权限按钮
            var nr_list = _NavigationRoleIService.GetList(m => m.NId == dto.Id && m.Status == 1, null, null).ToList();
            foreach (var item in dto.DictList)
            {
                if(nr_list.Find(m=>m.DId == item.Id) != null)
                {
                    item.Status = 1;
                }
                else
                {
                    item.Status = 0;
                }
            }
            return View(dto);
        }

        public IActionResult Detail()
        {
            return View();
        }


        public ActionResult GetFont()
        {
            return PartialView("~/Areas/SYS/Views/Navigation/_Font.cshtml");
        }


        [HttpPost]
        public JsonResult ListTree(NavigationRequest request)
        {
            request.sortKey = "Sort";
            request.sortType = 0;
            var list = _NavigationIService.GetList(request);
            var count = _NavigationIService.GetCount(request);
            List<zTree> list_ztree = new List<zTree>();
            zTree ztree = new zTree()
            {
                id = "0",
                pId = "#",
                name = "系统",
                noEditBtn = true,
                noRemoveBtn = true,
                open = true
            };
            list_ztree.Add(ztree);
            foreach (var item in list)
            {
                ztree = new zTree()
                {
                    id = item.Id,
                    pId = item.ParentId ?? "0",
                    name = item.Name,
                    open = true
                };
                if (item.IsSys == 1)
                {
                    ztree.noEditBtn = true;
                    ztree.noRemoveBtn = true;
                }
                list_ztree.Add(ztree);
            }

            Json reponse = new Json();
            reponse.code = 200;
            reponse.total = count;
            reponse.data = list_ztree;
            return Json(reponse);
        }



        [HttpPost]
        public JsonResult List(NavigationRequest request)
        {
            request.sortKey = "Sort";
            request.sortType = 0;
            request.Level = 1;
            var list = _NavigationIService.GetList(request);

            List<Navigation> nav_list = new List<Navigation>();

            foreach (var item in list)
            {
                nav_list.Add(item);
                request.Level = 2;
                request.ParentId = item.Id;
                var sub_list = _NavigationIService.GetList(request);
                nav_list.AddRange(sub_list);
            }



            var dto_list = _IMapper.Map<List<NavigationDto>>(nav_list);
            foreach (var item in dto_list)
            {
                item.DictList = GetDictRole();
                var nr_list = _NavigationRoleIService.GetList(m => m.NId == item.Id && m.Status == 1, null, null).ToList();
                foreach (var it in item.DictList)
                {
                    if (nr_list.Find(m => m.DId == it.Id) != null)
                    {
                        it.Status = 1;
                    }
                    else
                    {
                        it.Status = 0;
                    }
                }
            }

            Json reponse = new Json();
            reponse.code = 200;
            reponse.total = nav_list.Count;
            reponse.data = dto_list;
            return Json(reponse);
        }


        [HttpPost]
        public JsonResult Create(NavigationDto request)
        {
            Navigation model = new Navigation()
            {
                Areas = request.Areas,
                ControllerName = request.ControllerName,
                Icon = request.Icon,
                Id = Guid.NewGuid().GuidTo16String(),
                IsSys = request.IsSys,
                Link = request.Link,
                Name = request.Name,
                ParentId = request.ParentId,
                Sort = request.Sort,
                Level = request.Level
            };
            var id = _NavigationIService.Save(model);
            //新增菜单权限
            foreach (var item in request.DictList)
            {
                NavigationRole m_nr = new NavigationRole()
                {
                    Id = Guid.NewGuid().GuidTo16String(),
                    DId = item.Id,
                    NId = model.Id,
                    Status = item.Status
                };
                _NavigationRoleIService.Save(m_nr);
            }

            zTree ztree = new zTree()
            {
                id = model.Id,
                pId = model.ParentId ?? "0",
                name = model.Name,
                open = true
            };
            if (model.IsSys == 1)
            {
                ztree.noEditBtn = true;
                ztree.noRemoveBtn = true;
            }

            Json reponse = new Json();
            reponse.code = 200;
            reponse.status = 0;
            reponse.data = ztree;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Edit(NavigationDto request)
        {
            Json reponse = new Json();
            Navigation model = new Navigation()
            {
                Areas = request.Areas,
                ControllerName = request.ControllerName,
                Icon = request.Icon,
                Id = request.Id,
                IsSys = request.IsSys,
                Link = request.Link,
                Name = request.Name,
                ParentId = request.ParentId,
                Sort = request.Sort,
                Level = request.Level
            };
            //删除该菜单下,所有的权限按钮
            var list_role = _NavigationRoleIService.GetList(m => m.NId == model.Id, null, null).ToList();
            foreach (var item in list_role)
            {
                _NavigationRoleIService.Delete(item.Id);
            }
            //新增菜单权限
            foreach (var item in request.DictList)
            {
                NavigationRole m_nr = new NavigationRole()
                {
                    Id = Guid.NewGuid().GuidTo16String(),
                    DId = item.Id,
                    NId = model.Id,
                    Status = item.Status
                };
                _NavigationRoleIService.Save(m_nr);
            }
            _NavigationIService.Update(model);

            zTree ztree = new zTree()
            {
                id = model.Id,
                pId = model.ParentId ?? "0",
                name = model.Name,
                open = true
            };
            if (model.IsSys == 1)
            {
                ztree.noEditBtn = true;
                ztree.noRemoveBtn = true;
            }

            //获取菜单权限字典


            reponse.code = 200;
            reponse.status = 0;
            reponse.data = ztree;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            _NavigationIService.Delete(Id);
            Json reponse = new Json();
            reponse.code = 200;
            return Json(reponse);
        }


        private List<DictDto> GetDictRole()
        {
            List<SortOrder<Dict>> expressionOrder = new List<SortOrder<Dict>>()
            {
                new SortOrder<Dict>() { value = m => m.Sort, searchType = EnumBase.OrderType.Asc }
            };
            var dictType = _DictTypeIService.GetList(m => m.Mark == "Role", null, null).FirstOrDefault();
            var dict = _DictIService.GetList(m => m.Tid == dictType.Id && m.Status == 1, expressionOrder, null).ToList();
            var res = _IMapper.Map<List<DictDto>>(dict);
            return res;
        }
    }
}