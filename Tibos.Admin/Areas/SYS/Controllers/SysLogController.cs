using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tibos.Domain;
using Tibos.IService.Tibos;

namespace Tibos.Admin.Areas.SYS.Controllers
{
    [Area("SYS")]
    public class SysLogController : Controller
    {

        public ISysLogService _SysLogService { get; set; }

        public IMapper _IMapper { get; set; }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult GetList(SysLogDto dto)
        {
            var response = _SysLogService.GetList(dto);
            return Json(response);
        }
    }
}