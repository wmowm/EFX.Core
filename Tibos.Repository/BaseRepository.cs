using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tibos.Domain;
using Tibos.IRepository;

namespace Tibos.Repository.Tibos
{
    public class BaseRepository<T>: IBaseRepository<T> where T : BaseEntity
    {
        public virtual DbSet<T> Table { get; set; }
        public virtual BaseDbContext DbContent { get; set; }


        public BaseRepository()
        {

        }

        public BaseRepository(BaseDbContext dbContext)
        {
            this.DbContent = dbContext;
            this.Table = this.DbContent.Set<T>();
        }

        /// <summary>
        /// 提交
        /// </summary>
        public void SaveChanges(bool isBulkSave = true)
        {
            
            if (isBulkSave)
            {
                this.DbContent.BulkSaveChanges();
            }
            else
            {
                this.DbContent.SaveChanges();
            }

        }

        /// <summary>
        /// 获取所有列表信息
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            Table = DbContent.Set<T>();
            var list = Table.AsQueryable().ToList();
            return list;
        }

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="expression">表达式条件</param>
        /// <returns></returns>
        public virtual List<T> GetList(Expression<Func<T, bool>> expression)
        {
            return this.Table.Where(expression).ToList();
        }

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="expression">表达式条件</param>
        /// <returns></returns>
        public virtual List<T> GetList(Expression<Func<T, bool>> expression,int pageIndex,int pageSize)
        {
            return this.Table.Where(expression).ToList();
        }


        /// <summary>
        /// 添加实体信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        public virtual T Add(T entity, bool autoSave = true)
        {
            var result = this.Table.Add(entity).Entity;
            if (autoSave)
            {
                this.DbContent.BulkSaveChanges();
            }
            return result;
        }
        /// <summary>
        /// 添加实体信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        public virtual async Task<T> AddAsync(T entity, bool autoSave = true)
        {
            var result = (await this.Table.AddAsync(entity)).Entity;
            if (autoSave)
            {
                this.DbContent.BulkSaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 批量添加实体信息
        /// </summary>
        /// <param name="entities">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        public virtual void Add(IList<T> entities, bool autoSave = true)
        {
            this.Table.AddRange(entities);
            if (autoSave)
            {
                this.DbContent.BulkSaveChanges();
            }
        }

        /// <summary>
        /// 修改数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        public virtual void Update(T entity, bool autoSave = true)
        {
            this.DbContent.Update(entity);
            if (autoSave)
            {
                this.DbContent.BulkSaveChanges();
            }
        }

        /// <summary>
        /// 修改数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        public virtual async Task UpdateAsync(T entity, bool autoSave = true)
        {
            this.DbContent.Update(entity);
            if (autoSave)
            {
                await this.DbContent.BulkSaveChangesAsync();
            }
        }

        /// <summary>
        /// 批量修改实体信息
        /// </summary>
        /// <param name="entities">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        public virtual void Update(IList<T> entities, bool autoSave = true)
        {
            this.DbContent.UpdateRange(entities);
            if (autoSave)
            {
                this.DbContent.BulkSaveChanges();
            }
        }

        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        public virtual void Delete(string id, bool autoSave = true)
        {
            var entity = this.Table.First(f => f.Id == id);
            this.DbContent.Remove(entity);
            if (autoSave)
            {
                this.DbContent.BulkSaveChanges();
            }
        }

        /// <summary>
        /// 批量删除数据信息
        /// </summary>
        /// <param name="ids">主键</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        public virtual void Delete(IList<string> ids, bool autoSave = true)
        {
            var entities = this.Table.Where(f => ids.Contains(f.Id));
            this.DbContent.RemoveRange(entities);
            if (autoSave)
            {
                this.DbContent.BulkSaveChanges();
            }
        }

        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        public virtual void Delete(T entity, bool autoSave = true)
        {
            this.DbContent.Remove(entity);
            if (autoSave)
            {
                this.DbContent.BulkSaveChanges();
            }
        }

        /// <summary>
        /// 批量删除数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        public virtual void Delete(IList<T> entities, bool autoSave = true)
        {
            this.DbContent.RemoveRange(entities);
            if (autoSave)
            {
                this.DbContent.BulkSaveChanges();
            }
        }
        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="autoSave">是否自动保存，默认自动保存</param>
        public virtual void Delete(Expression<Func<T, bool>> expression, bool autoSave = true)
        {
            var entity = this.Table.Where(expression);
            this.DbContent.Remove(entity);
            if (autoSave)
            {
                this.DbContent.BulkSaveChanges();
            }
        }
        /// <summary>
        /// 数据是否存在
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        public virtual bool IsExist(Expression<Func<T, bool>> expression)
        {
            return this.Table.Any(expression);
        }

        /// <summary>
        /// 数据总数
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return this.Table.Count();
        }

        /// <summary>
        /// 数据总数
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> expression)
        {
            return this.Table.Count(expression);
        }



        /// <summary>
        /// 获取单条数据信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>

        public T Get(string id)
        {
            return this.Table.FirstOrDefault(f => f.Id == id);
        }
        /// <summary>
        /// 获取单条数据信息
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> expression)
        {
            return this.Table.FirstOrDefault(expression);
        }
        /// <summary>
        /// 获取单条数据信息
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await this.Table.FirstOrDefaultAsync(expression);
        }


    }
}
