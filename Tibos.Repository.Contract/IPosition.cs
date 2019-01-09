using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibos.Domain;
using System.Linq.Expressions;
using Tibos.Common;

//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Repository.Contract
{
	 	//Position
	public interface IPosition
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(object id);

		/// <summary>
		/// 增加一条数据
		/// </summary>
		object  Save(Position model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		void Update(Position model);
		/// <summary>
		/// 删除数据
		/// </summary>
		void Delete(int id);
		/// <summary>
		/// 删除数据
		/// </summary>
		void Delete(Position model);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Position Get(object id);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		IList<Position> LoadAll();

        /// <summary>
        /// 条件查询
        /// </summary>
        IList<Position> GetList(RequestParams request);

        /// <summary>
        /// 查询条件
        /// </summary>
        IList<Position> GetList(Expression<Func<Position, bool>> expression, List<SortOrder<Position>> expressionOrder, Pagination pagination);
        

        /// <summary>
        /// 获取总条数
        /// </summary>
        int GetCount(RequestParams request);
        

		#endregion  成员方法
      	
   
	}
}