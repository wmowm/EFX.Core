using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator.Common
{
    public class SqliteFreeSql: ISqliteFreeSql
    {
        private IFreeSql dao;
        public SqliteFreeSql()
        {
            dao = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, "Data Source=|DataDirectory|tibos.db")
            .UseAutoSyncStructure(true) //自动同步实体结构【开发环境必备】
            .Build();
        }

        public IAdo Ado => dao.Ado;

        public IAop Aop => dao.Aop;

        public ICodeFirst CodeFirst => dao.CodeFirst;

        public IDbFirst DbFirst => dao.DbFirst;

        public IDelete<T1> Delete<T1>() where T1 : class
        {
            return dao.Delete<T1>();
        }

        public IDelete<T1> Delete<T1>(object dywhere) where T1 : class
        {
            return dao.Delete<T1>(dywhere);
        }

        public void Dispose()
        {
            dao.Dispose();
        }

        public IInsert<T1> Insert<T1>() where T1 : class
        {
            return dao.Insert<T1>();
        }

        public IInsert<T1> Insert<T1>(T1 source) where T1 : class
        {
            return dao.Insert<T1>(source);
        }

        public IInsert<T1> Insert<T1>(T1[] source) where T1 : class
        {
            return dao.Insert<T1>(source);
        }

        public IInsert<T1> Insert<T1>(IEnumerable<T1> source) where T1 : class
        {
            return dao.Insert<T1>(source);
        }

        public ISelect<T1> Select<T1>() where T1 : class
        {
            return dao.Select<T1>();
        }

        public ISelect<T1> Select<T1>(object dywhere) where T1 : class
        {
            return dao.Select<T1>(dywhere);
        }

        public void Transaction(Action handler)
        {
            dao.Transaction(handler);
        }

        public void Transaction(Action handler, TimeSpan timeout)
        {
            dao.Transaction(handler,timeout);
        }

        public IUpdate<T1> Update<T1>() where T1 : class
        {
           return dao.Update<T1>();
        }

        public IUpdate<T1> Update<T1>(object dywhere) where T1 : class
        {
            return dao.Update<T1>(dywhere);
        }
    }
}
