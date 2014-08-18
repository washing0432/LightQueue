using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightQueue
{
    public interface IQueueTask : IDisposable
    {
        void Do();
        string Data { get; set; }
    }
}
