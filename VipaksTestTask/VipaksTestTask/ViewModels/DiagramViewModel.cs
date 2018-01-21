using System;
using System.Collections.ObjectModel;
using System.Linq;
using VipaksTestTask.Models;
using VipaksTestTask.Services;

namespace VipaksTestTask.ViewModels
{
    /// <summary>
    /// Модель для получения и отображения данных о пассажирах за сутки
    /// </summary>
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
            Name = "Test";
        }

        public ObservableCollection<HourColumn> Columns { get; } = new ObservableCollection<HourColumn>();

        public string Name { get; set; }

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
            return Columns.Single(x =>  time >= x.From && time < x.To);
        }
    }
}
