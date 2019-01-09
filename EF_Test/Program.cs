using System;
using System.Collections.Generic;
using System.Linq;

namespace EF_Test
{
    class Program
    {
        static void Main(string[] args)
        {

            DictRepository Dict = new DictRepository(new TibosDbContext());
            var dict_list = Dict.GetList();

            var count = Dict.Count();

            ManagerRepository Manager = new ManagerRepository(new DhmDbContext());

            var manager_list = Manager.GetList();

            Console.Read();
        }



        //public  List<Dict> GetList()
        //{
        //    using (BaseDbContext db = new BaseDbContext())
        //    {
        //        var query = db.Dict.AsQueryable();
        //        //query = query.OrderBy(p => p.Sort).ThenByDescending(p => p.CreateTime);
        //        var list = query.ToList();
        //        return list;
        //    }
        //}

        //public List<Dict> GetList(int pageIndex,int pageSize)
        //{
        //    using (BaseDbContext db = new BaseDbContext())
        //    {
        //        var query = db.Dict.Skip(pageIndex * pageSize).Take(pageSize);
        //        //query = query.OrderBy(p => p.Sort).ThenByDescending(p => p.CreateTime);
        //        var list = query.ToList();
        //        return list;
        //    }
        //}

    }
}
