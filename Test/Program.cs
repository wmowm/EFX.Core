using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using Tibos.Test;
using Newtonsoft.Json;
using Test.Model;
using System.Diagnostics;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using Tibos.Repository.Tibos;

namespace Test
{

    class Program
    {

        static void Main(string[] args)
        {
            for (int i = 0; i < 100000; i++)
            {
                ManagerRepository dal = new ManagerRepository();
                var model = dal.Get(m => m.UserName == "111222" && m.Password == "111222" && m.Status == 1);
            }

            Console.Read();
        }

        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            double intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            intResult = (dateTime - startTime).TotalMilliseconds;
            return  Convert.ToInt64(Math.Round(intResult, 0).ToString());
        }
    }
    class Test
    {
        public string Name { get; set; }
    }
}
