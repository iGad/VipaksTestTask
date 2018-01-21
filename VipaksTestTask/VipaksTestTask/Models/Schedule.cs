using System;
using System.Collections.Generic;
using System.Linq;

namespace VipaksTestTask.Models
{
    /// <summary>
    /// Модель расписания
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// Рейсы
        /// </summary>
        public List<Flight> Flights { get; set; }

        public void Validate()
        {
            if (!Flights.Any())
                throw new AppException(Resources.SceduleIsEmpty);
            if (Flights.Any(x => x.Time >= TimeSpan.FromDays(1)))
                throw new AppException(Resources.ScheduleContainsInvalidTime);
            if (Flights.Any(x => string.IsNullOrWhiteSpace(x.City)))
                throw new AppException(Resources.ScheduleContainsInvalidCity);
        }
    }
}