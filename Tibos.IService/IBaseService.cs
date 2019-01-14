using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tibos.Common;
using Tibos.Domain;

namespace Tibos.IService
{
    public interface IBaseService<T> where T : BaseEntity
    {

        /// <summary>
        /// 提交
        /// </summary>
        void SaveChanges();


        /// <summary>
        /// 获取所有列表信息
        /// </summary>
        /// <returns></returns>
        List<T> GetList();

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="expression">表达式条件</param>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="expression">表达式条件</param>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> expression, int pageIndex, int pageSize);

        PageResponse GetList(BaseDto dto);

        /// <summary>
        /// 添加实体信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        T Add(T entity, bool autoSave = true);

        /// <summary>
        /// 添加实体信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        Task<T> AddAsync(T entity, bool autoSave = true);


        /// <summary>
        /// 批量添加实体信息
        /// </summary>
        /// <param name="entities">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        void Add(IList<T> entities, bool autoSave = true);


        /// <summary>
        /// 修改数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        void Update(T entity, bool autoSave = true);

        /// <summary>
        /// 修改数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        Task UpdateAsync(T entity, bool autoSave = true);

        /// <summary>
        /// 批量修改实体信息
        /// </summary>
        /// <param name="entities">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        void Update(IList<T> entities, bool autoSave = true);

        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        void Delete(string id, bool autoSave = true);

        /// <summary>
        /// 批量删除数据信息
        /// </summary>
        /// <param name="ids">主键</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        void Delete(IList<string> ids, bool autoSave = true);

        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        void Delete(T entity, bool autoSave = true);

        /// <summary>
        /// 批量删除数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        void Delete(IList<T> entities, bool autoSave = true);

        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        void Delete(Expression<Func<T, bool>> expression, bool autoSave = true);
        /// <summary>
        /// 数据是否存在
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        bool IsExist(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 数据总数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 数据总数
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> expression);



        /// <summary>
        /// 获取单条数据信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>

        T Get(string id);
        /// <summary>
        /// 获取单条数据信息
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 获取单条数据信息
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
    }
}
