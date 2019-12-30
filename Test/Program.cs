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
using Microsoft.Scripting.Runtime;
using System.Threading.Tasks;

namespace Test
{

    class Program
    {

        static void Main(string[] args)
        {
            int targer = 6;
            List<int> nums = new List<int>() { 1, 2, 2, 3, 4, 5, 6, 3, 2 };
            var t = CheckNums(nums.ToArray(), 6);

            Console.Read();
        }


        private static int[] CheckNums(int[] nums, int targer)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < nums.Length-1; j++)
                {
                    if (nums[i] + nums[j] == targer) 
                    {
                        if (!res.Contains(i) && !res.Contains(j) && i != j) 
                        {
                            res.Add(i);
                            res.Add(j);
                        }
                    }
                }
            }
            return res.ToArray();
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
        static Test()
        {
            Console.WriteLine("static");
        }
        public Test() 
        {
            Console.WriteLine("t");
        }
    }
}
