using System;

namespace VipaksTestTask.Interfaces
{
    /// <summary>
    /// Интерфейс для таймера
    /// </summary>
    public interface ITimerWrapper
    {
        int Interval { get; set; }

        void Start(Action action);
        void Stop();
    }
}