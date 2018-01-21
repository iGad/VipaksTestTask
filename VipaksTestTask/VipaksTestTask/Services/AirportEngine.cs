using System;
using System.Linq;
using VipaksTestTask.Interfaces;
using VipaksTestTask.Models;

namespace VipaksTestTask.Services
{
    /// <summary>
    /// Основной сервис работы аэропорта. При каждом изменении времени находит рейсы, которые выполнились и генерирует соответствующие события прилета или вылета
    /// </summary>
    public class AirportEngine : IDisposable
    {
        private readonly IScheduleProvider _scheduleProvider;
        private readonly ITimeManager _timeManager;
        private readonly IPlaneCapacityProvider _planeCapacityProvider;
        private readonly Random _random = new Random();
        private Schedule _schedule;
        private TimeSpan _lastTime = new TimeSpan(0, 0, 0);
        private int _nextFlightIndex;

        public AirportEngine(IScheduleProvider scheduleProvider, ITimeManager timeManager, IPlaneCapacityProvider planeCapacityProvider)
        {
            _scheduleProvider = scheduleProvider;
            _timeManager = timeManager;
            _planeCapacityProvider = planeCapacityProvider;
            _timeManager.Tick += OnTimeManagerTick;
        }
        /// <summary>
        /// Событие о прибывшем самолете
        /// </summary>
        public event EventHandler<FlightEventArgs> PlaneArrived;
        /// <summary>
        /// Событие о вылетевшем самолете
        /// </summary>
        public event EventHandler<FlightEventArgs> PlaneDepartured;
        
        /// <summary>
        /// Запустить сервис
        /// </summary>
        public void Start()
        {
            _schedule = TryGetShedule();
            _schedule.Validate();
            _schedule.Flights = _schedule.Flights.OrderBy(x => x.Time).ToList();
            _timeManager.Start();
        }
        /// <summary>
        /// Остановить сервис
        /// </summary>
        public void Stop()
        {
            _timeManager.Stop();
        }

        protected virtual void OnPlaneArrived(FlightEventArgs e)
        {
            PlaneArrived?.Invoke(this, e);
        }

        protected virtual void OnPlaneDepartured(FlightEventArgs e)
        {
            PlaneDepartured?.Invoke(this, e);
        }

        private Schedule TryGetShedule()
        {
            try
            {
                return _scheduleProvider.GetSchedule();
            }
            catch 
            {
                throw new AppException(Resources.CanNotLoadSchedule);
            }
        }

        private void OnTimeManagerTick(object sender, TickEventArgs tickEventArgs)
        {
            var count = 0;
            while (count++ < _schedule.Flights.Count && IsFlightHappend(tickEventArgs))
            {
                FlightHappend(_schedule.Flights[_nextFlightIndex]);
                _nextFlightIndex = GetNextIndex(_nextFlightIndex);
            }
            _lastTime = tickEventArgs.Time;
        }

        private bool IsFlightHappend(TickEventArgs tickEventArgs)
        {
            var time = _schedule.Flights[_nextFlightIndex].Time;
            return time >= _lastTime && time <= tickEventArgs.Time || tickEventArgs.Time < _lastTime && (time >= _lastTime || time <= tickEventArgs.Time);
        }

        private void FlightHappend(Flight flight)
        {
            var flightInfo = new FlightInfo(flight) { PassengerCount = _random.Next(0, _planeCapacityProvider.GetPlaneCapacity(flight.PlaneType) + 1) };
            var eventArgs = new FlightEventArgs { FlightInfo = flightInfo };
            if (flight.FlightType == FlightType.Arrival)
                OnPlaneArrived(eventArgs);
            else
                OnPlaneDepartured(eventArgs);
        }

        private int GetNextIndex(int currentIndex)
        {
            if (currentIndex == _schedule.Flights.Count - 1)
                return 0;
            return currentIndex + 1;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
