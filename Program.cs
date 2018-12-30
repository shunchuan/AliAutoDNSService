using System.ServiceProcess;

namespace AliAutoDNSService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main() {
            //AliAutoDNSService service = new AliAutoDNSService();
            //service.Test(null);

            //while (true)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //}
            //return;
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new AliAutoDNSService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}


