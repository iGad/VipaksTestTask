using System.Collections.Generic;
using System.Linq;

namespace VipaksTestTask.Services
{
    public class Schedule
    {
        public List<Flight> Flights { get; set; }

        public void Validate()
        {
            if(!Flights.Any())
                throw new AppException("Расписание пустое");

        }
    }
}