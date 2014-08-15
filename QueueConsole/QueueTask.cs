using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QueueConsole
{
    public class QueueTask : IQueueTask
    {
        public void Do()
        {
            Console.WriteLine("{0} queue task done", Thread.CurrentThread.Name);
        }
    }
}
