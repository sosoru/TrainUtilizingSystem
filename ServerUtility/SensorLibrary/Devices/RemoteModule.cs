using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    public class RemoteModule
         :Device<RemoteModuleState>
    {
        public RemoteModule()
            : base()
        {
            this.ModuleType = ModuleTypeEnum.RemoteModule;
        }

        public void ChangeDevice(DeviceID id)
        {
            var packet = new DevicePacket()
            {
                ID = this.DeviceID,
                ModuleType = ModuleTypeEnum.RemoteModule
            };

            packet.ID.RemoteBit = true;
            var remstate =  new RemoteModuleState() { BasePacket = packet };
            remstate.RemotingID = id;

            this.SendPacket(remstate);
        }

        public void ChangeDevice(IDevice<IDeviceState<IPacketDeviceData>> dev)
        {
            this.ChangeDevice(dev.DeviceID);
        }
    }
}
