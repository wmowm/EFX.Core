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
	public interface OrganizationIService
	{
		#region 自定义
		 
        Organization Get(int id);

        IList<Organization> GetList(OrganizationRequest request);

        IList<Organization> GetList(Expression<Func<Organization, bool>> expression, List<SortOrder<Organization>> expressionOrder, Pagination pagination);

        int GetCount(OrganizationRequest request);

        string Save(Organization model);

        void Update(Organization model);


        void Delete(int id);

        bool Exists(int id);
        #endregion
     
   
	}
}