using System;
namespace SensorLibrary
{
    public interface IDeviceIO
    {
        DevicePacket ReadPacket();
        void WritePacket(DevicePacket packet);
    }
}
