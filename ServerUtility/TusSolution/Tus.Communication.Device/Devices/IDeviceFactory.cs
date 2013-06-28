using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tus.Communication.Device.Composition
{
    public interface IDeviceFactory
    {
        IDevice<IDeviceState<IPacketDeviceData>> CreateDevice();
        IDeviceState<IPacketDeviceData> CreateDeviceState();
        IPacketDeviceData CreateDeviceData();
    }
}
