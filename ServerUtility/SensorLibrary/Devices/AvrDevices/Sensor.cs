using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;
using SensorLibrary.DeviceStates.AvrDevices;

namespace SensorLibrary.Devices.AvrDevices
{
    public class Sensor
        : Device<SensorState>
    {
        public Sensor()
        {
            this.ModuleType = ModuleTypeEnum.AvrSensor;
        }

        
    }
}
