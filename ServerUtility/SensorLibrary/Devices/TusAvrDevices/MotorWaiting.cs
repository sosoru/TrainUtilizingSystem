using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;

namespace SensorLibrary.Devices.TusAvrDevices
{
    public class MotorWaiting
        : Device<MotorWaitingState>, ISensorDevice
    {
        public MotorWaiting()
        {
            this.CurrentState = new MotorWaitingState();
        }

        public bool IsDetected
        {
            get { throw new NotImplementedException(); }
        }
    }
}
