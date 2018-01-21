using System;
using VipaksTestTask.Models;
using VipaksTestTask.Services;

namespace VipaksTestTask.ViewModels
{
    /// <summary>
    /// Базовая вью-модель для сбора данных о пассажирах для табло
    /// </summary>
    public abstract class ScoreboardViewModel : ViewModel
    {
        protected AirportEngine Engine { get; }
        private int _lastFlightPassengersCount;
        private int _dayPassengersCount;
        private int _totalPassengersCount;
        private TimeSpan _lastFlightTime = TimeSpan.Zero;
        private string _title;

        protected ScoreboardViewModel(AirportEngine engine)
        {
            Engine = engine;
        }
        /// <summary>
        /// Заголовок табло
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }
        /// <summary>
        /// Кол-во пассажиров в последнем рейсе
        /// </summary>
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
        /// <summary>
        /// Кол-во пассажиров за сутки
        /// </summary>
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
        /// <summary>
        /// Кол-во пассажиров всего
        /// </summary>
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
            if (IsNewDay(args))
            {
                DayPassengersCount = LastFlightPassengersCount;
            }
            else
            {
                DayPassengersCount += LastFlightPassengersCount;
            }
            _lastFlightTime = args.FlightInfo.Time;
        }

        private bool IsNewDay(FlightEventArgs args)
        {
            return _lastFlightTime > args.FlightInfo.Time;
        }
    }
}