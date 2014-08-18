using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace LightQueue
{
    public class QueueManager
    {
        private static readonly AutoResetEvent QueueTaskEvent = new AutoResetEvent(false);
        public static Queue<IQueueTask> QueueTasks { get; set; }
        private static Thread _workDispatcherThread = null;
        private static object _lockEnqueue = new object();
        private static bool _workNotOver = true;

        static QueueManager()
        {

        }

        public static void Init()
        {
            QueueTasks = new Queue<IQueueTask>();
            _workDispatcherThread = new Thread(Working);
            _workDispatcherThread.Start();
        }

        public static void StopWoking()
        {
            Debug.Write("Queue working stop");
            _workNotOver = false;
            QueueTasks = new Queue<IQueueTask>();
            QueueTaskEvent.Set();
        }

        public static void Enqueue(IQueueTask pQueueTask)
        {
            if (!_workNotOver) return;

            lock (_lockEnqueue)
            {
                QueueTasks.Enqueue(pQueueTask);
                QueueTaskEvent.Set();
            }
        }

        private static void Working()
        {
            try
            {
                while (true && _workNotOver)
                {
                    if (QueueTasks.Count == 0)
                    {
                        QueueTaskEvent.WaitOne();
                        continue;
                    }

                    IQueueTask task = QueueTasks.Dequeue();
                    task.Do();
                }
            }
            catch (Exception exp)
            {
                Debug.Print(JsonConvert.SerializeObject(exp));
            }
        }

    }
}
