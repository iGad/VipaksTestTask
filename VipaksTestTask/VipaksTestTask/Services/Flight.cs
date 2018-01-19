using System;

namespace VipaksTestTask.Services
{
    public class Flight
    {
        public Flight(){}

        public Flight(Flight flight)
        {
            PlaneType = flight.PlaneType;
            EventType = flight.EventType;
            Time = flight.Time;
            City = flight.City;
        }
        public PlaneType PlaneType { get; set; }
        public EventType EventType { get; set; }
        public TimeSpan Time { get; set; }
        public string City { get; set; }
    }
}