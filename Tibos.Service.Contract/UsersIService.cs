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
	public interface UsersIService
	{
		#region 自定义
		 
        Users Get(int id);

        IList<Users> GetList();
		
        IList<Users> GetList(UsersRequest request);

        IList<Users> GetList(Expression<Func<Users, bool>> expression, List<SortOrder<Users>> expressionOrder, Pagination pagination);

        int GetCount(UsersRequest request);

        string Save(Users model);

        void Update(Users model);


        void Delete(int id);

        bool Exists(int id);
        #endregion
     
   
	}
}