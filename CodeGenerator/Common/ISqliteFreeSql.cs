using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator.Common
{
    public interface ISqliteFreeSql : IDisposable
    {
        //
        // 摘要:
        //     数据库访问对象
        IAdo Ado { get; }
        //
        // 摘要:
        //     所有拦截方法都在这里
        IAop Aop { get; }
        //
        // 摘要:
        //     CodeFirst 模式开发相关方法
        ICodeFirst CodeFirst { get; }
        //
        // 摘要:
        //     DbFirst 模式开发相关方法
        IDbFirst DbFirst { get; }

        //
        // 摘要:
        //     删除数据
        //
        // 类型参数:
        //   T1:
        IDelete<T1> Delete<T1>() where T1 : class;
        //
        // 摘要:
        //     删除数据，传入动态对象如：主键值 | new[]{主键值1,主键值2} | TEntity1 | new[]{TEntity1,TEntity2} | new{id=1}
        //
        // 参数:
        //   dywhere:
        //     主键值、主键值集合、实体、实体集合、匿名对象、匿名对象集合
        //
        // 类型参数:
        //   T1:
        IDelete<T1> Delete<T1>(object dywhere) where T1 : class;
        //
        // 摘要:
        //     插入数据
        //
        // 类型参数:
        //   T1:
        IInsert<T1> Insert<T1>() where T1 : class;
        //
        // 摘要:
        //     插入数据，传入实体
        //
        // 参数:
        //   source:
        //
        // 类型参数:
        //   T1:
        IInsert<T1> Insert<T1>(T1 source) where T1 : class;
        //
        // 摘要:
        //     插入数据，传入实体数组
        //
        // 参数:
        //   source:
        //
        // 类型参数:
        //   T1:
        IInsert<T1> Insert<T1>(T1[] source) where T1 : class;
        //
        // 摘要:
        //     插入数据，传入实体集合
        //
        // 参数:
        //   source:
        //
        // 类型参数:
        //   T1:
        IInsert<T1> Insert<T1>(IEnumerable<T1> source) where T1 : class;
        //
        // 摘要:
        //     查询数据
        //
        // 类型参数:
        //   T1:
        ISelect<T1> Select<T1>() where T1 : class;
        //
        // 摘要:
        //     查询数据，传入动态对象如：主键值 | new[]{主键值1,主键值2} | TEntity1 | new[]{TEntity1,TEntity2} | new{id=1}
        //
        // 参数:
        //   dywhere:
        //     主键值、主键值集合、实体、实体集合、匿名对象、匿名对象集合
        //
        // 类型参数:
        //   T1:
        ISelect<T1> Select<T1>(object dywhere) where T1 : class;
        //
        // 摘要:
        //     开启事务（不支持异步），60秒未执行完将自动提交
        //
        // 参数:
        //   handler:
        //     事务体 () => {}
        void Transaction(Action handler);
        //
        // 摘要:
        //     开启事务（不支持异步）
        //
        // 参数:
        //   handler:
        //     事务体 () => {}
        //
        //   timeout:
        //     超时，未执行完将自动提交
        void Transaction(Action handler, TimeSpan timeout);
        //
        // 摘要:
        //     修改数据
        //
        // 类型参数:
        //   T1:
        IUpdate<T1> Update<T1>() where T1 : class;
        //
        // 摘要:
        //     修改数据，传入动态对象如：主键值 | new[]{主键值1,主键值2} | TEntity1 | new[]{TEntity1,TEntity2} | new{id=1}
        //
        // 参数:
        //   dywhere:
        //     主键值、主键值集合、实体、实体集合、匿名对象、匿名对象集合
        //
        // 类型参数:
        //   T1:
        IUpdate<T1> Update<T1>(object dywhere) where T1 : class;
    }
}
