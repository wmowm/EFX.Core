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
using MySql.Data.MySqlClient;
using Tibos.CAP.Models;
using Tibos.Domain;

namespace Tibos.CAP.Controllers
{
    [Produces("application/json")]
    [Route("api/Publish")]
    public class PublishController : Controller
    {

        private readonly ICapPublisher _capBus;

        public PublishController(ICapPublisher capPublisher)
        {
            _capBus = capPublisher;
        }

        [Route("~/adonet/transaction")]
        public IActionResult AdonetWithTransaction()
        {
            string ConnectionString = "Data Source=132.232.4.73;Initial Catalog=CAP_DB;port=3307; User ID=root;Password=As123456;SslMode = none;";

            using (var connection = new MySqlConnection(ConnectionString))
            {

                using (var transaction = connection.BeginTransaction(_capBus, autoCommit: false))
                {
                    connection.Execute($"insert into MessageRecord(UserName,Msg,CreateTime) values('test','测试','{DateTime.Now}')", transaction: (IDbTransaction)transaction.DbTransaction);
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

        }
    }
}