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
        private TrainSensorState lastDetected = null;
        private TrainSensorState firstNotDetected = null;


        public TrainSensor(DeviceID id)
            : base()
        {
            this.ModuleType = ModuleTypeEnum.TrainSensor;
            //Observable.Range(1, 1024).Do((i) => this.history.Push(new TrainSensorState(new DevicePacket())));
        }

        public TrainSensor()
            : this(new DeviceID())
        { }

        //protected void OnTimerOverflowed()
        //{            
        //    if (this.TimerOverflowed != null)
        //        this.TimerOverflowed(this, new EventArgs());
        //}

        public TrainSensor ChangeMeisuringMode()
        {
            var state = this.CurrentState;

            state.Mode = TrainSensorMode.meisuring;
            this.SendPacket(state);

            return this;
        }

        public TrainSensor ChangeDetectingMode()
        {
            return ChangeDetectingMode(this.CurrentState.ThresholdVoltageLower);
        }

        public TrainSensor ChangeDetectingMode(float threshold)
        {
            var state = this.CurrentState;

            state.Mode = TrainSensorMode.detecting;
            state.ThresholdVoltageLower = threshold;
            this.SendPacket(state);

            return this;
        }

        public double CalculateSpeed(double leninterval)
        {
            TrainSensorState before, current;
            before = this.lastDetected;
            current = this.firstNotDetected;

            if (before != null && current != null)
            {
                double sec = 0.0;
                if (current.Timer - before.Timer < 0)
                    sec = Math.Abs(current.Timer - before.Timer) + ushort.MaxValue;
                else
                    sec = current.Timer - before.Timer;

                sec *= 256.0;
                sec /= 48000000.0;

                return leninterval / sec;
            }

            return 0.0;
        }

        public override void OnNext(IDeviceState<IPacketDeviceData> value)
        {
            var casted = value as TrainSensorState;
            if (casted == null)
                return;

            if (casted.BasePacket.ID == this.DeviceID)
            {
                if (casted.Mode == TrainSensorMode.detecting && casted.IsDetected)
                {
                    this.lastDetected = casted;
                    this.firstNotDetected = null;
                }
                else if (this.firstNotDetected == null)
                    this.firstNotDetected = casted;

                base.OnNext(value);
            }
        }


    }

}
