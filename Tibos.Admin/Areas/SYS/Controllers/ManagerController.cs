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
    public class ManagerController : Controller
    {

        public ManagerIService _ManagerIService { get; set; }
        public RoleIService _RoleIService { get; set; }

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
                model = _ManagerIService.Get(Id);

            }
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List(ManagerRequest request)
        {
            request.sortKey = "Sort";
            request.sortType = 0;
            IList<Manager> list = _ManagerIService.GetList(request);
            var count = _ManagerIService.GetCount(request);
            Json reponse = new Json();
            reponse.code = 200;
            reponse.total = count;
            reponse.data = list;
            return Json(reponse);
        }


        [HttpPost]
        public JsonResult Create(Manager request)
        {
            request.Id = Guid.NewGuid().GuidTo16String();
            var id = _ManagerIService.Save(request);
            Json reponse = new Json();
            reponse.code = 200;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Edit(Manager request)
        {
            Json reponse = new Json();
            _ManagerIService.Update(request);
            reponse.code = 200;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult EditStatus(string Id,int Status)
        {
            Json reponse = new Json();
            var model = _ManagerIService.Get(Id);
            if(model.Status != Status)
            {
                model.Status = Status;
                _ManagerIService.Update(model);
            }
            reponse.code = 200;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            _ManagerIService.Delete(Id);
            Json reponse = new Json();
            reponse.code = 200;
            return Json(reponse);
        }

        #endregion




        private List<Role> GetRoleList()
        {
            var res = _RoleIService.GetList(null);
            return res.ToList();
        }

    }
}