using System;
using VipaksTestTask.Interfaces;
using VipaksTestTask.Services;

namespace VipaksTestTask.UnitTests.Fakes
{
    class FakeTimer : ITimerWrapper
    {
        private Action _action;

        public void ExecuteAction()
        {
            _action();
        }
        public int Interval { get; set; }
        public void Start(Action action)
        {
            _action = action;
        }

        public void Stop()
        {
            
        }
    }
}
