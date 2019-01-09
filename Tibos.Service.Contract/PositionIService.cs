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
	public interface PositionIService
	{
		#region 自定义
		 
        Position Get(int id);

        IList<Position> GetList(PositionRequest request);

        IList<Position> GetList(Expression<Func<Position, bool>> expression, List<SortOrder<Position>> expressionOrder, Pagination pagination);

        int GetCount(PositionRequest request);

        string Save(Position model);

        void Update(Position model);


        void Delete(int id);

        bool Exists(int id);
        #endregion
     
   
	}
}