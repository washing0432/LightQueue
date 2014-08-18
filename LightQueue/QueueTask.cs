using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using LightQueue;
using Newtonsoft.Json;

namespace LightQueue
{
    public class QueueTask : IQueueTask
    {
        public void Do()
        {
            string message = string.Empty;
            try
            {
                message = string.Format("Thread:{0} Queue task done. Data:{1}", Thread.CurrentThread.ManagedThreadId, this.Data);
            }
            catch (FormatException exp)
            {
                 Debug.Print(JsonConvert.SerializeObject(exp));
            }
            //string message = string.Format("Thread:{0} Queue task done. Data ...", Thread.CurrentThread.ManagedThreadId, this.Data);
            //Console.WriteLine(message);
            Debug.Print(message);
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
