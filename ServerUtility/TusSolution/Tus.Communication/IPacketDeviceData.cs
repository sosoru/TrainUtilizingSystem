using System;
namespace Tus.Communication
{
    public interface IPacketDeviceData
    {
        byte InternalAddr { get; set; }
        byte DataLength { get; set; }
        byte ModuleType { get; set; }
    }
}
