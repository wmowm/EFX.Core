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
using Newtonsoft.Json;
using Tibos.CAP.Common;
using Tibos.CAP.Models;

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
            string ConnectionString = "Data Source=10.0.1.157;Initial Catalog=fuluDB;port=3306; User ID=root;Password=fulu123;SslMode = none;";

            using (var connection = new MySqlConnection(ConnectionString))
            {

                using (var transaction = connection.BeginTransaction(_capBus, autoCommit: false))
                {
                    //var sql = $"insert into MessageRecord(UserName,Msg,CreateTime) values('test','这里是正确的sql','{DateTime.Now}')";
                    //var sql2 = $"insert into MessageRecord(UserName,Msg,CreateTime) values('test','这里故意写错','{DateTime.Now}','')";

                    //connection.Execute(sql, transaction: (IDbTransaction)transaction.DbTransaction);
                    //connection.Execute(sql2, transaction: (IDbTransaction)transaction.DbTransaction);
                    //your business code
                    Users user = new Users();
                    user.Email = "XX";
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    user.Mobile = i.ToString();
                    //    _capBus.PublishAsync("tibos.services.bar", user, "callback-show-execute-time", max:i).Wait();
                    //    Console.WriteLine(i.ToString());
                    //}
                    _capBus.Publish("tibos.services.bar", user, "callback-show-execute-time");
                    transaction.Commit();
                }


            }

            return Ok();
        }

        [CapSubscribe("callback-show-execute-time")]
        public void BarMessageProcessor(Users user)
        {
            if (user.Email == "执行任务成功")
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
            SendFlashDepth(new FlashDepth()
            {
                CreateTime = DateTime.Now,
                Price = 82140,
                Symbol = "BTC/VHKD"
            });
            //SendDepth(new Depth()
            //{
            //    Symbol = "VHKD/CNY",
            //    Asks = 0.8840M,
            //    Bids = 0.8800M,
            //    AsksAmount = 10000000,
            //    BidsAmount = 10000000,
            //    CreateTime = DateTime.Now
            //});
            //SendDepth(new Depth()
            //{
            //    Symbol = "BTC/CNY",
            //    Asks = 56293M,
            //    Bids = 54622M,
            //    AsksAmount = 10000000,
            //    BidsAmount = 10000000,
            //    CreateTime = DateTime.Now
            //});
            //SendDepth(new Depth()
            //{
            //    Symbol = "ETH/CNY",
            //    Asks = 900M,
            //    Bids = 800M,
            //    AsksAmount = 10000000,
            //    BidsAmount = 10000000,
            //    CreateTime = DateTime.Now
            //});
            //SendDepth(new Depth()
            //{
            //    Symbol = "BTC/VHKD",
            //    Asks = 36000M,
            //    Bids = 35000M,
            //    AsksAmount = 10000000,
            //    BidsAmount = 10000000,
            //    CreateTime = DateTime.Now
            //});
            //SendDepth(new Depth()
            //{
            //    Symbol = "ETH/VHKD",
            //    Asks = 1000M,
            //    Bids = 900M,
            //    AsksAmount = 10000000,
            //    BidsAmount = 10000000,
            //    CreateTime = DateTime.Now
            //});
            return Json("");
        }


        [Route("~/adonet/GetTest/")]
        public JsonResult GetTest(string value)
        {
            return Json(value);
        }

        [Route("~/api/sendOrder/")]
        [HttpGet]
        public string SendOrder() 
        {
            return GetOrder();
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






        /// <summary>
        /// 推送行情
        /// </summary>
        /// <returns></returns>
        public string Symbol { get; set; }




        private void SendDepth(Depth depth)
        {

            var key = $"_VGPAY_{depth.Symbol}_";
            var db = _redis.GetDatabase();
            var info = "vgpay" + Guid.NewGuid();
            //如果5秒不释放锁 自动释放。避免死锁
            if (db.LockTake($"_vgpay_{depth.Symbol}_", info, TimeSpan.FromSeconds(5)))
            {
                try
                {
                    _redis.Set(key, depth);
                    Console.WriteLine("添加成功!");
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    db.LockRelease($"_vgpay_{depth.Symbol}", info);
                }
            }
        }





        private void SendFlashDepth(FlashDepth depth)
        {

            var key = $"_VGPAY_FLASH_{depth.Symbol}_";
            //var db = _redis.GetDatabase();
            _redis.Set(key, depth);
        }


        private Depth GetDepth(string Symbol)
        {
            var key = $"_VGPAY_{Symbol}_";

            var bl = _redis.Exists("num");
            var temp_num = 0;
            if (bl)
            {
                temp_num = _redis.Get<int>("num");
                temp_num += 1;
                _redis.Set("num", temp_num);
            }
            else
            {
                _redis.Set("num", 1);
                temp_num = 1;
            }

            var value = _redis.Get<Depth>(key);
            var res = $"第{temp_num}次获取行情,行情:{JsonConvert.SerializeObject(value)}";
            Console.WriteLine(res);
            return value;
        }
    }

    public class temp
    {
        public string msg { get; set; }

        public Depth model { get; set; }
    }



    public class Depth
    {
        /// <summary>
        /// 交易对
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// 卖币数量
        /// </summary>
        public decimal AsksAmount { get; set; }

        /// <summary>
        /// 买币数量
        /// </summary>
        public decimal BidsAmount { get; set; }

        /// <summary>
        /// 平台卖币
        /// </summary>
        public decimal Asks { get; set; }

        /// <summary>
        /// 平台收币
        /// </summary>
        public decimal Bids { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }



    public class FlashDepth
    {
        /// <summary>
        /// 交易对
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// 兑换价格
        /// </summary>
        public decimal Price { get; set; }

        public DateTime CreateTime { get; set; }
    }
}