using System;

namespace VipaksTestTask.Services
{
    public interface ITimeManager
    {
        int Multiplyer { get; set; }

        event EventHandler<TickEventArgs> Tick;

        void Start();
        void Stop();
    }
}