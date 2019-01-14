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
    public class ManagerController : Controller
    {

        public IManagerService _ManagerService { get; set; }
        public IRoleService _RoleService { get; set; }

        public IMapper _IMapper { get; set; }

        #region Manager
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewData["RoleList"] = GetRoleList();
            Manager model = new Manager();
            return View(model);
        }
        public IActionResult Edit(string Id)
        {
            ViewData["RoleList"] = GetRoleList();
            var model = new Manager();
            if (!string.IsNullOrEmpty(Id))
            {
                model = _ManagerService.Get(Id);

            }
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List(ManagerDto dto)
        {
            var response = _ManagerService.GetList(dto);
            return Json(response);
        }


        [HttpPost]
        public JsonResult Create(Manager request)
        {
            request.Id = Guid.NewGuid().GuidTo16String();
            var id = _ManagerService.Add(request);
            PageResponse response = new PageResponse();
            response.code = StatusCodeDefine.Success;
            response.status = 0;
            return Json(response);
        }

        [HttpPost]
        public JsonResult Edit(Manager request)
        {
            PageResponse response = new PageResponse();
            _ManagerService.Update(request);
            response.code = StatusCodeDefine.Success;
            response.status = 0;
            return Json(response);
        }

        [HttpPost]
        public JsonResult EditStatus(string Id,int Status)
        {
            PageResponse response = new PageResponse();
            var model = _ManagerService.Get(Id);
            if(model.Status != Status)
            {
                model.Status = Status;
                _ManagerService.Update(model);
            }
            response.code = StatusCodeDefine.Success;
            response.status = 0;
            return Json(response);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            _ManagerService.Delete(Id);
            PageResponse response = new PageResponse();
            response.code = StatusCodeDefine.Success;
            return Json(response);
        }

        #endregion




        private List<Role> GetRoleList()
        {
            var res = _RoleService.GetList();
            return res.ToList();
        }

    }
}