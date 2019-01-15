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



            var mm = Assembly.GetEntryAssembly().Location;
            Console.WriteLine(mm);

            var path = @"F:\GitProject\EFX_Core\Tibos.Admin\bin\Debug\Tibos.Repository.dll";
            var path2= @"F:\GitProject\EFX_Core\Tibos.Admin\bin\Debug\Tibos.Service.dll";
            //程序集注入
            byte[] buffer = System.IO.File.ReadAllBytes(path);
            var Repository = Assembly.Load(buffer);

            Console.Read();
        }
    }
}
