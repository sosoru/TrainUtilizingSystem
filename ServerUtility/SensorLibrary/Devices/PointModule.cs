using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    public class PointModule
    : Device<PointModuleState>
    {
        public PointModule()
            :base()
        {
            this.ModuleType = ModuleTypeEnum.PointModule;
        }

        public override void OnNext(IDeviceState<IPacketDeviceData> value)
        {
            base.OnNext(value);
        }

        public void ChangePoint(byte address, PointStateEnum state)
        {
            this.CurrentState[address] = state;
            this.SendPacket(this.CurrentState);
        }
    }


}