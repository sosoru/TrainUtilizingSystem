using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;

namespace SensorLibrary.Devices.TusAvrDevices
{
    public class MotorWaiting
        : Device<MotorWaitingState>
    {
        public MotorWaiting()
        {
            this.CurrentState = new MotorWaitingState();
        }


    }
}
