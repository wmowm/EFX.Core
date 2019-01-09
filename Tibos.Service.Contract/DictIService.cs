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
	public interface DictIService
	{
		#region 自定义
		 
        Dict Get(string id);

        IList<Dict> GetList();
		
        IList<Dict> GetList(DictRequest request);

        IList<Dict> GetList(Expression<Func<Dict, bool>> expression, List<SortOrder<Dict>> expressionOrder, Pagination pagination);

        int GetCount(DictRequest request);

        string Save(Dict model);

        void Update(Dict model);


        void Delete(string id);

        bool Exists(string id);
        #endregion
     
   
	}
}