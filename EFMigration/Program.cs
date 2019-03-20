using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始数据迁移~~~~");
            using (var db = new BaseDbContext())
            {
                //检查迁移
                CheckMigrations(db);
            }
            Console.Read();
        }

        /// <summary>
        /// 检查迁移
        /// </summary>
        /// <param name="db"></param>
        static void CheckMigrations(BaseDbContext db)
        {
            Console.WriteLine("检查迁移数据!");

            //判断是否有待迁移
            if (db.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("准备迁移数据...");
                //执行迁移
                db.Database.Migrate();
                Console.WriteLine("数据迁移完成!");
            }
            else
            {
                Console.WriteLine("没有可迁移的数据!");
            }
        }

    }
}
