using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tibos.Common;
using Tibos.Domain;
using Tibos.Service.Contract;

namespace Tibos.Admin.Areas.CMS.Controllers
{
    [Area("CMS")]
    public class NewsController : Controller
    {
        public UsersIService _UsersIService { get; set; }
        public NavigationIService _NavigationIService { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult List(UsersRequest request)
        {
            var list = _UsersIService.GetList(request);
            var count = _UsersIService.GetCount(request);
            Json reponse = new Json();
            reponse.code = 200;
            reponse.total = count;
            reponse.data = list;
            return Json(reponse);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }



        [HttpPost]
        public JsonResult Create(UsersRequest request)
        {
            //添加用户
            //for (int i = 0; i < 5000; i++)
            //{
            //    var rdm = new Random().Next(10000000, 999999999);
            //    Users user = new Users()
            //    {
            //        Email = rdm + "@qq.com",
            //        Id = Guid.NewGuid().GuidTo16String(),
            //        LoginIp = "127.0.0.1",
            //        LoginTime = DateTime.Now.AddDays(-new Random().Next(1000, 9999)),
            //        Mobile = "150" + new Random().Next(100000, 999999),
            //        Password = "123456",
            //        Sex = rdm % 3,
            //        Status = rdm % 4,
            //        UserName = $"tibos_{i}"
            //    };
            //    _UsersIService.Save(user);
            //}

            //添加菜单
            //Navigation model_0 = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Areas = "CMS",
            //    ControllerName = "News",
            //    IsSys = 0,
            //    Level = 1,
            //    Name = "资讯列表",
            //    ParentId = "",
            //    Sort = 1,
            //    Link = "#"
            //};
            //Navigation model_1 = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Areas = "CMS",
            //    ControllerName = "News",
            //    IsSys = 0,
            //    Level = 2,
            //    Name = "资讯列表",
            //    ParentId = model_0.Id,
            //    Sort = 1
            //};
            //model_1.Link = $"/{model_1.Areas}/{model_1.ControllerName}/Index";
            //_NavigationIService.Save(model_0);
            //_NavigationIService.Save(model_1);




            //Navigation model_2 = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Areas = "SYS",
            //    ControllerName = "Navigation",
            //    IsSys = 1,
            //    Level = 1,
            //    Name = "系统设置",
            //    ParentId = "",
            //    Sort = 1,
            //    Link = "#"
            //};
            //Navigation model_3 = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Areas = "SYS",
            //    ControllerName = "Navigation",
            //    IsSys = 0,
            //    Level = 2,
            //    Name = "菜单列表",
            //    ParentId = model_2.Id,
            //    Sort = 1
            //};
            //model_3.Link = $"/{model_3.Areas}/{model_3.ControllerName}/Index";
            //_NavigationIService.Save(model_2);
            //_NavigationIService.Save(model_3);



            Json reponse = new Json();
            reponse.code = 200;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Edit(UsersRequest request)
        {
            Json reponse = new Json();
            reponse.code = 200;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Del()
        {
            Json reponse = new Json();
            reponse.code = 200;
            
            return Json(reponse);
        }
    }
}