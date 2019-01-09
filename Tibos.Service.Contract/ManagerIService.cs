using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Tibos.Common;
using Tibos.Domain;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Service.Contract
{
	public interface ManagerIService
	{
		#region 自定义
		 
        Manager Get(string id);

        IList<Manager> GetList(ManagerRequest request);

        IList<Manager> GetList(Expression<Func<Manager, bool>> expression, List<SortOrder<Manager>> expressionOrder, Pagination pagination);

        int GetCount(ManagerRequest request);

        string Save(Manager model);

        void Update(Manager model);


        void Delete(string id);

        bool Exists(string id);
        #endregion
     
   
	}
}