using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;

namespace SensorLibrary.DeviceCommands
{
    public class DeviceCommand<TState>
        : IDeviceCommand<TState>, IDisposable
        where TState : IDeviceState<IPacketDeviceData>
    {
        private IObservable<IDeviceState<IPacketDeviceData>> obsv;
        private IDisposable unsubscriber;

        public DeviceCommand(IObservable<IDeviceState<IPacketDeviceData>> observable)
        {
            this.obsv = observable;
            this.unsubscriber = this.obsv.Subscribe();
        }

        #region Dispose-Finalize Pattern
        private bool __disposed = false;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (__disposed) return;
            if (disposing)
            {

            }

            if(this.unsubscriber!=null)
                 this.unsubscriber.Dispose();
            
            //base.Dispose();
            __disposed = true;
        }

        ~DeviceCommand()
        {
            this.Dispose(false);
        }
        #endregion

        public string Name { get; set; }

        public Func<IDeviceCommand<TState>, bool> IsExecutable { get; set; }

        public Action<IDeviceCommand<TState>> Command { get; set; }

        public IDevice<TState> OwnerDevice { get; protected set; }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(TState value)
        {
            
        }
    }
}
