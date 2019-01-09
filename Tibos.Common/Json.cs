using System;
using System.Collections.Generic;
using System.Text;

namespace Tibos.Common
{
    /// <summary>
    /// 返回的Json模型
    /// </summary>
    public class Json
    {
        private int _status = 0;
        /// <summary>
        /// 状态 -1失败,0成功
        /// </summary>
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string pitchId { get; set; }//要定位的页面元素id

        public string msg { get; set; }//消息


        public int code { get; set; } //状态码
        public string returnUrl { get; set; }//跳转的页面

        public object data { get; set; }//数据

        public int total { get; set; }//总条数
    }
}
