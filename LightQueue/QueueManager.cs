using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using HZ.Logging.NlogWrapper;
using Newtonsoft.Json;

namespace LightQueue
{
    public class QueueManager
    {
        private static readonly AutoResetEvent QueueTaskEvent = new AutoResetEvent(false);
        public static Queue<IQueueTask> QueueTasks { get; set; }
        private static readonly object _lockEnqueue = new object();
        private static readonly object _lockDequeue = new object();
        private static bool _workNotOver = true;

        static QueueManager()
        {

        }

        public static void Init()
        {
            QueueTasks = new Queue<IQueueTask>();
            for (int i = 0; i < 30; i++)
            {
                StartTaskThread();
            }
        }

        public static void Enqueue(IQueueTask pQueueTask)
        {
            if (!_workNotOver) return;

            lock (_lockEnqueue)
            {
                try
                {
                    QueueTasks.Enqueue(pQueueTask);
                    QueueTaskEvent.Set();
                }
                catch (Exception exp)
                {
                    HZLogger.Fatal(string.Format("队列控制器发生异常 {0}", JsonConvert.SerializeObject(exp)));

                }
            }
        }

        private static void Working()
        {
            while (true && _workNotOver)
            {
                try
                {
                    if (QueueTasks.Count == 0)
                    {
                        QueueTaskEvent.WaitOne();
                        continue;
                    }

                    IQueueTask task = null;
                    lock (_lockDequeue)
                    {
                        //这边有可能拿到0数量
                        if (QueueTasks.Count == 0) continue;
                        task = QueueTasks.Dequeue();
                    }
                    task.Do();
                }
                catch (Exception exp)
                {
                    HZLogger.Error(string.Format("队列任务处理发生异常 {0}", JsonConvert.SerializeObject(exp)));
                }
            }
        }

        public static void StopWoking()
        {
            HZLogger.Info(string.Format("队列停止工作 还有{0}个任务未完成", QueueTasks.Count));
            _workNotOver = false;
            QueueTasks = new Queue<IQueueTask>();
            QueueTaskEvent.Set();
        }

        private static void StartTaskThread()
        {
            var task = new Thread(Working);
            task.Start();
        }

    }
}
