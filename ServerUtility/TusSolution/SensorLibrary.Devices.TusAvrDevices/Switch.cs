using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Tus.Communication.Device.AvrComposed
{
    [DataContract]
    public class Switch
        :Device<SwitchState>
    {
        public Switch()
            : base(ModuleTypeEnum.AvrSwitch, new SwitchState())
        {
        }

        public bool PositionReversed { get; set; }

        public override string ToString()
        {
            return ((this.PositionReversed)? "r!" : "" )+  base.ToString();
        }
    }
}
