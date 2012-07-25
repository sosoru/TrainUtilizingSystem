using System;
namespace SensorLibrary.Packet.IO
{
    public interface IDeviceIO
    {
        DevicePacket ReadPacket();
        void WritePacket(DevicePacket packet);
    }
}
