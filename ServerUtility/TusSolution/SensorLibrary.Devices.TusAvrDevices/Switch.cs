using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary.Devices.TusAvrDevices
{
    public class Switch
        :Device<SwitchState>
    {
        public Switch()
            : base(ModuleTypeEnum.AvrSwitch, new SwitchState())
        {
        }

    }
}
