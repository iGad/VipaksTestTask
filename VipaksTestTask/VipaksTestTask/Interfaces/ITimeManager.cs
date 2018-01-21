using System;
using VipaksTestTask.Services;

namespace VipaksTestTask.Interfaces
{
    /// <summary>
    /// Интерфейс, описывающий работу класса, который контролирует ход времени в приложении
    /// </summary>
    public interface ITimeManager
    {
        /// <summary>
        /// Множитель по отношению к реальному времени
        /// </summary>
        int Multiplyer { get; set; }
        /// <summary>
        /// Событие об изменении времени
        /// </summary>
        event EventHandler<TickEventArgs> Tick;
        /// <summary>
        /// Запуск
        /// </summary>
        void Start();
        /// <summary>
        /// Остановка
        /// </summary>
        void Stop();
    }
}