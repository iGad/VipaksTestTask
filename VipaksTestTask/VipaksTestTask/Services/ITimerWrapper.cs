using System;

namespace VipaksTestTask.Services
{
    public interface ITimerWrapper
    {
        int Interval { get; set; }

        void Start(Action action);
        void Stop();
    }
}