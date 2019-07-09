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
    public class NavigationController : Controller
    {
        public INavigationService _NavigationService { get; set; }
        public IDictTypeService _DictTypeService { get; set; }

        public IDictService _DictService { get; set; }

        public INavigationRoleService _NavigationRoleService { get; set; }

        public IMapper _IMapper { get; set; }
        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Create(string Id)
        {
            var model = new Navigation();
            if (!string.IsNullOrEmpty(Id) && Id != "0")
            {
               model = _NavigationService.Get(Id);
            }
            var dto = _IMapper.Map<NavigationDto>(model);
            //dto.DictList = GetDictRole();
            //foreach (var item in dto.DictList)
            //{
            //    item.Status = 0;
            //}
            return View(dto);
        }
        public IActionResult Edit(string Id)
        {
            var model = new Navigation();
            if (!string.IsNullOrEmpty(Id))
            {
                model = _NavigationService.Get(Id);
            }
            var dto = _IMapper.Map<NavigationDto>(model);
            //dto.DictList = GetDictRole();
            //获取菜单下所有选中的权限按钮
            //var nr_list = _NavigationRoleService.GetList(m => m.NId == dto.Id && m.Status == 1);
            //foreach (var item in dto.DictList)
            //{
            //    if(nr_list.Find(m=>m.DId == item.Id) != null)
            //    {
            //        item.Status = 1;
            //    }
            //    else
            //    {
            //        item.Status = 0;
            //    }
            //}
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
        public JsonResult ListTree(NavigationDto dto)
        {
            var response = _NavigationService.GetList(dto);
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
            foreach (var item in (List<Navigation>)response.data)
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

            response.data = list_ztree;
            return Json(response);
        }



        [HttpPost]
        public JsonResult List()
        {
            //dto.Level = 1;
            //var navData = _NavigationService.GetList(dto);

            //List<Navigation> nav_list = new List<Navigation>();

            //foreach (var item in (List<Navigation>)navData.data)
            //{
            //    nav_list.Add(item);
            //    dto.Level = 2;
            //    dto.ParentId = item.Id;
            //    var sub_list = _NavigationService.GetList(dto);
            //    nav_list.AddRange((List<Navigation>)sub_list.data);
            //}



            //var dto_list = _IMapper.Map<List<NavigationDto>>(nav_list);
            //foreach (var item in dto_list)
            //{
            //    item.DictList = GetDictRole();
            //    var nr_list = _NavigationRoleService.GetList(m => m.NId == item.Id && m.Status == 1).ToList();
            //    foreach (var it in item.DictList)
            //    {
            //        if (nr_list.Find(m => m.DId == it.Id) != null)
            //        {
            //            it.Status = 1;
            //        }
            //        else
            //        {
            //            it.Status = 0;
            //        }
            //    }
            //}
            var response = _NavigationService.GetList(new NavigationDto());
            var nav_dto = _IMapper.Map<List<NavigationDto>>(response.data);
            //菜单的权限按钮
            foreach (var item in nav_dto)
            {
                var res = _NavigationRoleService.GetList(new NavigationRoleDto() { NId = item.Id});
                if (res.total > 0)
                {
                    item.NRList = (List<NavigationRoleDto>)res.data;
                }
                else
                {
                    item.NRList = new List<NavigationRoleDto>();
                }
               
            }
            response.data = nav_dto;
            response.code = 0;

            return Json(response);
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
            model.Level = string.IsNullOrEmpty(model.ParentId) ? 1 : 2;
            var id = _NavigationService.Add(model);
            //新增菜单权限
            //foreach (var item in request.DictList)
            //{
            //    NavigationRole m_nr = new NavigationRole()
            //    {
            //        Id = Guid.NewGuid().GuidTo16String(),
            //        DId = item.Id,
            //        NId = model.Id,
            //        Status = item.Status
            //    };
            //    _NavigationRoleService.Add(m_nr);
            //}

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

            PageResponse response = new PageResponse();
            response.code = StatusCodeDefine.Success;
            response.status = 0;
            response.data = ztree;
            return Json(response);
        }

        [HttpPost]
        public JsonResult Edit(NavigationDto request)
        {
            PageResponse response = new PageResponse();
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
            ////删除该菜单下,所有的权限按钮
            //var list_role = _NavigationRoleService.GetList(m => m.NId == model.Id).ToList();
            //foreach (var item in list_role)
            //{
            //    _NavigationRoleService.Delete(item.Id);
            //}
            ////新增菜单权限
            //foreach (var item in request.DictList)
            //{
            //    NavigationRole m_nr = new NavigationRole()
            //    {
            //        Id = Guid.NewGuid().GuidTo16String(),
            //        DId = item.Id,
            //        NId = model.Id,
            //        Status = item.Status
            //    };
            //    _NavigationRoleService.Add(m_nr);
            //}
            _NavigationService.Update(model);

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


            response.code = StatusCodeDefine.Success;
            response.status = 0;
            response.data = ztree;
            return Json(response);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            _NavigationService.Delete(Id);
            PageResponse response = new PageResponse();
            response.code = StatusCodeDefine.Success;
            return Json(response);
        }


        public IActionResult Authorize(string Id)
        {
            //获取所有的权限按钮
            List<DictDto> list_dict = GetDictRole();
            //获取菜单所有的按钮
            var res = _NavigationRoleService.GetList(new NavigationRoleDto() { NId = Id });
            if (res.total > 0)
            {
                var NRList = (List<NavigationRoleDto>)res.data;

                var list_dict_new = list_dict.Where(m => NRList.Select(p => p.DId).Contains(m.Id)).ToList(); //权限按钮交集

                var NRList_new  = NRList.Where(m => list_dict.Select(p => p.Id).Contains(m.DId)).ToList(); //菜单按钮交集

                NRList.RemoveAll(m=>NRList_new.Select(p=>p.Id).Contains(m.Id));

                list_dict.RemoveAll(m => list_dict_new.Select(p => p.Id).Contains(m.Id));

                List<NavigationRole> list_nr_add = new List<NavigationRole>();
                List<NavigationRole> list_nr_del = new List<NavigationRole>();
                foreach (var item in list_dict)
                {
                    NavigationRole nr_model = new NavigationRole()
                    {
                        Id = Guid.NewGuid().GuidTo16String(),
                        DId = item.Id,
                        NId = Id,
                        Status = 0
                    };
                    list_nr_add.Add(nr_model);
                }
                list_nr_del = _IMapper.Map<List<NavigationRole>>(NRList);
                _NavigationRoleService.Add(list_nr_add, false);
                _NavigationRoleService.Delete(list_nr_del, false);
            }
            else
            {
                List<NavigationRole> list_nr_init = new List<NavigationRole>();
                foreach (var item in list_dict)
                {
                    NavigationRole nr_model = new NavigationRole()
                    {
                        Id = Guid.NewGuid().GuidTo16String(),
                        DId = item.Id,
                        NId = Id,
                        Status = 0
                    };
                    list_nr_init.Add(nr_model);
                }
                _NavigationRoleService.Add(list_nr_init, false);
            } 
            _NavigationRoleService.SaveChanges();
            var response = _NavigationRoleService.GetList(new NavigationRoleDto() { NId = Id });
            ViewData["list_nr"] = response.data;
            return View();
        }

        [HttpPost]
        public JsonResult Authorize(List<NavigationRole> list)
        {
            PageResponse response = new PageResponse()
            {
                code = StatusCodeDefine.Success,
                msg = "授权失败",
                status = -1
            };
            if (!list.Any())
            {
                return Json(response);
            }
            _NavigationRoleService.Update(list);
            response = new PageResponse()
            {
                code = StatusCodeDefine.Success,
                msg = "授权成功",
                status = 0
            };
            return Json(response);
        }

        private List<DictDto> GetDictRole()
        {
            List<SortOrder<Dict>> expressionOrder = new List<SortOrder<Dict>>()
            {
                new SortOrder<Dict>() { value = m => m.Sort, searchType = EnumBase.OrderType.Asc }
            };
            var dictType = _DictTypeService.GetList(m => m.Mark == "Role").FirstOrDefault();

            var response = _DictService.GetList(new DictDto() { Tid = dictType.Id, Status = 1 });
            var res = _IMapper.Map<List<DictDto>>(response.data);
            return res;
        }
    }
}