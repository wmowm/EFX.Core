using System;
using System.Collections.Generic;
using System.Globalization;
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




        /// <summary>  
        ///  Http同步Post同步请求  
        /// </summary>  
        /// <param name="url">Url地址</param>  
        /// <param name="postStr">请求Url数据</param>  
        /// <param name="encode">编码(默认UTF8)</param>  
        /// <returns></returns>  
        public static string HttpPost(string url, string postStr = "", Encoding encode = null)
        {
            string result;
            try
            {
                var webClient = new WebClient { Encoding = Encoding.UTF8 };

                if (encode != null)
                    webClient.Encoding = encode;

                var sendData = Encoding.UTF8.GetBytes(postStr);

                webClient.Headers.Add("Content-Type", "application/json; charset=UTF-8");
                webClient.Headers.Add("ContentLength", sendData.Length.ToString(CultureInfo.InvariantCulture));
                //webClient.Headers.Add("X-Compress-Codes", "0");

                var readData = webClient.UploadData(url, "POST", sendData);

                result = Encoding.UTF8.GetString(readData);

            }
            catch (Exception ex)
            {
                result = ex.Message;
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
