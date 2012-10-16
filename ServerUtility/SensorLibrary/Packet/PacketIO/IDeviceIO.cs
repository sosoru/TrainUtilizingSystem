using System;
using System.Reactive;

namespace SensorLibrary.Packet.IO
{
    public interface IDeviceIO
    {
        DevicePacket ReadPacket();
        void WritePacket(DevicePacket packet);
        IObservable<DevicePacket> GetReadingPacket();
        IObservable<Unit> GetWritingPacket(DevicePacket pack);
    }
}
