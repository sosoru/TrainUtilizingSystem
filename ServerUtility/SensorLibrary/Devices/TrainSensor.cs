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

        private TrainSensorState startDetected = null;
        private TrainSensorState endofUnDetected = null;
        private DateTime DetectedTime;

        public TrainSensor(DeviceID id)
            : base()
        {
            this.ModuleType = ModuleTypeEnum.TrainSensor;

            this.DetectedTime = DateTime.Now;
        }

        public TrainSensor()
            : this(new DeviceID())
        { }

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
            before = this.startDetected;
            current = this.endofUnDetected;

            if (before != null && current != null && this.IsSolidSpeed)
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

        public double calcElapsedTimer(TrainSensorState before, TrainSensorState current)
        {
            var flowed = current.OverflowedCount - before.OverflowedCount;

            return current.Timer - before.Timer + ushort.MaxValue * flowed;
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
                    if (DateTime.Now.Subtract(this.DetectedTime).TotalMilliseconds > 100.0)
                    {
                        this.startDetected = casted;
                    }

                    this.endofUnDetected = casted;
                    this.DetectedTime = DateTime.Now;
                }
                base.OnNext(value);
            }
        }

        public bool IsSolidSpeed
        {
            get
            {
                return this.DetectedTime.Subtract(DateTime.Now).TotalMilliseconds > 150.0;
            }
        }

        private bool isSatisfyInterval(TrainSensorState before, TrainSensorState current)
        {
            int interval = current.Timer - before.Timer;

            if (before.Timer > current.Timer)
                interval += (int)ushort.MaxValue;

            return interval > (uint)Math.Ceiling(48000000.0 / 100.0 / 256.0);

        }
    }

}
