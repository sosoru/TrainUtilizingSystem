using System;
namespace SensorLibrary.Devices.TusAvrDevices
{
    public interface ISensorDevice
    {
        bool IsDetected { get; }
    }
}
