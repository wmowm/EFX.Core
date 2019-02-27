using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using Tibos.Test;
using Newtonsoft.Json;
using Test.Model;

namespace Test
{

    class Program
    {

        static void Main(string[] args)
        {
            string world = @"G:\world\zzz.docx";
            string html = @"G:\world\zzz.html";
            //var r = wordHelp.WordToHtml(world, html);

            NpoiHeplper.Export(world, html, null);
            Console.Read();
            return;
            var ai = "b3adb7e5a168e167";
            var sendingTime = DateTimeToUnixTimestamp(DateTime.Now);
            Model.Body body = new Model.Body()
            {
                cs1 = "15019400599",
                t = "cstm",
                tm = sendingTime,
                n = "trading",
                var = new Model.DataTrading()
                {
                    //tradingId = "test001",
                    tradingPair = "ETH/VHKD",
                    //tradingWay_var = "委托买入"
                }
            };
            List<Body> list = new List<Body>();
            list.Add(body);
            var data = JsonConvert.SerializeObject(list);

            string url = $"https://api.growingio.com/v3/{ai}/s2s/cstm?stm={sendingTime}";

            
            var res = HttpCommon.PostWebRequest(url, data);

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
}
