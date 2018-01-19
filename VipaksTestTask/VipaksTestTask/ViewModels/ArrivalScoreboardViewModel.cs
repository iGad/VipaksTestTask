using VipaksTestTask.Services;

namespace VipaksTestTask.ViewModels
{
    public class ArrivalScoreboardViewModel : ScoreboardViewModel
    {
        public ArrivalScoreboardViewModel(AirportEngine engine) : base(engine)
        {
            engine.PlaneArrived += OnFlightHappend;
            Name = Resources.ArrivalCaption;
        }
    }
}