using System;
using System.Collections.Generic;
using System.Threading;

namespace QueueConsole
{
    public class QueueManager
    {
        private static AutoResetEvent QueueTaskEvent = new AutoResetEvent(false);
        private static AutoResetEvent workEvent = new AutoResetEvent(false);
        private static Queue<IQueueTask> QueueTasks { get; set; }
        private static Thread workThread = null;

        static QueueManager()
        {
            QueueTasks = new Queue<IQueueTask>();
            workThread = new Thread(DoingTask);
            workThread.Start();
        }

        public static void Enqueue(IQueueTask pQueueTask)
        {
            QueueTasks.Enqueue(pQueueTask);
            QueueTaskEvent.Set();
            Thread.Sleep(200);
        }

        public static void Working()
        {
            while (true)
            {
                if (QueueTasks.Count == 0) continue;
                workEvent.Set();
                QueueTaskEvent.WaitOne();
            }
        }

        public static void DoingTask()
        {
            while (true)
            {
                if (QueueTasks.Count == 0) return;
                IQueueTask task = QueueTasks.Dequeue();
                task.Do();

                workEvent.WaitOne();
            }
        }
    }
}
