using System;
using SensorLibrary;
using SensorLibrary.Devices;

namespace SensorLivetView.Models.Devices
{
    public interface IDeviceModel< out TDev>
     where TDev : IDevice<IDeviceState<IPacketDeviceData>>
    {
        DeviceID DevID { get; }
        TDev TargetDevice { get; }
    }
}
