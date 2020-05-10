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
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };
            using (var httpClient = new HttpClient(handler))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var httpResponseMessage = httpClient.GetAsync(url).Result;
                var result= httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    return result;
                }
                else throw new HttpRequestException(httpResponseMessage.Content.ReadAsStringAsync().Result);
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
