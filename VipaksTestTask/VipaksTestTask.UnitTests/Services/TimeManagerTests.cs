using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using VipaksTestTask.Services;
using VipaksTestTask.UnitTests.Fakes;

namespace VipaksTestTask.UnitTests.Services
{
    [TestFixture]
    public class TimeManagerTests
    {
        private FakeTimer CreateTimer()
        {
            return new FakeTimer();
        }

        private TimeManager CreateTimeManager(ITimerWrapper timer)
        {
            return new TimeManager(timer);
        }

        [Test]
        [TestCase(1, 1000, new []{"00:00:01"})]
        [TestCase(5, 1000, new[] { "00:00:01", "00:00:02", "00:00:03", "00:00:04", "00:00:05" })]
        [TestCase(4, 5400000, new[] { "01:30:00", "03:00:00", "04:30:00", "06:00:00"})]
        [TestCase(4, 39600000, new[] { "11:00:00", "22:00:00", "09:00:00", "20:00:00" })]
        public void OnTimerElapsed_WhenElapsedSomeTimes_RaiseEventsWithExpectedTime(int expectedTicks, int timerInterval, string[] expectedTimes)
        {
            var timer = CreateTimer();
            var manager = CreateTimeManager(timer);
            timer.Interval = timerInterval;
            var eventCount = 0;
            var times = new List<string>();
            manager.Tick += (s, args) =>
            {
                eventCount++;
                times.Add(args.Time.ToString(@"hh\:mm\:ss"));
            };
            manager.Start();

            for(var i=0;i<expectedTicks;i++)
                timer.ExecuteAction();

            Assert.AreEqual(expectedTicks, eventCount);
            Assert.IsTrue(expectedTimes.SequenceEqual(times));
        }
    }
}
