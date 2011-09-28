using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reactive.Linq;

using SensorLibrary;

namespace SensorLibrary
{
    public class PacketDispatcherSingle<TDevice, TDevState>
        : PacketDispatcher
        where TDevice : IDevice<IDeviceState<IPacketDeviceData>>
    {
        public ObservableCollection<TDevice> FoundDeviceList { get; private set; }

        public PacketDispatcherSingle()
            : base()
        {
            this.FoundDeviceList = new ObservableCollection<TDevice>();
        }

        public override void Notify(IDeviceState<IPacketDeviceData> state)
        {
            if (!(state is TDevState))
                return;

            var before = this.FoundDeviceList.FirstOrDefault((dev) => dev.DeviceID == state.BasePacket.ID);

            if (before == null)
            {
                var fact = DeviceFactory.AvailableDeviceTypes.First((d) => d.ModuleType == state.BasePacket.ModuleType);
                
                var dev = (TDevice)fact.DeviceCreate();
                dev.DeviceID = state.BasePacket.ID;
                dev.Observe(this);
                this.FoundDeviceList.Add(dev);
            }

            base.Notify(state);
        }
    }
}
