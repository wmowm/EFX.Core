using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EF_Test
{
    class Program
    {


        /// <summary>
        /// 检查迁移
        /// </summary>
        /// <param name="db"></param>
        static void CheckMigrations(TibosDbContext db)
        {
            try
            {
                //执行迁移
                db.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        static void Main(string[] args)
        {

            //using (var db = new TibosDbContext())
            //{
            //    //检查迁移
            //    //CheckMigrations(db);







            //}


            for (int i = 0; i < 1000; i++)
            {
                ManagerRepository manager = new ManagerRepository(new TibosDbContext());
                var model = manager.Get(m => m.UserName == "111222" && m.Password == "111222" && m.Status == 1);
            }

            //DictRepository Dict = new DictRepository(new TibosDbContext());
            //var dict_list = Dict.GetList();

            //var count = Dict.Count();

            //ManagerRepository Manager = new ManagerRepository(new DhmDbContext());

            //var manager_list = Manager.GetList();

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
