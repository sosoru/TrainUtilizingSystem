using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SensorLibrary.Packet.Data;

namespace SensorLibrary.Devices.PicUsbDevices
{
    public class PointModule
    : Device<PointModuleState>
    {
        public PointModule()
            :base()
        {
            this.ModuleType = ModuleTypeEnum.PointModule;

            this.StateEqualityComparer = new GenericComparer<PointModuleState>((x, y) => x.Data.Directions.SequenceEqual(y.Data.Directions));

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