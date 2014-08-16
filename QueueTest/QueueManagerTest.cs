using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueConsole;

namespace QueueTest
{
    [TestClass]
    public class QueueManagerTest
    {
        public int LoopCount = 500;

        [TestMethod]
        public void Queue_MultThreadPushTest()
        {
            QueueManager.Init();

            Thread th1 = new Thread(PushMail);
            Thread th2 = new Thread(PushMessage);
            Thread th3 = new Thread(PushOrder);
            th1.Start();
            th2.Start();
            th3.Start();

            Thread.Sleep(1000);

            while (QueueManager.QueueTasks.Count>0)
            {
                
            }
            
            QueueManager.StopWoking();
        }

        public void PushMail()
        {
            for (int i = 0; i < this.LoopCount; i++)
            {
                QueueManager.Enqueue(new QueueTask() { Data = string.Concat("Mail No.", i) });
            }
        }

        public void PushMessage()
        {
            for (int i = 0; i < this.LoopCount; i++)
            {
                QueueManager.Enqueue(new QueueTask() { Data = string.Concat("Message No.", i) });
            }
        }

        public void PushOrder()
        {
            for (int i = 0; i < this.LoopCount; i++)
            {
                QueueManager.Enqueue(new QueueTask() { Data = string.Concat("Order No.", i) });
            }
        }
    }
}
