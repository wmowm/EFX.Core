using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tibos.Domain;
using Tibos.IRepository;
using Tibos.IService;
using Tibos.IService.Tibos;

namespace Tibos.Service
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> dao;
        public BaseService(IBaseRepository<T> dao)
        {
            this.dao = dao;
        }


        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="isBulkSave"></param>
        public void SaveChanges()
        {
            dao.SaveChanges();

        }

        public T Add(T entity, bool autoSave = true)
        {
            return dao.Add(entity, autoSave);
        }

        public void Add(IList<T> entities, bool autoSave = true)
        {
             dao.Add(entities, autoSave);
        }

        public Task<T> AddAsync(T entity, bool autoSave = true)
        {
            return dao.AddAsync(entity, autoSave);
        }

        public int Count()
        {
            return dao.Count();
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return dao.Count(expression);
        }

        public void Delete(string id, bool autoSave = true)
        {
             dao.Delete(id, autoSave);
        }

        public void Delete(IList<string> ids, bool autoSave = true)
        {
            dao.Delete(ids, autoSave);
        }

        public void Delete(T entity, bool autoSave = true)
        {
            dao.Delete(entity, autoSave);
        }

        public void Delete(IList<T> entities, bool autoSave = true)
        {
            dao.Delete(entities, autoSave);
        }

        public void Delete(Expression<Func<T, bool>> expression, bool autoSave = true)
        {
            dao.Delete(expression, autoSave);
        }

        public T Get(string id)
        {
          return dao.Get(id);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return dao.Get(expression);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return dao.GetAsync(expression);
        }

        public List<T> GetList()
        {
            return dao.GetList();
        }

        public List<T> GetList(Expression<Func<T, bool>> expression)
        {
            return dao.GetList(expression);
        }

        public List<T> GetList(Expression<Func<T, bool>> expression, int pageIndex, int pageSize)
        {
            return dao.GetList(expression, pageIndex, pageSize);
        }

        public bool IsExist(Expression<Func<T, bool>> expression)
        {
            return dao.IsExist(expression);
        }

        public void Update(T entity, bool autoSave = true)
        {
            dao.Update(entity, autoSave);
        }

        public void Update(IList<T> entities, bool autoSave = true)
        {
            dao.Update(entities, autoSave);
        }

        public Task UpdateAsync(T entity, bool autoSave = true)
        {
            return dao.UpdateAsync(entity, autoSave);
        }
    }
}
