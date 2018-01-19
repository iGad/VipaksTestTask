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
            if(!Flights.Any())
                throw new AppException("Расписание пустое");

        }
    }
}