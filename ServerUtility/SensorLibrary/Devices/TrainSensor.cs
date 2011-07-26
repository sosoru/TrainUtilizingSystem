using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;

namespace SensorLibrary
{
    public class TrainSensor
    : Device<TrainSensorState>
    {

        //public event EventHandler TimerOverflowed;

        public TrainSensor(DeviceID id)
            : this(id, null)
        {
            //Observable.Range(1, 1024).Do((i) => this.history.Push(new TrainSensorState(new DevicePacket())));
        }

        public TrainSensor(DeviceID id, IObservable<IDeviceState<IPacketDeviceData>> obsv)
            : base(id, ModuleTypeEnum.TrainSensor, obsv) { }

        //protected void OnTimerOverflowed()
        //{            
        //    if (this.TimerOverflowed != null)
        //        this.TimerOverflowed(this, new EventArgs());
        //}

        public MeisuringTrainSensor ChangeMeisuringMode()
        {
            var state = this.CurrentState;

            state.Mode = TrainSensorMode.meisuring;
            this.SendPacket(state);

            return new MeisuringTrainSensor(this.DeviceID, this.Observing);
        }

        public DetectingTrainSensor ChangeDetectingMode()
        {
            var state = this.CurrentState;
            state.Mode = TrainSensorMode.detecting;
            this.SendPacket(state);

            return new DetectingTrainSensor(this.DeviceID, this.Observing);
        }

        public DetectingTrainSensor ChangeDetectingMode(float threshold)
        {
            var state = this.CurrentState;

            state.Mode = TrainSensorMode.detecting;
            state.ThresholdVoltage = threshold;
            this.SendPacket(state);

            return new DetectingTrainSensor(this.DeviceID, this.Observing);
        }

    }

}
