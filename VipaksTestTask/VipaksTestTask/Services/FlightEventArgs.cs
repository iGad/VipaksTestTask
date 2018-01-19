using System;

namespace VipaksTestTask.Services
{
    public class FlightEventArgs : EventArgs
    {
        public FlightInfo FlightInfo { get; set; }
    }
}