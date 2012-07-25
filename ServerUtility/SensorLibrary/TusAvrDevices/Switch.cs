using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary.DeviceStates.AvrDevices;

namespace SensorLibrary.Devices.AvrDevices
{
    public class Switch
        :Device<SwitchState>
    {
        public Switch()
        {
            this.ModuleType = ModuleTypeEnum.AvrSwitch;

        }

    }
}
