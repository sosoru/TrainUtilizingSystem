using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tus.Communication.Device.AvrComposed
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
