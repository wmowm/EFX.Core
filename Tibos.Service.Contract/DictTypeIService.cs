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
	public interface DictTypeIService
	{
		#region 自定义
		 
        DictType Get(string id);

        IList<DictType> GetList();
		
        IList<DictType> GetList(DictTypeRequest request);

        IList<DictType> GetList(Expression<Func<DictType, bool>> expression, List<SortOrder<DictType>> expressionOrder, Pagination pagination);

        int GetCount(DictTypeRequest request);

        string Save(DictType model);

        void Update(DictType model);


        void Delete(string id);

        bool Exists(string id);
        #endregion
     
   
	}
}