using System;

namespace VipaksTestTask.Services
{
    public class TickEventArgs : EventArgs
    {
        public TimeSpan Time { get; set; }
    }
}