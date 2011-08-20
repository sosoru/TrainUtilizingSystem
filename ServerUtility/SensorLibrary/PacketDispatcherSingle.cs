using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reactive.Linq;

namespace SensorLibrary
{
    public class PacketDispatcherSingle
        : PacketDispatcher
    {
        public ObservableCollection<IDevice<IDeviceState<IPacketDeviceData>>> FoundDeviceList { get; private set; }

        public PacketDispatcherSingle()
            : base()
        {
            this.FoundDeviceList = new ObservableCollection<IDevice<IDeviceState<IPacketDeviceData>>>();
        }

        public override void Notify(IDeviceState<IPacketDeviceData> state)
        {
            var before = this.FoundDeviceList.FirstOrDefault((dev) => dev.DeviceID == state.BasePacket.ID);

            if (before == null)
            {
                var fact = DeviceFactory.AvailableDeviceTypes.First((d) => d.ModuleType == state.BasePacket.ModuleType);
                
                var dev = fact.DeviceCreate();
                dev.DeviceID = state.BasePacket.ID;
                dev.Observe(this);
                this.FoundDeviceList.Add(dev);
            }

            base.Notify(state);
        }
    }
}
