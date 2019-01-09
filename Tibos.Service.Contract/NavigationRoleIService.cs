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
	public interface NavigationRoleIService
	{
		#region 自定义
		 
        NavigationRole Get(string id);

        IList<NavigationRole> GetList();
		
        IList<NavigationRole> GetList(NavigationRoleRequest request);

        IList<NavigationRole> GetList(Expression<Func<NavigationRole, bool>> expression, List<SortOrder<NavigationRole>> expressionOrder, Pagination pagination);

        int GetCount(NavigationRoleRequest request);

        string Save(NavigationRole model);

        void Update(NavigationRole model);


        void Delete(string id);

        bool Exists(string id);
        #endregion
     
   
	}
}