using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
namespace AliAutoDNSService.Methods
{
    class HttpUtility
    {
        public static string Get(string url)
        {
            try
            {
                //Log.ConsoleWrite($"Get Url is {url}");
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //2020-11-15解决请求阿里云接口失败的问题，错误信息【基础连接已经关闭: 发送时发生错误】
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    //
                    var httpResponseMessage = httpClient.GetAsync(url).Result;
                    var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        return result;
                    }
                    else throw new HttpRequestException(httpResponseMessage.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                Log.ConsoleWrite("请求失败！");
                Log.ConsoleWriteNoDate($"Get Url is {url},Exception is {ex.Message}");
                throw ex;
            }
        }

        public static string GetExternalIp()
        {
            try
            {
                var url = "http://www.net.cn/static/customercare/yourip.asp?" + new Random().Next();
                string response = Get(url);
                string myReg = @"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))";
                Match mc = Regex.Match(response, myReg, RegexOptions.Singleline);
                if (mc.Success && mc.Groups.Count > 1)
                {
                    return mc.Value;
                }
                else
                {
                    Log.ConsoleWrite("获取公网IP失败！");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.ConsoleWrite("获取公网IP失败！");
                Log.ConsoleWriteNoDate(ex.Message);
                return null;
            }
        }
    }
}
