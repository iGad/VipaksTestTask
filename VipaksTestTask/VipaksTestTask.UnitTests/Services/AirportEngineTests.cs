using System;
using System.Linq;
using NUnit.Framework;
using VipaksTestTask.Interfaces;
using VipaksTestTask.Services;
using VipaksTestTask.UnitTests.Fakes;

namespace VipaksTestTask.UnitTests.Services
{
    [TestFixture]
    public class AirportEngineTests
    {
        private AirportEngine CreateEngine(IScheduleProvider scheduleProvider)
        {
            return CreateEngine(new FakeTimeManager(), scheduleProvider);
        }

        private AirportEngine CreateEngine(ITimeManager timeManager)
        {
            return CreateEngine(timeManager, new FakeScheduleProvider());
        }

        private AirportEngine CreateEngine(ITimeManager timeManager, IScheduleProvider scheduleProvider)
        {
            return new AirportEngine(scheduleProvider, timeManager, new PlaneCapacityProvider());
        }




        [Test]
        public void Start_Always_OrderFlights()
        {
            var scheduleProvider = new FakeScheduleProvider();
            var schedule = scheduleProvider.GetSchedule();
            schedule.Flights = schedule.Flights.OrderByDescending(x => x.Time).ToList();
            scheduleProvider.Shedule = schedule;
            var engine = CreateEngine(scheduleProvider);

            engine.Start();

            var orderedFlights = schedule.Flights.OrderBy(x => x.Time).ToList();
            Assert.IsTrue(orderedFlights.SequenceEqual(schedule.Flights));
        }

        [Test]
        public void Start_Always_StartTimeManager()
        {
            var timeManager = new FakeTimeManager();
            var engine = CreateEngine(timeManager);

            engine.Start();
            
            Assert.IsTrue(timeManager.IsStartCalled);
        }

        [Test]
        [TestCase(1000, 1, 0)]
        [TestCase(5401000, 1, 1)]
        [TestCase(86400000, 5, 5)]
        public void OnTimeManagerTick_WhenExistsFlights_RaiseExpectedEvent(int addedMilliseconds, int expectedArrived, int expectedDepartured)
        {
            var scheduleProvider = new FakeScheduleProvider();
            var schedule = scheduleProvider.GetSchedule();
            scheduleProvider.Shedule = schedule;
            var timeManager = new FakeTimeManager();
            int arrived = 0;
            int departured = 0;
            var engine = CreateEngine(timeManager, scheduleProvider);
            engine.PlaneArrived += (s, a) => arrived++;
            engine.PlaneDepartured += (s, a) => departured++;
            engine.Start();
            
            timeManager.RaiseTick(new TickEventArgs {Time = schedule.Flights.First().Time.Add(TimeSpan.FromMilliseconds(addedMilliseconds)) });

            Assert.AreEqual(expectedArrived, arrived);
            Assert.AreEqual(expectedDepartured, departured);
        }

        [Test]
        [TestCase(1000, 5400, 2)]
        [TestCase(5401000, 14, 10)]
        [TestCase(80000000, 2, 20)]
        public void OnTimeManagerTick_WhenSomeTimes_RaiseExpectedEventCount(int addedMilliseconds, int tickCount, int expectedEventCount)
        {
            var scheduleProvider = new FakeScheduleProvider();
            var schedule = scheduleProvider.GetSchedule();
            scheduleProvider.Shedule = schedule;
            var timeManager = new FakeTimeManager();
            int eventCount = 0;
            var engine = CreateEngine(timeManager, scheduleProvider);
            engine.PlaneArrived += (s, a) => eventCount++;
            engine.PlaneDepartured += (s, a) => eventCount++;
            engine.Start();
            var time = TimeSpan.Zero;

            for (var i = 0; i < tickCount; i++)
            {
                time += TimeSpan.FromMilliseconds(addedMilliseconds);
                if (time.Days > 0)
                    time = time.Subtract(TimeSpan.FromDays(time.Days));
                timeManager.RaiseTick(new TickEventArgs { Time = time });
            }

            Assert.AreEqual(expectedEventCount, eventCount);
        }
    }
}
