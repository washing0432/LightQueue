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
        public int LoopCount = 1000;

        [TestMethod]
        public void Queue_MultThreadPushTest()
        {
            QueueManager.Init();

            for (int i = 0; i < 50; i++)
            {
                var imod = i % 3;
                switch (imod)
                {
                    case 0:
                        {
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



            Thread.Sleep(1000);

            while (QueueManager.QueueTasks.Count > 0)
            {

            }

            QueueManager.StopWoking();
        }



        public void PushMail()
        {
            for (int i = 0; i < this.LoopCount; i++)
            {
                Debug.Print("Thead:{0} Enqueue Mail {1}", Thread.CurrentThread.ManagedThreadId, i);
                QueueManager.Enqueue(new QueueTask() { Data = string.Concat("Mail No.", i) });
            }
        }

        public void PushMessage()
        {
            for (int i = 0; i < this.LoopCount; i++)
            {
                Debug.Print("Thead:{0} Enqueue Message {1}", Thread.CurrentThread.ManagedThreadId, i);
                QueueManager.Enqueue(new QueueTask() { Data = string.Concat("Message No.", i) });
            }
        }

        public void PushOrder()
        {
            for (int i = 0; i < this.LoopCount; i++)
            {
                Debug.Print("Thead:{0} Enqueue Order {1}", Thread.CurrentThread.ManagedThreadId, i);
                QueueManager.Enqueue(new QueueTask() { Data = string.Concat("Order No.", i) });
            }
        }
    }
}
