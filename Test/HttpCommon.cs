using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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




        public static string HttpPost(string url, string strDate, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "post";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/x-www-form-urlencoded";
            var buffer = encoding.GetBytes(strDate);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback =
                        new RemoteCertificateValidationCallback(CheckValidationResult);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            if (responseStream == null) return string.Empty;
            using (var reader = new StreamReader(responseStream, encoding))
            {
                return reader.ReadToEnd();
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;// Always accept
        }






        public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode=null)
        {
            if (dataEncode == null)
                dataEncode = Encoding.UTF8;
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                
            }
            return ret;
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
            req.ContentType = "text/html; charset=utf-8";
            req.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjRCQTUyN0FDNkFEQTNCNDZGNTg5MDQzRjhCMDg2NzcwNTkxMzJCRjYiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJTNlVuckdyYU8wYjFpUVFfaXdobmNGa1RLX1kifQ.eyJuYmYiOjE1NTEzNDQ0ODksImV4cCI6MTU1MTM0ODA4OSwiaXNzIjoiaHR0cDovLzE5My4xMTIuMTA0LjEwMzo5MTExIiwiYXVkIjpbImh0dHA6Ly8xOTMuMTEyLjEwNC4xMDM6OTExMS9yZXNvdXJjZXMiLCJhZ2VudHNlcnZpY2UiXSwiY2xpZW50X2lkIjoianMiLCJzdWIiOiIxMDAwMyIsImF1dGhfdGltZSI6MTU1MTM0NDQ4OSwiaWRwIjoibG9jYWwiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiYWdlbnRzZXJ2aWNlIl0sImFtciI6WyJwd2QiXX0.FK37BlzKeyCVvoSFPxbmunGhRzP9_FBUXkCVg3sI3NsAwc_8cu_6clwTFP7QfKPRiymN6j9G_guDPwC1lGugbtUlY6KFpDVNL6A3mGuo-lc4VrGsn6yHzVbHsYDLr5o02fOolAXY6Y3jZigiJr3g2IOvAn8A3hyWmy8FI6OiJCGLxj_M-nJDQK7MtlLce9hbvD0S20MfLbvg86u1dN0qHhSpB_MkcSqFxhne-Uoi-Rq9tpxBjLYy0JzcbBn1aGDRwOP2CHgBVo9oKbQc8FqhwCpJx1q8_iSxNjYHOJvRj9u9vFZEozt4pSrFSYmwEzTDWFp0sik__mNbihIJc8kczw");
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
