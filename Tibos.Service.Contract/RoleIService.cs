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
	public interface RoleIService
	{
		#region 自定义
		 
        Role Get(string id);

        IList<Role> GetList(RoleRequest request);

        IList<Role> GetList(Expression<Func<Role, bool>> expression, List<SortOrder<Role>> expressionOrder, Pagination pagination);

        int GetCount(RoleRequest request);

        string Save(Role model);

        void Update(Role model);


        void Delete(string id);

        bool Exists(string id);
        #endregion
     
   
	}
}