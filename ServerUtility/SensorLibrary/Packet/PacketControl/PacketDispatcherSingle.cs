﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reactive.Linq;

using SensorLibrary.Devices;

namespace SensorLibrary.Packet.Control
{
    public class PacketDispatcherSingle<TDevice, TDevState>
        : PacketDispatcher
        where TDevice : IDevice<IDeviceState<IPacketDeviceData>>
    {
        public DeviceFactoryProvider FactoryProvider { get; private set; }
        public ObservableCollection<TDevice> FoundDeviceList { get; private set; }
        public override System.Collections.Specialized.INotifyCollectionChanged DeviceFoundNotifier
        {
            get { return this.FoundDeviceList; }
        }

        public PacketDispatcherSingle(DeviceFactoryProvider fprovider)
            : base()
        {
            this.FactoryProvider = fprovider;
            this.FoundDeviceList = new ObservableCollection<TDevice>();
        }

        public override void Notify(IDeviceState<IPacketDeviceData> state)
        {
            if (state ==null || !(state is TDevState))
                return;

            var before = this.FoundDeviceList.FirstOrDefault((dev) => dev.DeviceID == state.BasePacket.ID);

            if (before == null)
            {
                var fact = this.FactoryProvider.AvailableDeviceTypes.First((d) => d.ModuleType == state.BasePacket.ModuleType);

                var dev = (TDevice)fact.DeviceCreate();
                dev.DeviceID = state.BasePacket.ID;
                dev.Observe(this);
                this.FoundDeviceList.Add(dev);
            }

            base.Notify(state);
        }
    }
}
