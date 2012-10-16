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
        {
            this.ModuleType = ModuleTypeEnum.AvrSwitch;
            this.CurrentState = new SwitchState();
            this.CurrentState.BasePacket.ModuleType = this.ModuleType;
        }

    }
}
