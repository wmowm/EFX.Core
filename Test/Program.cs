using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
            Double i = 1000000000;
            var e = Math.Pow((1 + 1 / i), i);
            Console.WriteLine(e);
            //MQHelper mq = new MQHelper();
            //mq.Publish();

            Console.Read();
        }
    }
}
