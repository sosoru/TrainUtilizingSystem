using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary.Devices.TusAvrDevices
{
    public class Motor
        : Device<MotorState>
    {
        public Motor()
        {
            this.ModuleType = ModuleTypeEnum.AvrMotor;
        }
    }
}
