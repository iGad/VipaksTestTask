using System;

namespace VipaksTestTask.Models
{
    /// <summary>
    /// Модель рейса в расписании
    /// </summary>
    public class Flight
    {
        public Flight(){}

        public Flight(Flight flight)
        {
            PlaneType = flight.PlaneType;
            FlightType = flight.FlightType;
            Time = flight.Time;
            City = flight.City;
        }
        /// <summary>
        /// Тип самолета
        /// </summary>
        public PlaneType PlaneType { get; set; }
        /// <summary>
        /// Тип рейса
        /// </summary>
        public FlightType FlightType { get; set; }
        /// <summary>
        /// Время рейса
        /// </summary>
        public TimeSpan Time { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
    }
}