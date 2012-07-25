using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;

using SensorLibrary.Packet.Data;
using SensorLibrary.Devices.PicUsbDevices;

namespace SensorLibrary.Manipulators
{
    public class ControllerManipulator
        : DeviceManipulater<TrainController, TrainControllerState>
    {

        public TimeSpan Duration { get; set; }
        public double To { get; set; }

        public TrainSensor TriggerSensor { get; set; }
        public TrainSensor AbortingSensor { get; set; }

        public override bool IsExecutable
        {
            get
            {
                return base.IsExecutable && TriggerSensor != null;
            }
        }

        protected override void ExecuteInternal()
        {
            if (this.TriggerSensor.CurrentState.Mode != TrainSensorMode.detecting)
                this.TriggerSensor.ChangeDetectingMode();

            if (this.AbortingSensor.CurrentState.Mode != TrainSensorMode.detecting)
                this.AbortingSensor.ChangeDetectingMode();

            while (!this.TriggerSensor.CurrentState.IsDetected)
            {
                Thread.Sleep(100);
            }

            var curvalue = this.TargetDevice.CurrentState.Duty;
            var actualTo = (double)(1 << (this.TargetDevice.CurrentState.DutyResolution - 1)) * To;

            var interval = 500.0;
            var rate = (actualTo - (double)curvalue) / (Duration.TotalMilliseconds / interval);

            while (Math.Abs(this.TargetDevice.CurrentState.Duty - actualTo) > rate)
            {
                var curstate = this.TargetDevice.CurrentState;
                curstate.Duty += (int)rate;
                this.TargetDevice.SendPacket(curstate);

                Thread.Sleep((int)interval);
                if (this.AbortingSensor != null && this.AbortingSensor.CurrentState.IsDetected)
                {
                    var abstate = this.TargetDevice.CurrentState;
                    curstate.Duty = 0;
                    this.TargetDevice.SendPacket(abstate);
                    break;
                }
            }
        }
    }
}
