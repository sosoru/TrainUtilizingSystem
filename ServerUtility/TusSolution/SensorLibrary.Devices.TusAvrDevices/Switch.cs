using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Tus.Diagnostics;

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

        public override void SendState()
        {
            if (this.CurrentState.Position == PointStateEnum.Any)
                return;

            if(this.CurrentState != null)
                Logger.WriteLineAsDeviceInfo("Switch {0} changed to {1}", this.DeviceIDString, this.CurrentState.PositionString);

            base.SendState();
        }
    }
}
