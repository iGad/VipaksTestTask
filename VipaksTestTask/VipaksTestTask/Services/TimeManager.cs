using System;

namespace VipaksTestTask.Services
{
    public class TickEventArgs : EventArgs
    {
        public TimeSpan Time { get; set; }
    }
    public class TimeManager : ITimeManager
    {
        public const int DefaultTimerInterval = 500;
        private readonly ITimerWrapper _timer;
        private TimeSpan _time = new TimeSpan(0, 0, 0);
        private int _multiplyer = 1;

        public TimeManager(ITimerWrapper timer)
        {
            _timer = timer;
            _timer.Interval = DefaultTimerInterval;
        }

        private void OnTimerElapsed()
        {
            _time += TimeSpan.FromMilliseconds(_timer.Interval * Multiplyer);
            if (_time.Days > 0)
                _time = _time - new TimeSpan(_time.Days, 0, 0, 0);
            OnTick(new TickEventArgs {Time = _time});
        }

        public int Multiplyer
        {
            get { return _multiplyer; }
            set
            {
                if(value < 1 || value > 10000)
                    throw new AppException("Неверное значение множителя");
                _multiplyer = value;
            }
        }

        public event EventHandler<TickEventArgs> Tick;

        protected virtual void OnTick(TickEventArgs e)
        {
            Tick?.Invoke(this, e);
        }

        public void Start()
        {
            _timer.Start(OnTimerElapsed);
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
