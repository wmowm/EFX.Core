using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tibos.Test
{
    public class HttpCommon
    {
        /// <summary>  
        /// Http同步Post异步请求  
        /// </summary>  
        /// <param name="url">Url地址</param>  
        /// <param name="postStr">请求Url数据</param>  
        /// <param name="callBackUploadDataCompleted">回调事件</param>  
        /// <param name="encode"></param>  
        public static void HttpPostAsync(string url, string postStr = "",
            UploadDataCompletedEventHandler callBackUploadDataCompleted = null, Encoding encode = null)
        {
            var webClient = new WebClient { Encoding = Encoding.UTF8 };

            if (encode != null)
                webClient.Encoding = encode;

            var sendData = Encoding.UTF8.GetBytes(postStr);

            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            webClient.Headers.Add("ContentLength", sendData.Length.ToString(CultureInfo.InvariantCulture));

            if (callBackUploadDataCompleted != null)
                webClient.UploadDataCompleted += callBackUploadDataCompleted;

            webClient.UploadDataAsync(new Uri(url), "POST", sendData);
        }




        public static string Get(string func, string strParam = null)
        {
            string result = "";
            StringBuilder realUrl = new StringBuilder();
            realUrl.Append(func).Append("?");
            StringBuilder param = new StringBuilder();

            if (strParam != null)
            {
                if (param.Length > 0)
                {
                    param.Append("&");
                }
                param.Append(strParam);
            }
            realUrl.Append(param.ToString());
            WebRequest req = WebRequest.Create(realUrl.ToString());
            //req.ContentType = "text/html; charset=utf-8";
            
            WebResponse res = req.GetResponse();
            Stream stream = res.GetResponseStream();

            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }



        /// <summary>
        /// 异步请求get(UTF-8)
        /// </summary>
        /// <param name="url">链接地址</param>       
        /// <param name="formData">写在header中的内容</param>
        /// <returns></returns>
        public static async Task<string> HttpGetAsync(string url, List<KeyValuePair<string, string>> formData = null)
        {
            HttpClient httpClient = new HttpClient();
           
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,
            };
            var resp = await httpClient.SendAsync(request);
            resp.EnsureSuccessStatusCode();
            string token = await resp.Content.ReadAsStringAsync();

            return token;
        }
    }
}
