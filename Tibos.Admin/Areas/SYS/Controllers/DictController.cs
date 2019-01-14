using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tibos.Common;
using Tibos.Domain;
using Tibos.IService.Tibos;
using Tibos.Service.Tibos;

namespace Tibos.Admin.Areas.SYS.Controllers
{
    [Area("SYS")]
    public class DictController : Controller
    {


        public IDictService _DictService { get; set; }

        public IDictTypeService _DictTypeService { get; set; }

        #region Dict
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string Tid)
        {
            Dict model = new Dict();
            model.Tid = Tid;
            return View(model);
        }
        public IActionResult Edit(string Id)
        {
            var model = new Dict();
            if (!string.IsNullOrEmpty(Id))
            {
                model = _DictService.Get(Id);
            }
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List(DictDto dto)
        {
            IList<Dict> list = new List<Dict>();
            PageResponse reponse = new PageResponse();
            if (!string.IsNullOrEmpty(dto.Tid))
            {
                reponse = _DictService.GetList(dto);
            }
            return Json(reponse);
        }


        [HttpPost]
        public JsonResult Create(Dict request)
        {
            request.Id = Guid.NewGuid().GuidTo16String();
            var id = _DictService.Add(request);
            PageResponse reponse = new PageResponse();
            reponse.code = StatusCodeDefine.Success;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Edit(Dict request)
        {
            PageResponse reponse = new PageResponse();
            _DictService.Update(request);
            reponse.code = StatusCodeDefine.Success;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult EditStatus(string Id,int Status)
        {
            PageResponse reponse = new PageResponse();
            var model = _DictService.Get(Id);
            if(model.Status != Status)
            {
                model.Status = Status;
                _DictService.Update(model);
            }
            reponse.code = StatusCodeDefine.Success;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            _DictService.Delete(Id);
            PageResponse reponse = new PageResponse();
            reponse.code = StatusCodeDefine.Success;
            return Json(reponse);
        }

        #endregion

    }
}