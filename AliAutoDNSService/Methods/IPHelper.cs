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
        private int Error_Times = 0;

        public string GetExternalIp()
        {
            try
            {
                //WebClient client = new WebClient();
                //client.Encoding = System.Text.Encoding.Default;
                //client.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
                //client.Headers.Add("X-Requested-With: XMLHttpRequest");
                //string response = client.DownloadString("https://www.baidu.com/s?wd=ip");//百度
                //string myReg = "(?<=fk=\")(\\d|\\.)+";
                //string response = client.DownloadString("http://www.net.cn/static/customercare/yourip.asp?"+new Random().Next());//百度
                var url = "http://www.net.cn/static/customercare/yourip.asp?" + new Random().Next();
                string response = HttpUtility.Get(url);
                string myReg = @"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))";
                Match mc = Regex.Match(response, myReg, RegexOptions.Singleline);
                if (mc.Success && mc.Groups.Count > 1)
                {
                    Error_Times = 0;
                    return mc.Value;
                }
                else
                {
                    Error_Times++;
                    Log.ConsoleWrite("获取公网IP失败！");
                    if (Error_Times == 10)
                    {
                        Log.ConsoleWrite(response);
                        Error_Times = 0;

                    }
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
