using System;

namespace VipaksTestTask.Models
{
    public class FlightEventArgs : EventArgs
    {
        public FlightInfo FlightInfo { get; set; }
    }
}