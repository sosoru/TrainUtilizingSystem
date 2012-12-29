using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace SensorLibrary.Devices.Factories
{
    [InheritedExport]
    public class DeviceFactory<TDev, TState, TData>
        : IDeviceFactory
        where TDev : class, IDevice<TState>, new()
        where TState : class, IDeviceState<TData>, new()
        where TData : IPacketDeviceData, new()
    {

        public IDevice<IDeviceState<IPacketDeviceData>> CreateDevice()
        {
            return (IDevice<IDeviceState<IPacketDeviceData>>)new TDev();
        }

        public IDeviceState<IPacketDeviceData> CreateDeviceState()
        {
            return (IDeviceState<IPacketDeviceData>)new TState();
        }

        public IPacketDeviceData CreateDeviceData()
        {
            return new TData();
        }
    }
}
