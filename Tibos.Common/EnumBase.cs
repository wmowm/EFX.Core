using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Tibos.Common
{
    public class EnumBase
    {
        private static List<Dictionary<string, int>> list_enum;
        //单例模式,初始化枚举对象
        public EnumBase()
        {
            if (list_enum == null)
            {
                list_enum = new List<Dictionary<string, int>>();
                list_enum.Add(GetEnum(typeof(Authorize)));
                list_enum.Add(GetEnum(typeof(BackupType)));
                list_enum.Add(GetEnum(typeof(RecommendType)));
                list_enum.Add(GetEnum(typeof(SearchType)));
                list_enum.Add(GetEnum(typeof(ExamType)));
                list_enum.Add(GetEnum(typeof(StudentType)));
            }
        }
        public enum Authorize
        {
            /// <summary>
            /// 查看
            /// </summary>
            [Description("查看")]
            查看 = 1,

            /// <summary>
            /// 修改
            /// </summary>
            [Description("修改")]
            修改 = 2,

            /// <summary>
            /// 添加
            /// </summary>
            [Description("添加")]
            添加 = 3,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("删除")]
            删除 = 4,

            /// <summary>
            /// 审核
            /// </summary>
            [Description("审核")]
            审核 = 5,

            /// <summary>
            /// 登录
            /// </summary>
            [Description("登录")]
            登录 = 6,

            /// <summary>
            /// 下载
            /// </summary>
            [Description("下载")]
            下载 = 7
        }

        public enum BackupType
        {
            /// <summary>
            /// 完整备份
            /// </summary>
            [Description("完整备份")]


            完整备份 = 1,
            /// <summary>
            /// 差异备份
            /// </summary>
            [Description("差异备份")]
            差异备份 = 2
        }

        public enum RecommendType
        {
            [Description("置顶")]
            置顶 = 1,
            [Description("推荐")]
            推荐 = 2,
            [Description("热门")]
            热门 = 3,
            [Description("允许评论")]
            允许评论 = 4
        }

        public enum SearchType
        {
            /// <summary>
            ///  等于
            /// </summary>
            Eq = 1,
            /// <summary>
            /// 大于
            /// </summary>
            Gt = 2,
            /// <summary>
            /// 大于等于
            /// </summary>
            Ge = 3,
            /// <summary>
            /// 小于
            /// </summary>
            Lt = 4,
            /// <summary>
            /// 小于等于
            /// </summary>
            Le = 5,
            /// <summary>
            /// 等于空值
            /// </summary>
            IsNull = 6,
            /// <summary>
            ///  非空值
            /// </summary>
            IsNotNull = 7,
            /// <summary>
            /// 模糊查询 xx%
            /// </summary>
            Like = 8,
            /// <summary>
            /// 模糊查询 %xx
            /// </summary>
            StartLike = 9,
            /// <summary>
            /// 等于列表中的某一个值
            /// </summary>
            In = 10,
            /// <summary>
            /// 不等于列表中任意一个值
            /// </summary>
            NotIn = 11,
            /// <summary>
            /// 分页{pageindex,pagesize}
            /// </summary>
            Paging = 12,
            /// <summary>
            /// 分组
            /// </summary>
            Group = 13
        }

        public enum ExamType
        {
            /// <summary>
            /// 未审核
            /// </summary>
            未审核 = 0,
            /// <summary>
            /// 成功
            /// </summary>
            成功 = 1,
            /// <summary>
            /// 失败
            /// </summary>
            失败 = 2
        }

        public enum StudentType
        {
            /// <summary>
            /// 未缴费
            /// </summary>
            未缴费 = 1,
            /// <summary>
            ///已缴费
            /// </summary>
            已缴费 = 2,
            /// <summary>
            /// 学习期
            /// </summary>
            学习期 = 3,
            /// <summary>
            ///就业期
            /// </summary>
            就业期 = 4,
            /// <summary>
            /// 其它
            /// </summary>
            其它 = 5

        }

        public enum OrderType 
        {
            /// <summary>
            /// 正序
            /// </summary>
            Asc = 0,
            /// <summary>
            /// 倒序
            /// </summary>
            Desc = 1
        }

        /// <summary>
        /// 枚举转字典
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetEnum(Type t)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var item in Enum.GetValues(t))
            {
                dic.Add(item.ToString(), Convert.ToInt32(item));
            }
            return dic;
        }

        /// <summary>
        /// 获取所有枚举的集合
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, int>> GetList_Enum()
        {
            return list_enum;
        }

        /// <summary>
        /// 获取枚举类型在集合里的索引
        /// </summary>
        /// <returns></returns>
        public int GetEnumIndex(Type t)
        {
            var res = GetEnum(t);
            return list_enum.IndexOf(res);
        }
    }
}
