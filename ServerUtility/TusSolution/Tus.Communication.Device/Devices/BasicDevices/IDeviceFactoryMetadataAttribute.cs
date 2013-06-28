using System;
using Tus.Communication;

namespace Tus.Communication.Device.Composition
{
    public interface IDeviceFactoryMetadataAttribute
    {
        ModuleTypeEnum ModuleType { get; }
    }
}
