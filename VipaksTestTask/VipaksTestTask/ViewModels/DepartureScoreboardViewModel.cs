using VipaksTestTask.Services;

namespace VipaksTestTask.ViewModels
{
    /// <summary>
    /// Модель для получения и отображения данных об улетевших пассажирах
    /// </summary>
    public class DepartureScoreboardViewModel : ScoreboardViewModel
    {
        public DepartureScoreboardViewModel(AirportEngine engine) : base(engine)
        {
            engine.PlaneDepartured += OnFlightHappend;
            Title = Resources.DepartureCaption;
        }
    }
}