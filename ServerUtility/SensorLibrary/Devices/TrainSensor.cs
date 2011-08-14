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
            : base()
        {
            this.ModuleType = ModuleTypeEnum.TrainSensor;
            //Observable.Range(1, 1024).Do((i) => this.history.Push(new TrainSensorState(new DevicePacket())));
        }

        public TrainSensor()
            : base()
        { }

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

            return new MeisuringTrainSensor()
                {
                    DeviceID = this.DeviceID,
                    Observing = this.Observing,
                };
        }

        public DetectingTrainSensor ChangeDetectingMode()
        {
            return ChangeDetectingMode(this.CurrentState.ThresholdVoltage);
        }

        public DetectingTrainSensor ChangeDetectingMode(float threshold)
        {
            var state = this.CurrentState;

            state.Mode = TrainSensorMode.detecting;
            state.ThresholdVoltage = threshold;
            this.SendPacket(state);

            return new DetectingTrainSensor()
                {
                    DeviceID = this.DeviceID,
                    Observing = this.Observing,
                };
        }

    }

}
