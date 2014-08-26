using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using HZ.Logging.NlogWrapper;
using LightQueue;
using Newtonsoft.Json;

namespace QueueTest
{
    public class QueueTask : IQueueTask
    {
        public void Do()
        {
            string message = string.Empty;
            try
            {
                message = string.Format(" QueueTask Done. Data:{0}", this.Data);
                HZLogger.Info(message);
            }
            catch (FormatException exp)
            {
                HZLogger.Error(exp);
            }
            catch (Exception exp)
            {
                HZLogger.Error(exp);
            }
        }


        public string Data
        {
            get;
            set;
        }

        public void Dispose()
        {
            Debug.Print("Task data:{0} has been released", this.Data);
        }
    }
}
