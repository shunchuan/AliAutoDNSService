using AliAutoDNSService.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AliAutoDNSService
{
    public partial class AliAutoDNSService : ServiceBase
    {
        private Thread worker;
        public AliAutoDNSService()
        {
            InitializeComponent();
            Log.ConsoleWrite("初始化成功");

            //eventLog1 = new System.Diagnostics.EventLog();
            //if (!System.Diagnostics.EventLog.SourceExists("AliAutoDNSService"))
            //{
            //    //EventSourceCreationData mySourceData = new EventSourceCreationData("AliAutoDNSService", "LogAliAutoDNSService");
            //    //mySourceData.MessageResourceFile = messageFile;
            //    //mySourceData.CategoryResourceFile = messageFile;
            //    //mySourceData.CategoryCount = CategoryCount;
            //    //mySourceData.ParameterResourceFile = messageFile;
            //    System.Diagnostics.EventLog.CreateEventSource("AliAutoDNSService", "LogAliAutoDNSService");

            //    Log.ConsoleWrite("CreateEventSource成功");
            //}
            //Log.ConsoleWrite("初始化成功");
            //eventLog1.Source = "AliAutoDNSService";
            //eventLog1.Log = "LogAliAutoDNSService";
            //eventLog1.WriteEntry("初始化成功", EventLogEntryType.Information);
            //Methods.Log.eventLog1 = eventLog1;
            //Methods.Log.ConsoleWrite("初始化成功");
        }



public void Test(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                worker = new Thread(new Methods.ToDo().Run);
                worker.IsBackground = true;
                worker.Start();
            }
            catch (Exception ex)
            {
                Log.ConsoleWrite(ex.StackTrace + ex.Message);
                OnStop();
            }
        }

        protected override void OnStop()
        {
            try
            {
                Log.ConsoleWrite("服务开始停止");
                worker.Abort();
                Log.ConsoleWrite("服务已停止");
                base.OnStop();
            }
            catch (Exception ex)
            {
                Log.ConsoleWrite(ex.StackTrace+ ex.Message);
            }
        }
    }
}
