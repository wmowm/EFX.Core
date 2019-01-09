using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibos.Domain;
using Tibos.Common;
using Tibos.Repository.Contract;
using Tibos.Service.Contract;
using System.Linq.Expressions;


//Nhibernate Code Generation Template 1.0
//author:Tibos
//blog:www.cnblogs.com/Tibos
//Entity Code Generation Template
namespace Tibos.Service
{
	public class OrganizationService:OrganizationIService
	{
		private readonly IOrganization dao;
        public OrganizationService(IOrganization dao)
		{
            this.dao = dao;
        }
		//这个里面是通用方法,实现增删改查排序(动软代码生成器自动生成)
		#region  Method
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Organization Get(int id) 
        {
            return dao.Get(id);
        }
        public RequestParams GetWhere(OrganizationRequest request)
        {
            if (request == null) return null;
            RequestParams rp = new RequestParams();
            //追加查询参数
            //if (!string.IsNullOrEmpty(request.email))
            //{
            //    rp.Params.Add(new Params() { key = "email", value = request.email, searchType = EnumBase.SearchType.Eq });
            //}
            //添加排序(多个排序条件,可以额外添加)
            //if (!string.IsNullOrEmpty(request.sortKey))
            //{
            //    rp.Sort.Add(new Sort() { key = request.sortKey, searchType = (EnumBase.OrderType)request.sortType });
            //}
            //else
            //{
            //    rp.Sort = null;
            //}

            //添加分页
            if (request.pageIndex > 0)
            {
                rp.Paging.pageIndex = request.pageIndex;
                rp.Paging.pageSize = request.pageSize;
            }
            else
            {
                rp.Paging = null;
            }
            return rp;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IList<Organization> GetList(OrganizationRequest request) 
        {
            RequestParams rp = GetWhere(request);
            return dao.GetList(rp);
        }

        public IList<Organization> GetList(Expression<Func<Organization, bool>> expression, List<SortOrder<Organization>> expressionOrder, Pagination pagination)
        {
            return dao.GetList(expression, expressionOrder, pagination);
        }

        /// <summary>
        /// 获取当前条件下的总记录
        /// </summary>
        /// <returns></returns>
        public int GetCount(OrganizationRequest request)
        {
            RequestParams rp = GetWhere(request);
            return dao.GetCount(rp);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m_user"></param>
        /// <returns></returns>
        public string Save(Organization model) 
        {
             return dao.Save(model).ToString();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m_user"></param>
        /// <returns></returns>
        public void Update(Organization model) 
        {
            dao.Update(model);
        }

        public void Delete(int id)
        {
            dao.Delete(id);
        }

        public bool Exists(int id) 
        {
            return dao.Exists(id);
        }
		#endregion
      
   
	}
}