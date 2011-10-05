using System;
using SensorLibrary;

namespace SensorLivetView.Models.Devices
{
    public interface IDeviceModel< out TDev>
     where TDev : IDevice<IDeviceState<IPacketDeviceData>>
    {
        DeviceID DevID { get; }
        TDev TargetDevice { get; }
    }
}
