using VipaksTestTask.Models;
using VipaksTestTask.Services;

namespace VipaksTestTask.ViewModels
{
    /// <summary>
    /// Модель для получения данных о последнем совершенном рейсе
    /// </summary>
    public class LastFlightViewModel : ViewModel
    {
        private readonly AirportEngine _engine;
        private string _lastFlightInfo;

        public LastFlightViewModel(AirportEngine engine)
        {
            _engine = engine;
            _engine.PlaneArrived += OnFlightHappend;
            _engine.PlaneDepartured += OnFlightHappend;
        }

        public string LastFlightInfo
        {
            get { return _lastFlightInfo; }
            set
            {
                if (_lastFlightInfo != value)
                {
                    _lastFlightInfo = value;
                    RaisePropertyChanged(nameof(LastFlightInfo));
                }

            }
        }

        private void OnFlightHappend(object sender, FlightEventArgs flightEventArgs)
        {
            var pattern = flightEventArgs.FlightInfo.FlightType == FlightType.Arrival ? Resources.LastArrivedPattern : Resources.LastDeparturedPattern;
            LastFlightInfo = string.Format(pattern, flightEventArgs.FlightInfo.City, flightEventArgs.FlightInfo.PlaneType.ToString(), flightEventArgs.FlightInfo.Time, flightEventArgs.FlightInfo.PassengerCount);
        }

    }
}
