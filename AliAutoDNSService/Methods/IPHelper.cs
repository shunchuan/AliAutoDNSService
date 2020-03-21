using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

namespace AliAutoDNSService.Methods
{
    public class IPHelper
    {
        public string getLocalIP(string apiUrl)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(apiUrl);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                responseString = responseString.Substring(1);
                responseString = responseString.Substring(0, responseString.Length - 1);
                return responseString;
            }catch(Exception ex)
            {
                Log.ConsoleWrite("获取公网IP失败！");
                Log.ConsoleWriteNoDate(ex.Message);
                return null;
            }
        }

        public string GetExternalIp()
        {
            try
            {
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.Default;
                client.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
                client.Headers.Add("X-Requested-With: XMLHttpRequest");
                //string response = client.DownloadString("https://www.baidu.com/s?wd=ip");//百度
                //string myReg = "(?<=fk=\")(\\d|\\.)+";
                string response = client.DownloadString("http://www.net.cn/static/customercare/yourip.asp");//百度
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
