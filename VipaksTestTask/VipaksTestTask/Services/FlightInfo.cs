using System;

namespace VipaksTestTask.Services
{
    public class FlightInfo : Flight
    {
        private int _passengerCount;

        public FlightInfo() { }

        public FlightInfo(Flight flight):base(flight)
        {
        }

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