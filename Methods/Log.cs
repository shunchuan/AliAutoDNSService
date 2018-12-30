//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Diagnostics;

//namespace AliAutoDNSService.Methods
//{
//    public class Log
//    {
//        public static void ConsoleWrite(string message, EventLogEntryType type=EventLogEntryType.Information)
//        {
//            var log = "【当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "】 " + message;
//            //eventLog1.WriteEntry(log, type);
//            Console.WriteLine(log);
//        }

//        public static void ConsoleWriteNoDate(string message, EventLogEntryType type = EventLogEntryType.Information)
//        {
//            //eventLog1.WriteEntry(message, type);
//            Console.WriteLine(message);
//        }

//        public static System.Diagnostics.EventLog eventLog1;
//    }
//}

using System;
using System.IO;

namespace AliAutoDNSService.Methods
{
    public class Log
    {
        public static string GetAppDomainPath => System.AppDomain.CurrentDomain.BaseDirectory;

        public static void ConsoleWrite(string info)
        {
            StreamWriter writer=null;
            FileStream fileStream = null;
            //CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            CreateDirectory(GetAppDomainPath);
            var logFile = GetAppDomainPath +"\\"+ DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(logFile);
                if (!fileInfo.Exists)
                {
                    fileStream = fileInfo.Create();
                    writer = new StreamWriter(fileStream);
                }
                else
                {
                    fileStream = fileInfo.Open(FileMode.Append, FileAccess.Write);
                    writer = new StreamWriter(fileStream);
                }

                var log = "【当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "】 " + info;
                writer.WriteLine(log);

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }

        public static void CreateDirectory(string infoPath)
        {
            if (!Directory.Exists(infoPath))
            {
                Directory.CreateDirectory(infoPath);
            }
        }

        public static void ConsoleWriteNoDate(string message)
        {
            ConsoleWrite(message);
        }
    }
}
