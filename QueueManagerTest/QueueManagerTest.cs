using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueConsole;

namespace QueueManagerTest
{
    [TestClass]
    public class QueueManagerTest
    {
        public static int runCount = 1000000;

        [TestMethod]
        public void TestMethod1()
        {
            QueueManager.Init();

            Thread t1 = new Thread(PushMessage);
            Thread t2 = new Thread(PushMail);
            Thread t3 = new Thread(PushOrder);

            t1.Start();
            t2.Start();
            t3.Start();

            Thread.Sleep(2000);

            while (QueueManager.QueueTasks.Count == 0)
            {

            }
        }

        public static void PushMessage()
        {
            for (int i = 0; i < runCount; i++)
            {
                Debug.Print("Message task {0} times ...", i);
                //Console.Read();
                QueueManager.Enqueue(new QueueTask() { Data = string.Format("I'm Message No.{0}", i) });
            }
        }

        public static void PushMail()
        {
            for (int i = 0; i < runCount; i++)
            {
                Debug.Print("Email task {0} times ...", i);
                //Console.Read();
                QueueManager.Enqueue(new QueueTask() { Data = string.Format("I'm Email No.{0}", i) });
            }
        }

        public static void PushOrder()
        {
            for (int i = 0; i < runCount; i++)
            {
                Debug.Print("Order task {0} times ...", i);
                //Console.Read();
                QueueManager.Enqueue(new QueueTask() { Data = string.Format("I'm Order No.{0}", i) });
            }
        }
    }
}
