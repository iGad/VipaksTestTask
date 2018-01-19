using VipaksTestTask.Services;

namespace VipaksTestTask.ViewModels
{
    public class DepartureScoreboardViewModel : ScoreboardViewModel
    {
        public DepartureScoreboardViewModel(AirportEngine engine) : base(engine)
        {
            engine.PlaneDepartured += OnFlightHappend;
            Name = Resources.DepartureCaption;
        }
    }
}