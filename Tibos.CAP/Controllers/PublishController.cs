using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Tibos.CAP.Common;
using Tibos.CAP.Models;
using Tibos.Domain;

namespace Tibos.CAP.Controllers
{
    [Produces("application/json")]
    [Route("api/Publish")]
    public class PublishController : Controller
    {

        private readonly ICapPublisher _capBus;
        private readonly ICacheService _redis;
        public PublishController(ICapPublisher capPublisher, ICacheService redis)
        {
            _capBus = capPublisher;
            _redis = redis;
            //_redis.Insert<int>("Product_Count", 100);

        }

        [Route("~/adonet/transaction")]
        public IActionResult AdonetWithTransaction()
        {
            string ConnectionString = "Data Source=148.70.88.40;Initial Catalog=CAP_DB;port=3307; User ID=root;Password=As123456;SslMode = none;";

            using (var connection = new MySqlConnection(ConnectionString))
            {

                using (var transaction = connection.BeginTransaction(_capBus, autoCommit: false))
                {
                    var sql = $"insert into MessageRecord(UserName,Msg,CreateTime) values('test','这里是正确的sql','{DateTime.Now}')";
                    var sql2 = $"insert into MessageRecord(UserName,Msg,CreateTime) values('test','这里故意写错','{DateTime.Now}','')";

                    connection.Execute(sql, transaction: (IDbTransaction)transaction.DbTransaction);
                    connection.Execute(sql2, transaction: (IDbTransaction)transaction.DbTransaction);
                    //your business code
                    Users user = new Users();
                    user.Email = "XX";
                    _capBus.Publish("tibos.services.bar", user, "callback-show-execute-time");
                    transaction.Commit();
                }


            }

            return Ok();
        }

        [CapSubscribe("callback-show-execute-time")]
        public void BarMessageProcessor(Users user)
        {
            if(user.Email == "执行任务成功")
            {

            }
            else
            {
                //执行补偿
            }
        }


        [Route("~/adonet/test")]
        public JsonResult Test()
        {
            var model = new temp() { msg = GetOrder() };
            return Json(model);
        }



        private string GetOrder()
        {
            var res = "";
            var db = _redis.GetDatabase();
            var info = "name-" + Guid.NewGuid();
            //如果5秒不释放锁 自动释放。避免死锁
            if (db.LockTake("name", info, TimeSpan.FromSeconds(5)))
            {
                try
                {
                    int result = _redis.Get<int>("ppt");
                    if (result > 100)
                    {
                        res = ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>很遗憾,没有商品了!";
                        Console.WriteLine(res);
                        return res;
                    }
                    _redis.Set("ppt", result + 1);
                    res = $">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>恭喜你,抢到了第{result}件商品!";
                    Console.WriteLine(res);
                    return res;
                }
                catch (Exception ex)
                {

                }
                finally
                {

                    db.LockRelease("name", info);

                }
            }
            res = ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>都没有挤进去,好尴尬呀!";
            Console.WriteLine(res);
            return res;
        }

    }

    public class temp
    {
        public string msg { get; set; }
    }
}