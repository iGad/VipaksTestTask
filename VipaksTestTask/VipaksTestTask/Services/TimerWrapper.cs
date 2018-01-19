using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace VipaksTestTask.Services
{
    public class TimerWrapper : ITimerWrapper
    {
        private readonly Timer _timer = new Timer();
        private Action _action;

        public TimerWrapper()
        {
            _timer.Elapsed += OnTimerElapsed;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _action();
        }

        public int Interval { get; set; }
        
        public void Start(Action action)
        {
            _action = action ?? throw new ArgumentException(nameof(action));
            _timer.Interval = Interval;
            _timer.Start();
        }
        
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
