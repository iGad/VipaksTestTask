using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VipaksTestTask.Services;

namespace VipaksTestTask.UnitTests.Fakes
{
    class FakeTimeManager : ITimeManager
    {
        public int Multiplyer { get; set; }
        public event EventHandler<TickEventArgs> Tick;
        public bool IsStartCalled { get; private set; }
        public bool IsStopCalled { get; private set; }

        public void RaiseTick(TickEventArgs args)
        {
            Tick?.Invoke(this, args);
        }

        public void Start()
        {
            IsStartCalled = true;
        }

        public void Stop()
        {
            IsStopCalled = true;
        }
    }
}
