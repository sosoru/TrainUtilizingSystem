using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Tus.TransControl.Base;

namespace DialogConsole.Features.Base
{
    [FeatureMetadata("t", "send testdata")]
    [Export(typeof(IFeature))]
    class JsonTestFeature
        : BaseFeature, IFeature
    {
        private class MockPredicator : IRouteLockPredicator
        {
            private bool _next = false;
            public void EnterNext()
            {
                this._next = true;
            }
            public bool ShouldLockNext(Vehicle v)
            {
                if (this._next)
                {
                    this._next = false;
                    return true;
                }
                return false;
            }

            public bool ShouldReleaseBefore(Vehicle v)
            {
                return true;
            }
        }

        private MockPredicator _predicator = new MockPredicator();
        private IDisposable _enteringPipe;
        public void Execute()
        {
            var vehicle = this.Param.UsingLayout.Vehicles.First();

            if (vehicle != null && !vehicle.Predicators.Any(p => p == this._predicator))
            {
                vehicle.Predicators.Add(this._predicator);
            }
        }

        public void Init()
        {
            this._enteringPipe =
                Observable.Defer(() => Observable.Start(this._predicator.EnterNext))
                          .Delay(TimeSpan.FromSeconds(2))
                          .Repeat()
                          .Subscribe();
        }
    }
}
