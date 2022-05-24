using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;
using System.Reflection;
using System.IO;

namespace SaveMyDataService
{
    [RunInstaller(true)]
    public partial class Service1 : ServiceBase
    {
        int ScheduleTime = Convert.ToInt32(ConfigurationSettings.AppSettings["ThreadTime"]);

        public Thread Worker = null;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ThreadStart start = new ThreadStart(Working);
                Worker = new Thread(start);
                Worker.Start();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Working()
        {
            // TODO: logowanie do bazy
            while (true)
            {
                string logPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string logFile = "SaveMyDataLog.txt";

                using(StreamWriter writer = new StreamWriter($@"{logPath}\\{logFile}", true))
                {
                    writer.WriteLine($"SaveMyData Service called on: {DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")}{Environment.NewLine}");
                    writer.Close();
                }

                Thread.Sleep(ScheduleTime);
            }
        }
        protected override void OnStop()
        {
            try
            {
                if ((Worker != null) && (Worker.IsAlive))
                {
                    Worker.Abort();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
