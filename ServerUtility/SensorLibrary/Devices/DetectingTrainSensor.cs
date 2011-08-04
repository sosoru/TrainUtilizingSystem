using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace SensorLibrary
{
    public class DetectingTrainSensor
        : TrainSensor
    {

        private TrainSensorState lastDetected = null;
        private TrainSensorState firstNotDetected = null;

        public DetectingTrainSensor(DeviceID id, IObservable<IDeviceState<IPacketDeviceData>> obsv)
            : base(id, obsv)
        { }

        public DetectingTrainSensor(DeviceID id)
            : this(id, null)
        { }

        public override void OnNext(IDeviceState<IPacketDeviceData> value)
        {
            var casted = value as TrainSensorState;
            if (casted == null)
                return;

            if (casted.BasePacket.ID == this.DeviceID && casted.Mode == TrainSensorMode.detecting)
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

        public IObservable<DetectingTrainSensor> GetSpeedChangedObservable()
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
                .Select((args) => args.Sender as DetectingTrainSensor);

        }



    }
}
