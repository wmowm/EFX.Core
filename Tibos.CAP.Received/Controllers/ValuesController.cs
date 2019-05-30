using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Tibos.Domain;

namespace Tibos.CAP.Received.Controllers
{
    //[EnableCors("any")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        [CapSubscribe("tibos.services.bar")]
        public Users BarMessageProcessor(Users user)
        {
            try
            {
                //执行任务成功
                user.Email = "执行任务成功";
            }
            catch (Exception ex)
            {
                //执行任务失败
                user.Email = "执行任务失败";
            }

            return user;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public JsonResult PostTest(string value)
        {
            return Json(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
