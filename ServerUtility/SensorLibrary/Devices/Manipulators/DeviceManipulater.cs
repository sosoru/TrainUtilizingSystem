using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;

using SensorLibrary.Devices;

namespace SensorLibrary.Manipulators
{
    public class DeviceManipulater<TDevice, TState>
        : IDeviceManipulator
        where TDevice : class, IDevice<TState>
        where TState : class, IDeviceState<IPacketDeviceData>
    {
        public TDevice TargetDevice { get; set; }

        public virtual bool IsExecutable
        {
            get { return this.TargetDevice != null; }
        }

        protected virtual void ExecuteInternal()
        {

        }

        public virtual Action ExecuteFunc { get{ return new Action(ExecuteInternal);}}
    }

}
