using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundWorker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service1() 
            };
            if (Environment.UserInteractive)
            {
                Console.Title = "BackgroundWorker";

                var type = typeof(ServiceBase);
                const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
                var method = type.GetMethod("OnStart", flags);

                foreach (var service in ServicesToRun)
                {
                    method.Invoke(service, new object[] { null });
                }

                Console.WriteLine("Service Started!");
                Console.ReadLine();
                ServicesToRun[0].Stop();
            }
            else
            {
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
