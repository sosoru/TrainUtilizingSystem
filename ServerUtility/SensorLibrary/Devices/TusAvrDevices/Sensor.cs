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
            this.CurrentState = new SensorState();
        }

        public bool IsDetected
        {
            get
            {
                return this.CurrentState.Voltage > 0.5f;
            }
        }
            }
}
