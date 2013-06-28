using System;
namespace Tus.Communication
{
    public interface IDeviceState<out T>
    {
        //DevicePacket BasePacket { get; set; }
        DeviceID ID { get; set; }
        ModuleTypeEnum ModuleType { get; set; }
        T Data { get; }
    }
}
