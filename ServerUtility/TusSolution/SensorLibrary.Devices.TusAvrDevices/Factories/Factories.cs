using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using Tus.Communication;
using Tus.Communication.Device.AvrComposed;

namespace Tus.Communication.Device.Composition
{
    [Export(typeof(IDeviceFactory))]
    [DeviceFactoryMetadata(ModuleTypeEnum.AvrMotor)]
    public class MotorDeviceFactory
        : DeviceFactory<Motor, MotorState, MotorData>
    { }

    [Export(typeof(IDeviceFactory))]
    [DeviceFactoryMetadata(ModuleTypeEnum.AvrSwitch)]
    public class SwitchDeviceFactory
        : DeviceFactory<Switch, SwitchState, SwitchData>
    { }

    [Export(typeof(IDeviceFactory))]
    [DeviceFactoryMetadata(ModuleTypeEnum.AvrSensor)]
    public class SensorDeviceFactory
        : DeviceFactory<UsartSensor, SensorState, SensorData>
    { }

    [Export(typeof(IDeviceFactory))]
    [DeviceFactoryMetadata(ModuleTypeEnum.AvrKernel)]
    public class KernelDeviceFactory
        : DeviceFactory<Kernel, KernelState, KernelData>
    { }
}
