using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;

namespace SensorLibrary.Devices.TusAvrDevices
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
