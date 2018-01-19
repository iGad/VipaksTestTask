using System.Collections.Generic;
using System.Collections.ObjectModel;
using VipaksTestTask.Interfaces;
using VipaksTestTask.Models;
using VipaksTestTask.Services;

namespace VipaksTestTask.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly ITimeManager _timeManager;
        private readonly AirportEngine _engine;
        private string _lastFlightInfo;

        public MainViewModel(ITimeManager timeManager, AirportEngine engine, ArrivalScoreboardViewModel arrivalScoreboard, DepartureScoreboardViewModel departureScoreboard, DiagramViewModel diagramViewModel)
        {
            _timeManager = timeManager;
            _engine = engine;
            _engine.PlaneArrived += OnFlightHappend;
            _engine.PlaneDepartured += OnFlightHappend;
            ArrivalScoreboard = arrivalScoreboard;
            DepartureScoreboard = departureScoreboard;
            DiagramViewModel = diagramViewModel;
            Columns = new Dictionary<string, int>
            {
                {"1", 10},
                {"2", 10},
                {"3", 10},
                {"4", 10},
                {"5", 10},
                {"6", 10},
                {"7", 10},
                {"8", 10},
                {"9", 10},
                {"10", 10},
                {"11", 10},
                {"12", 10},
            };
            PossibleMultiplyers = new ObservableCollection<int>
            {
                1,
                10,
                100,
                1000,
                10000
            };
            _engine.Start();
        }

        private void OnFlightHappend(object sender, FlightEventArgs flightEventArgs)
        {
            var pattern = flightEventArgs.FlightInfo.FlightType == FlightType.Arrival ? Resources.LastArrivedPattern : Resources.LastDeparturedPattern;
            LastFlightInfo = string.Format(pattern, flightEventArgs.FlightInfo.City, flightEventArgs.FlightInfo.PlaneType.ToString(), flightEventArgs.FlightInfo.Time, flightEventArgs.FlightInfo.PassengerCount);
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

        public int TimeMultiplyer
        {
            get => _timeManager.Multiplyer;
            set
            {
                if (_timeManager.Multiplyer != value)
                {
                    _timeManager.Multiplyer = value;
                    RaisePropertyChanged(nameof(TimeMultiplyer));
                }
            }
        }

        public ObservableCollection<int> PossibleMultiplyers { get; set; }
        public Dictionary<string, int> Columns { get; set; }
        public ArrivalScoreboardViewModel ArrivalScoreboard { get; set; }
        public DepartureScoreboardViewModel DepartureScoreboard { get; set; }
        public DiagramViewModel DiagramViewModel { get; set; }
    }
}
