using System.IO;
using Newtonsoft.Json;
using VipaksTestTask.Interfaces;
using VipaksTestTask.Models;

namespace VipaksTestTask.Services
{
    /// <summary>
    /// Поставщик расписания из файла
    /// </summary>
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
