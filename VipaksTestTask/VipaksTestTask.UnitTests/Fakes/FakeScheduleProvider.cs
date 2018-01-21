using System;
using System.Collections.Generic;
using VipaksTestTask.Interfaces;
using VipaksTestTask.Models;

namespace VipaksTestTask.UnitTests.Fakes
{
    class FakeScheduleProvider : IScheduleProvider
    {
        public int FlightCount { get; set; } = 10;
        public int FlightPerHour { get; set; } = 2;

        public Schedule Shedule { get; set; }

        public Schedule GetSchedule()
        {
            if (Shedule != null)
                return Shedule;
            var schedule = new Schedule();
            schedule.Flights = new List<Flight>();
            var i = 0;
            var planeTypeCount = Enum.GetNames(typeof(PlaneType)).Length;
            while (i < FlightCount)
            {
                for (var j = 0; j < FlightPerHour && i < FlightCount; j++)
                {
                    schedule.Flights.Add(new Flight
                    {
                        Time = new TimeSpan(i, j * 60 / FlightPerHour, 0),
                        PlaneType = (PlaneType) (i % planeTypeCount),
                        FlightType = (FlightType) (i % 2),
                        City = "test" + i
                    });
                    i++;
                }
            }
           
            return schedule;
        }
    }
}
