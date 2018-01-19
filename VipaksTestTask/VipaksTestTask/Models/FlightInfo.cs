using System;

namespace VipaksTestTask.Models
{
    /// <summary>
    /// Информация о состоявшемся рейсе
    /// </summary>
    public class FlightInfo : Flight
    {
        private int _passengerCount;

        public FlightInfo() { }

        public FlightInfo(Flight flight):base(flight)
        {
        }

        /// <summary>
        /// Количество пассажиров
        /// </summary>
        public int PassengerCount
        {
            get { return _passengerCount; }
            set
            {
                if(value < 0)
                    throw new ArgumentException();
                _passengerCount = value;
            }
        }
    }
}