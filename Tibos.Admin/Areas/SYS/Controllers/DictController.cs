using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tibos.Common;
using Tibos.Domain;
using Tibos.Service.Contract;

namespace Tibos.Admin.Areas.SYS.Controllers
{
    [Area("SYS")]
    public class DictController : Controller
    {

        public DictIService _DictIService { get; set; }

        public DictTypeIService _DictTypeIService { get; set; }

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
                model = _DictIService.Get(Id);
            }
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List(DictRequest request)
        {
            request.sortKey = "Sort";
            request.sortType = 0;
            IList<Dict> list = new List<Dict>();
            var count = 0;
            if (!string.IsNullOrEmpty(request.Tid))
            {
                 list = _DictIService.GetList(request);
                 count = _DictIService.GetCount(request);
            }
            Json reponse = new Json();
            reponse.code = 200;
            reponse.total = count;
            reponse.data = list;
            return Json(reponse);
        }


        [HttpPost]
        public JsonResult Create(Dict request)
        {
            request.Id = Guid.NewGuid().GuidTo16String();
            var id = _DictIService.Save(request);
            Json reponse = new Json();
            reponse.code = 200;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Edit(Dict request)
        {
            Json reponse = new Json();
            _DictIService.Update(request);
            reponse.code = 200;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult EditStatus(string Id,int Status)
        {
            Json reponse = new Json();
            var model = _DictIService.Get(Id);
            if(model.Status != Status)
            {
                model.Status = Status;
                _DictIService.Update(model);
            }
            reponse.code = 200;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            _DictIService.Delete(Id);
            Json reponse = new Json();
            reponse.code = 200;
            return Json(reponse);
        }

        #endregion

    }
}