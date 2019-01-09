using System;
using System.Collections.Generic;
using System.Text;

namespace Tibos.Common
{
    public static class GuidExtensions
    {
        /// <summary> 
        /// 根据GUID获取16位的唯一字符串 
        /// </summary> 
        /// <param name=\"guid\"></param> 
        /// <returns></returns> 
        public static string GuidTo16String(this Guid guid)
        {
            string base64 = Convert.ToBase64String(guid.ToByteArray());

            string encoded = base64.Replace("/", "_").Replace("+", "-");

            return encoded.Substring(0, 22);
        }
        /// <summary> 
        /// 根据GUID获取19位的唯一数字序列 
        /// </summary> 
        /// <returns></returns> 
        public static long GuidToLongID(this Guid guid)
        {
            byte[] buffer = guid.ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

    }
}
