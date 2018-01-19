using System;
using VipaksTestTask.Services;

namespace VipaksTestTask.Interfaces
{
    public interface ITimeManager
    {
        int Multiplyer { get; set; }

        event EventHandler<TickEventArgs> Tick;

        void Start();
        void Stop();
    }
}