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
    public class DictTypeController : Controller
    {

        public DictIService _DictIService { get; set; }

        public DictTypeIService _DictTypeIService { get; set; }

        public IMapper _IMapper { get; set; }

        #region DictType
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = new DictType();
            return View(model);
        }
        public IActionResult Edit(string Id)
        {
            var model = new DictType();
            if (!string.IsNullOrEmpty(Id))
            {
                model = _DictTypeIService.Get(Id);
            }
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List(DictTypeRequest request)
        {

            var list = _DictTypeIService.GetList(request);

            List<DictType> nav_list = new List<DictType>();

        


            Json reponse = new Json();
            reponse.code = 200;
            reponse.total = nav_list.Count;
            reponse.data = nav_list;
            return Json(reponse);
        }



        [HttpPost]
        public JsonResult ListTree(DictTypeRequest request)
        {
            request.sortKey = "Sort";
            request.sortType = 0;
            var list = _DictTypeIService.GetList(request);
            var count = _DictTypeIService.GetCount(request);
            List<zTree> list_ztree = new List<zTree>();
            zTree ztree = new zTree()
            {
                id = "0",
                pId = "#",
                name = "字典",
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
                    pId = "0",
                    name = item.Name,
                    open = true
                };
                list_ztree.Add(ztree);
            }

            Json reponse = new Json();
            reponse.code = 200;
            reponse.total = count;
            reponse.data = list_ztree;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Create(DictType request)
        {
            request.Id = Guid.NewGuid().GuidTo16String();
            var id = _DictTypeIService.Save(request);
            zTree ztree = new zTree()
            {
                id = request.Id,
                pId = "0",
                name = request.Name,
                open = true
            };
            Json reponse = new Json();
            reponse.code = 200;
            reponse.status = 0;
            reponse.data = ztree;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Edit(DictType request)
        {
            Json reponse = new Json();
            _DictTypeIService.Update(request);
            zTree ztree = new zTree()
            {
                id = request.Id,
                pId = "0",
                name = request.Name,
                open = true
            };
            reponse.code = 200;
            reponse.status = 0;
            reponse.data = ztree;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            _DictTypeIService.Delete(Id);
            Json reponse = new Json();
            reponse.code = 200;
            return Json(reponse);
        }

        #endregion

    }
}