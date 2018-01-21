using System.Collections.ObjectModel;
using VipaksTestTask.Interfaces;
using VipaksTestTask.Services;

namespace VipaksTestTask.ViewModels
{
    /// <summary>
    /// Основная вью-модель приложения, содержит дочерние модели и управляет запуском онлайн-табло и скоростью имитации
    /// </summary>
    public class MainViewModel : ViewModel
    {
        private readonly ITimeManager _timeManager;
        private readonly AirportEngine _engine;

        public MainViewModel(ITimeManager timeManager, AirportEngine engine, LastFlightViewModel lastFlightViewModel, ArrivalScoreboardViewModel arrivalScoreboard, DepartureScoreboardViewModel departureScoreboard, DiagramViewModel diagramViewModel)
        {
            _timeManager = timeManager;
            _engine = engine;
            LastFlightViewModel = lastFlightViewModel;
            ArrivalScoreboard = arrivalScoreboard;
            DepartureScoreboard = departureScoreboard;
            DiagramViewModel = diagramViewModel;
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
        /// <summary>
        /// Информация о последнем рейсе
        /// </summary>
        public LastFlightViewModel LastFlightViewModel { get; set; }
        /// <summary>
        /// Список возможных множителей времени выполнения
        /// </summary>
        public ObservableCollection<int> PossibleMultiplyers { get; set; }
        /// <summary>
        /// Вью-модель отвечающая за статистику прилетевших пассажиров
        /// </summary>
        public ArrivalScoreboardViewModel ArrivalScoreboard { get; set; }
        /// <summary>
        /// Вью-модель отвечающая за статистику вылетевших пассажиров
        /// </summary>
        public DepartureScoreboardViewModel DepartureScoreboard { get; set; }
        /// <summary>
        /// Вью-модель отвечающая за статистику пассажиров за сутки
        /// </summary>
        public DiagramViewModel DiagramViewModel { get; set; }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            _engine.Stop();
        }
    }
}
