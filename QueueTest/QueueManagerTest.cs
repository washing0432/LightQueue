using System;
using System.Diagnostics;
using System.Threading;
using LightQueue;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QueueTest
{
    [TestClass]
    public class QueueManagerTest
    {
        /*
         * mod      threads      loops       totaltimes      spend(ms)
         * 线程池   10           1000        10000           18380
         * 普通     10           1000        10000           23483.3481 
         * 
         * 线程池   50           1000        50000           90247.0238
         * 普通     50           1000        50000           94951.4942
         * 
         * 线程池   50           10000       500000          866500.6414
         * 普通     50           10000       500000          857228.7143
         * 
         * 线程池   100          10000       1000000         1560621.3898
         * 1秒500次任务+ 0错误
         */

        public int LoopCount = 10000;

        [TestMethod]
        public void Queue_MultThreadPushTest()
        {
            QueueManager.Init();

            for (int i = 0; i < 10; i++)
            {
                var imod = i % 3;
                switch (imod)
                {
                    case 0:
                        {
                            //var callback = new WaitCallback(PushMail);
                            //ThreadPool.QueueUserWorkItem(callback);
                            Thread th1 = new Thread(PushMail);
                            th1.Start();
                            break;
                        }
                    case 1:
                        {
                            Thread th2 = new Thread(PushMessage);
                            th2.Start();
                        }
                        break;
                    case 2:
                        {
                            Thread th3 = new Thread(PushOrder);
                            th3.Start();
                        }
                        break;
                }
            }


            bool go = true;
            while (go)
            {

            }

            //QueueManager.StopWoking();
        }



        public void PushMail()
        {
            for (int i = 0; i < this.LoopCount; i++)
            {
                Debug.Print("Thead:{0} Enqueue Mail {1}", Thread.CurrentThread.ManagedThreadId, i);
                QueueManager.Enqueue(new QueueTask() { Data = string.Concat("Mail No.", i) });
            }

            QueueManager.StopWoking();
        }

        public void PushMessage()
        {
            for (int i = 0; i < this.LoopCount; i++)
            {
                Debug.Print("Thead:{0} Enqueue Message {1}", Thread.CurrentThread.ManagedThreadId, i);
                QueueManager.Enqueue(new QueueTask() { Data = string.Concat("Message No.", i) });
            }

            QueueManager.StopWoking();
        }

        public void PushOrder()
        {
            for (int i = 0; i < this.LoopCount; i++)
            {
                Debug.Print("Thead:{0} Enqueue Order {1}", Thread.CurrentThread.ManagedThreadId, i);
                QueueManager.Enqueue(new QueueTask() { Data = string.Concat("Order No.", i) });
            }

            QueueManager.StopWoking();
        }
    }
}
