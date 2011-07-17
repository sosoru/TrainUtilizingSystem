using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    public class PointModule
    : Device<PointModuleState>
    {
        public PointModule(DeviceID id, IObservable<IDeviceState<IPacketDeviceData>> obsv)
            : base(id, ModuleTypeEnum.PointModule, obsv)
        {
            if (id.ModulePart % 4 == 1)
                throw new InvalidOperationException("invalid module no");
        }

        public PointModule(DeviceID id)
            : this(id, null)
        {
        }

        public void ChangePoint(byte address, PointStateEnum state)
        {
            this.CurrentState[address] = state;
            this.SendPacket(this.CurrentState.BasePacket);
        }
    }


}