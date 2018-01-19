using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VipaksTestTask.Services
{
    public class ScheduleProvider : IScheduleProvider
    {
        public const string ScheduleFileName = "schedule.json";
        public Schedule GetSchedule()
        {
            var text = File.ReadAllText(ScheduleFileName);
            var schedule = JsonConvert.DeserializeObject<Schedule>(text);
            return schedule;
        }
        
    }
}
