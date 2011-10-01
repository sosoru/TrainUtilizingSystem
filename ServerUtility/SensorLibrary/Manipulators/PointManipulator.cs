using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;

using SensorLibrary;

namespace SensorLibrary.Manipulators
{
    public class PointManipulator
        : IDeviceManipulator
    {
        private DeviceStateDeserializer<PointModule, PointModuleState> deserializer;

        public PointModule Target { get; set; }
        public PointModuleState To { get; set; }

        public PointManipulator()
        {
            this.deserializer = new DeviceStateDeserializer<PointModule, PointModuleState>()
            {
                ApplyFunc = apply,
            };
        }

        private PointModuleState apply(DeviceStateDeserializer<PointModule, PointModuleState> des)
        {
            var state = new PointModuleState()
            {
                BasePacket = new DevicePacket() { ID = des.TargetDevice.DeviceID, ModuleType = ModuleTypeEnum.PointModule },
            };

            this.Target.CurrentState.Data.Directions.CopyTo(state.Data.Directions, 0);

            Observable.Range(0, des.TargetDevice.CurrentState.StateLength)
                      .Where(i => des.DeserializingState.GetPointState(i) != PointStateEnum.Any)
                      .Do(i => state.SetPointState(i, des.DeserializingState.GetPointState(i)));

            return state;
        }

        public bool IsExecutable
        {
            get { return this.deserializer.IsExecutable && this.Target != null && this.To != null; }
        }

        public Action ExecuteFunc
        {
            get {
                this.deserializer.TargetDevice = this.Target;
                this.deserializer.DeserializingState = this.To;
                return this.deserializer.ExecuteFunc; 
            }
        }
    }
}
