using VipaksTestTask.Models;

namespace VipaksTestTask.Interfaces
{
    /// <summary>
    /// Интерфейс поставщика расписания
    /// </summary>
    public interface IScheduleProvider
    {
        /// <summary>
        /// Получить расписание
        /// </summary>
        /// <returns></returns>
        Schedule GetSchedule();
    }
}