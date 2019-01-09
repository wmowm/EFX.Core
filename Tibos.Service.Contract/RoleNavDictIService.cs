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
	public interface RoleNavDictIService
	{
		#region 自定义
		 
        RoleNavDict Get(string id);

        IList<RoleNavDict> GetList();
		
        IList<RoleNavDict> GetList(RoleNavDictRequest request);

        IList<RoleNavDict> GetList(Expression<Func<RoleNavDict, bool>> expression, List<SortOrder<RoleNavDict>> expressionOrder, Pagination pagination);

        int GetCount(RoleNavDictRequest request);

        string Save(RoleNavDict model);

        void Update(RoleNavDict model);


        void Delete(string id);

        bool Exists(string id);
        #endregion
     
   
	}
}