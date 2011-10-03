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
            : base()
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
            return ChangeDetectingMode(this.CurrentState.ThresholdVoltage);
        }

        public TrainSensor ChangeDetectingMode(float threshold)
        {
            var state = this.CurrentState;

            state.Mode = TrainSensorMode.detecting;
            state.ThresholdVoltage = threshold;
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
                double sec = 0.0f;
                if (current.Timer - before.Timer < 0)
                    sec = Math.Abs(current.Timer - before.Timer) + ushort.MaxValue;
                else
                    sec = current.Timer - before.Timer;

                sec /= 48000000.0;

                return leninterval / sec;
            }

            return double.NaN;
        }

        public IObservable<TrainSensor> GetSpeedChangedObservable()
        {
            return this.GetNextObservable().Where((args) =>
            {
                var bef = args.EventArgs.beforestate as TrainSensorState;
                var cur = args.EventArgs.state as TrainSensorState;
                if (bef != null && cur != null)
                    return true;
                else
                    return false;
            })
                .Select((args) => args.Sender as TrainSensor);

        }

        public override void OnNext(IDeviceState<IPacketDeviceData> value)
        {
            var casted = value as TrainSensorState;
            if (casted == null)
                return;

            if (casted.BasePacket.ID.ModuleAddr == this.DeviceID.ModuleAddr && casted.Mode == TrainSensorMode.detecting)
            {
                if (casted.IsDetected)
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
