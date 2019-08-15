using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Model
{
    public class Body
    {
        /// <summary>
        /// 用户登录ID
        /// </summary>
        public string cs1 { get; set; }

        /// <summary>
        /// 事件发生时间时间戳（毫秒）
        /// </summary>
        public long tm { get; set; }

        /// <summary>
        /// 事件标识
        /// </summary>
        public string n { get; set; }

        /// <summary>
        /// 对象
        /// </summary>
        public DataTrading var { get; set; }

        /// <summary>
        /// 固定cstm
        /// </summary>
        public string t { get; set; }
    }

    public class DataTrading
    {
        /// <summary>
        /// 币币交易对
        /// </summary>
        public string tradingPair { get; set; }

        public int tradingPairNumber { get; set; }
    }
}
