using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueConsole
{
    public interface IQueueTask : IDisposable
    {
        void Do();
        string Data { get; set; }
    }
}
