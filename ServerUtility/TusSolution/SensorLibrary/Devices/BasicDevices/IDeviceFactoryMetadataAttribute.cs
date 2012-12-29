using System;
namespace SensorLibrary.Devices.BasicDevices
{
    public interface IDeviceFactoryMetadataAttribute
    {
        SensorLibrary.ModuleTypeEnum ModuleType { get; }
    }
}
