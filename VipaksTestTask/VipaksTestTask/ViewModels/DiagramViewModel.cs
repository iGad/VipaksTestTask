using System;
using System.Collections.ObjectModel;
using System.Linq;
using VipaksTestTask.Services;

namespace VipaksTestTask.ViewModels
{
    public class HourColumn : ViewModel
    {
        private int _arrivedCount;
        private int _departuredCount;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }

        public int ArrivedCount
        {
            get { return _arrivedCount; }
            set
            {
                _arrivedCount = value;
                RaisePropertyChanged(nameof(ArrivedCount));
            }
        }

        public int DeparturedCount
        {
            get { return _departuredCount; }
            set
            {
                _departuredCount = value;
                RaisePropertyChanged(nameof(DeparturedCount));
            }
        }
    }
    public class DiagramViewModel : ViewModel
    {
        private readonly AirportEngine _engine;
        private TimeSpan _lastTime = TimeSpan.Zero;

        public DiagramViewModel(AirportEngine engine)
        {
            _engine = engine;
            _engine.PlaneArrived += OnPlaneArrived;
            _engine.PlaneDepartured += OnPlaneDepartured;
            InitColumns();
        }

        public ObservableCollection<HourColumn> Columns { get; } = new ObservableCollection<HourColumn>();

        private void InitColumns()
        {
            for (var i = 0; i < 24; i++)
            {
                Columns.Add(new HourColumn {From = TimeSpan.FromHours(i), To = TimeSpan.FromHours(i + 1).Subtract(TimeSpan.FromMilliseconds(1))});
            }
        }

        private void OnPlaneDepartured(object sender, FlightEventArgs flightEventArgs)
        {
            CheckDay(flightEventArgs);
            var column = FindColumn(flightEventArgs.FlightInfo.Time);
            column.DeparturedCount += flightEventArgs.FlightInfo.PassengerCount;
        }

        private void OnPlaneArrived(object sender, FlightEventArgs flightEventArgs)
        {
            CheckDay(flightEventArgs);
            var column = FindColumn(flightEventArgs.FlightInfo.Time);
            column.ArrivedCount += flightEventArgs.FlightInfo.PassengerCount;
        }

        private void CheckDay(FlightEventArgs flightEventArgs)
        {
            if (IsNewDay(flightEventArgs.FlightInfo.Time))
            {
                foreach (var column in Columns)
                {
                    column.DeparturedCount = 0;
                    column.ArrivedCount = 0;
                }
            }
            _lastTime = flightEventArgs.FlightInfo.Time;
        }

        private bool IsNewDay(TimeSpan flightInfoTime)
        {
            return _lastTime > flightInfoTime;
        }

        private HourColumn FindColumn(TimeSpan time)
        {
            return Columns.Single(x => x.From >= time && x.To < time);
        }
    }
}
