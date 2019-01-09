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
	public interface NavigationIService
	{
		#region 自定义
		 
        Navigation Get(string id);

        IList<Navigation> GetList();

        IList<Navigation> GetList(NavigationRequest request);

        IList<Navigation> GetList(Expression<Func<Navigation, bool>> expression, List<SortOrder<Navigation>> expressionOrder, Pagination pagination);

        int GetCount(NavigationRequest request);

        string Save(Navigation model);

        void Update(Navigation model);


        void Delete(string id);

        bool Exists(string id);
        #endregion
     
   
	}
}