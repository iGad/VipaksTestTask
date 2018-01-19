using System;
using VipaksTestTask.Models;
using VipaksTestTask.Services;

namespace VipaksTestTask.ViewModels
{
    public abstract class ScoreboardViewModel : ViewModel
    {
        protected AirportEngine Engine { get; }
        private int _lastFlightPassengersCount;
        private int _dayPassengersCount;
        private int _totalPassengersCount;
        private TimeSpan _lastFlightTime = TimeSpan.Zero;
        private string _name;

        protected ScoreboardViewModel(AirportEngine engine)
        {
            Engine = engine;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        public int LastFlightPassengersCount
        {
            get { return _lastFlightPassengersCount; }
            set
            {
                if (_lastFlightPassengersCount != value)
                {
                    _lastFlightPassengersCount = value;
                    RaisePropertyChanged(nameof(LastFlightPassengersCount));
                }
            }
        }

        public int DayPassengersCount
        {
            get { return _dayPassengersCount; }
            set {
                if (_dayPassengersCount != value)
                {
                    _dayPassengersCount = value;
                    RaisePropertyChanged(nameof(DayPassengersCount));
                }
            }
        }

        public int TotalPassengersCount
        {
            get { return _totalPassengersCount; }
            set {
                if (_totalPassengersCount != value)
                {
                    _totalPassengersCount = value;
                    RaisePropertyChanged(nameof(TotalPassengersCount));
                }
            }
        }

        protected virtual void OnFlightHappend(object sender, FlightEventArgs args)
        {
            LastFlightPassengersCount = args.FlightInfo.PassengerCount;
            TotalPassengersCount += LastFlightPassengersCount;
            if (_lastFlightTime > args.FlightInfo.Time)
            {
                DayPassengersCount = LastFlightPassengersCount;
            }
            else
            {
                DayPassengersCount += LastFlightPassengersCount;
            }
            _lastFlightTime = args.FlightInfo.Time;
        }
    }
}