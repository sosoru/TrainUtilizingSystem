using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;

using SensorLibrary;
using SensorLibrary.Devices;

namespace SensorLibrary.Manipulators
{
    public class DeviceStateDeserializer<TDevice, TState>
        : DeviceManipulater<TDevice, TState>
        where TDevice : class, IDevice<TState> 
        where TState : class,  IDeviceState<IPacketDeviceData>
    {
        private TState _deserializingState;
        public TState DeserializingState
        {
            get
            {
                return this._deserializingState;
            }
            set
            {
                this._deserializingState = value;
                this._deserializingState.BasePacket.ID = this.TargetDevice.DeviceID;
                if (this.TargetDevice.ModuleType != this._deserializingState.BasePacket.ModuleType)
                    throw new InvalidOperationException("invalid module type");
            }
        }

        public override bool IsExecutable
        {
            get
            {
                return base.IsExecutable && this.DeserializingState != null;
            }
        }

        public Func<DeviceStateDeserializer<TDevice, TState>, TState> ApplyFunc { get; set; } 

        protected override void  ExecuteInternal()
        {
            TState deserialstate;
            if (this.ApplyFunc != null)
                deserialstate = this.ApplyFunc(this);
            else
                deserialstate = this.DeserializingState;

            this.TargetDevice.SendPacket(deserialstate);

        }
    }
}
