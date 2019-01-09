using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tibos.Api.Annotation;
using Tibos.Common;
using Tibos.Confing.autofac;
using Tibos.ConfingModel;
using Tibos.ConfingModel.model;
using Tibos.Domain;
using Tibos.Service;
using Tibos.Service.Contract;
namespace Tibos.Api.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public HomeController(IMapper mapper)
        {
        }
        public ManagerIService _ManagerIService { get; set; }
        public NavigationIService _NavigationIService { get; set; }
        public IMapper _IMapper { get; set; }


        // GET api/values
        [HttpGet]
        [AlwaysAccessibleAttribute]
        public IEnumerable<string> Get()
        {
            var config = JsonConfigurationHelper.GetAppSettings<ManageConfig>("ManageConfig.json", "ManageConfig");
            return new string[] { "" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            //Navigation navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "主页",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "layui-icon layui-icon-home",
            //    IsSys = 1,
            //    Level = 1,
            //    Link = "/",
            //    ParentId = "#",
            //    Sort = 1
            //};
            //var m1 = _NavigationIService.Save(navigation);

            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "控制台",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "",
            //    IsSys = 1,
            //    Level = 2,
            //    Link = "/",
            //    ParentId = m1,
            //    Sort = 1
            //};
            //_NavigationIService.Save(navigation);

            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "主页一",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "",
            //    IsSys = 1,
            //    Level = 2,
            //    Link = "/",
            //    ParentId = m1,
            //    Sort = 2
            //};
            //_NavigationIService.Save(navigation);


            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "主页二",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "",
            //    IsSys = 1,
            //    Level = 2,
            //    Link = "/",
            //    ParentId = m1,
            //    Sort = 3
            //};
            //_NavigationIService.Save(navigation);



            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "组件",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "layui-icon layui-icon-component",
            //    IsSys = 1,
            //    Level = 1,
            //    Link = "/",
            //    ParentId = "#",
            //    Sort = 2
            //};
            //var m2 = _NavigationIService.Save(navigation);



            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "组件一",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "",
            //    IsSys = 1,
            //    Level = 2,
            //    Link = "/",
            //    ParentId = m2,
            //    Sort = 1
            //};
            //_NavigationIService.Save(navigation);


            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "组件二",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "",
            //    IsSys = 1,
            //    Level = 2,
            //    Link = "/",
            //    ParentId = m2,
            //    Sort = 2
            //};
            //_NavigationIService.Save(navigation);




            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "设置",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "layui-nav-more",
            //    IsSys = 1,
            //    Level = 1,
            //    Link = "/",
            //    ParentId = "#",
            //    Sort = 3
            //};
            //var m3 = _NavigationIService.Save(navigation);


            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "系统设置",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "",
            //    IsSys = 1,
            //    Level = 2,
            //    Link = "/",
            //    ParentId = m3,
            //    Sort = 1
            //};
            //var m31 = _NavigationIService.Save(navigation);

            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "网站设置",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "",
            //    IsSys = 1,
            //    Level = 3,
            //    Link = "/",
            //    ParentId = m31,
            //    Sort = 1
            //};
            //_NavigationIService.Save(navigation);


            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "邮箱设置",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "",
            //    IsSys = 1,
            //    Level = 3,
            //    Link = "/",
            //    ParentId = m31,
            //    Sort = 2
            //};
            //_NavigationIService.Save(navigation);



            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "我的设置",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "",
            //    IsSys = 1,
            //    Level = 2,
            //    Link = "/",
            //    ParentId = m3,
            //    Sort = 2
            //};
            //var m32 = _NavigationIService.Save(navigation);



            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "基本资料",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "",
            //    IsSys = 1,
            //    Level = 3,
            //    Link = "/",
            //    ParentId = m32,
            //    Sort = 1
            //};
            //_NavigationIService.Save(navigation);

            //navigation = new Navigation()
            //{
            //    Id = Guid.NewGuid().GuidTo16String(),
            //    Name = "修改密码",
            //    Areas = "/",
            //    ControllerName = "/",
            //    Icon = "",
            //    IsSys = 1,
            //    Level = 3,
            //    Link = "/",
            //    ParentId = m32,
            //    Sort = 2
            //};
            //_NavigationIService.Save(navigation);

            return await Task.Run<string>(()=> {return Test(); });
        }

        private string Test()
        {
            return "666";
        }



    }



   

}
