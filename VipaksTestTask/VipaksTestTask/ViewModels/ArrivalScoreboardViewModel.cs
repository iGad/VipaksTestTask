using VipaksTestTask.Services;

namespace VipaksTestTask.ViewModels
{
    /// <summary>
    /// Модель для получения и отображения данных о прибывших пассажирах
    /// </summary>
    public class ArrivalScoreboardViewModel : ScoreboardViewModel
    {
        public ArrivalScoreboardViewModel(AirportEngine engine) : base(engine)
        {
            engine.PlaneArrived += OnFlightHappend;
            Title = Resources.ArrivalCaption;
        }
    }
}