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
        public PageResponse CheckToken(string token)
        {
            PageResponse json = new PageResponse();
            if (string.IsNullOrEmpty(token))
            {
                json.msg = "token不能为空!";
                json.status = -1;
                json.code = StatusCodeDefine.Unauthorized;
                return json;
            }
            var id = _Cache.Get(token);
            if (id == null)
            {
                json.msg = "token已失效!";
                json.status = -1;
                json.code = StatusCodeDefine.Unauthorized;
                return json;
            }
            return json;
        }

    }
}
