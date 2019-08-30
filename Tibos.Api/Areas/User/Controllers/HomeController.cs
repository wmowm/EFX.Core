using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tibos.Api.Annotation;
using Tibos.Api.Common;
using Tibos.Common;
using Tibos.Confing.autofac;
using Tibos.ConfingModel;
using Tibos.ConfingModel.model;
using Tibos.Domain;
using Tibos.Repository.Tibos;
using Tibos.Service;
namespace Tibos.Api.Areas.User.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]

    public class HomeController : Controller
    {
        public HomeController(IMapper mapper)
        {
        }
        public IMapper _IMapper { get; set; }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var config =  JsonConfigurationHelper.GetAppSettings<ManageConfig>("ManageConfig.json", "ManageConfig");
            return new string[] { "" };
        }
        [AlwaysAccessible]
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {

            return await Task.Run<string>(()=> {return Test(); });
        }


        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [AlwaysAccessible]
        [HttpPost]
        public async Task<string> login(string user_name, string password, string code, string returnUrl)
        {
            ManagerRepository dal = new ManagerRepository();
            var model = dal.Get(m => m.UserName == user_name && m.Password == password && m.Status == 1);
            return "777";
        }



        private string Test()
        {
            return "zzz";
        }



    }



   

}
