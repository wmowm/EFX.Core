using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Tibos.Test;

namespace Test
{

    class Model
    {

    }

    class Program
    {

        static void Main(string[] args)
        {
            //手续费(月)
            var Handlingfee = 0.38M/100;

            //总金额
            var TotalAmount = 160000M;

            //每月还款本金
            var RepaymentAmount = 2000M;

            var UserAmount = 0M;
            int i = 0;
            //while(TotalAmount > 0)
            //{
            //    i++;
            //    var cost = TotalAmount * Handlingfee;
            //    if (Handlingfee >= TotalAmount) Handlingfee = TotalAmount;
            //    TotalAmount = TotalAmount - RepaymentAmount;
            //    Console.WriteLine($"第{i}期手续费{cost},还款总金额{cost + RepaymentAmount},剩余应还本金{TotalAmount}");
            //    UserAmount += cost + RepaymentAmount;
            //    if (Handlingfee >= TotalAmount)
            //    {
            //        Console.WriteLine($"累计还款总金额:{UserAmount}");
            //    }
            //}

            //





            Console.Read();
        }
    }
}
