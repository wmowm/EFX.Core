using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Tibos.Domain;
using Tibos.IRepository;
//using Tibos.Repository;
using Tibos.Repository.Tibos;
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
            IBaseRepository<Dict> rep = new TibosRepository<Dict>();
            var list = rep.GetList();

            IBaseRepository<Manager> re = new DhmRepository<Manager>();
            var ll = re.GetList();
            Console.Read();
        }
    }
}
