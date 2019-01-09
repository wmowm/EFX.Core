using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;

namespace Tibos.Common
{
    public class Token
    {
        private readonly IMemoryCache _Cache;
        public Token()
        {

        }
        public Token(IMemoryCache memoryCache)
        {
            _Cache = memoryCache;
        }
        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="user_name"></param>
        /// <param name="password"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public string CreateToken(string user_name,string password,string sign)
        {
            //加密规则
            //1.用户名 + 密码  进行md5加密 得到加密字符串
            //2.加密字符串 + 签名进行md5加密  得到加密字符串2
            //3.加密字符串转大写
            //4.去掉字符串里面的-
            using (var md5 = MD5.Create())
            {
                //1.获取(用户名+密码)的md5加密字符串
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(user_name + password));
                string result1 = BitConverter.ToString(result);

                var result2 = md5.ComputeHash(Encoding.UTF8.GetBytes(result1 + sign));
                var strResult = BitConverter.ToString(result2);
                string result3 = strResult.Replace("-", "");
                return result3;
            }
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Json CheckToken(string token)
        {
            Json json = new Json();
            if (string.IsNullOrEmpty(token))
            {
                json.msg = "token不能为空!";
                json.status = -1;
                return json;
            }
            var id = _Cache.Get(token);
            if (id == null)
            {
                json.msg = "token已失效!";
                json.status = -1;
                return json;
            }
            return json;
        }

        /// <summary>
        /// 忽略Token验证
        /// </summary>
        /// <param name="ControllerName"></param>
        /// <param name="ActionName"></param>
        /// <returns></returns>
        public bool PassToken(string ControllerName,string ActionName)
        {
            if (ControllerName.ToLower() == "user" && ActionName.ToLower() == "gettoken") return true;
            if (ControllerName.ToLower() == "user" && ActionName.ToLower() == "checktoken") return true;

            if (ControllerName.ToLower() == "home" && ActionName.ToLower() == "get") return true;
            return false;
        }
    }
}
