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
using Tibos.Service;
namespace Tibos.Api.Areas.User.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]

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
            var config = JsonConfigurationHelper.GetAppSettings<ManageConfig>("ManageConfig.json", "ManageConfig");
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
        public async Task<string> PostTest666([FromBody]List<Users> list)
        {
            return await Task.Run(() => 
            {
                return list.Count.ToString(); 
            });
            return "777";
        }



        private string Test()
        {
            return "zzz";
        }



    }



   

}
