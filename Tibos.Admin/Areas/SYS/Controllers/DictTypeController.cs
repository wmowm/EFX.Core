using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tibos.Common;
using Tibos.Domain;
using Tibos.IService.Tibos;
using Tibos.IService.Tibos;

namespace Tibos.Admin.Areas.SYS.Controllers
{
    [Area("SYS")]
    public class DictTypeController : Controller
    {

        public IDictService _DictService { get; set; }

        public IDictTypeService _DictTypeService { get; set; }

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
                model = _DictTypeService.Get(Id);
            }
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List(DictTypeDto dto)
        {

            PageResponse reponse = new PageResponse();
            reponse = _DictTypeService.GetList(dto);
            return Json(reponse);
        }



        [HttpPost]
        public JsonResult ListTree(DictTypeDto dto)
        {

            PageResponse reponse = new PageResponse();
            reponse = _DictTypeService.GetList(dto);
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
            foreach (var item in (List<DictType>)reponse.data)
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
            reponse.data = list_ztree;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Create(DictType request)
        {
            request.Id = Guid.NewGuid().GuidTo16String();
            var id = _DictTypeService.Add(request);
            zTree ztree = new zTree()
            {
                id = request.Id,
                pId = "0",
                name = request.Name,
                open = true
            };
            PageResponse reponse = new PageResponse();
            reponse.code = StatusCodeDefine.Success;
            reponse.status = 0;
            reponse.data = ztree;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Edit(DictType request)
        {
            PageResponse reponse = new PageResponse();
            _DictTypeService.Update(request);
            zTree ztree = new zTree()
            {
                id = request.Id,
                pId = "0",
                name = request.Name,
                open = true
            };
            reponse.code = StatusCodeDefine.Success;
            reponse.status = 0;
            reponse.data = ztree;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            _DictTypeService.Delete(Id);
            PageResponse reponse = new PageResponse();
            reponse.code = StatusCodeDefine.Success;
            return Json(reponse);
        }

        #endregion

    }
}