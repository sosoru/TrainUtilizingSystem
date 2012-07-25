using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary.DeviceStates;
using SensorLibrary.DeviceStates.AvrDevices;

namespace SensorLibrary.Devices.AvrDevices
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
