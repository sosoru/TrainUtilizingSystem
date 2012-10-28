using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SensorLibrary.Packet.Data;

using SensorLibrary.Devices.TusAvrDevices;

namespace SensorLibrary.Devices
{
    public interface IDeviceFactory<out TDev, out TState, out TData>
        where TDev : class, IDevice<TState>
        where TState : class, IDeviceState<TData>
        where TData : IPacketDeviceData
    {
        Func<TDev> DeviceCreate { get; }
        Func<TState> DeviceStateCreate { get; }
        Func<TData> DeviceDataCreate { get; }
        ModuleTypeEnum ModuleType { get; }
    }

    public abstract class DeviceFactoryProvider
    {
        private class devfactint<TDev, TState, TData>
          : IDeviceFactory<TDev, TState, TData>
            where TDev : class, IDevice<TState>, new()
            where TState : class, IDeviceState<TData>, new()
            where TData : IPacketDeviceData, new()
        {
            public devfactint(ModuleTypeEnum mtype)
            {
                DeviceCreate = () => new TDev();
                DeviceStateCreate = () => new TState();
                DeviceDataCreate = () => new TData();

                this.ModuleType = mtype;
            }

            public Func<TDev> DeviceCreate { get; private set; }
            public Func<TState> DeviceStateCreate { get; private set; }
            public Func<TData> DeviceDataCreate { get; private set; }
            public ModuleTypeEnum ModuleType { get; private set; }
        }

        protected static IDeviceFactory<TDev, TState, TData>  DefaultFactory<TDev, TState, TData> (ModuleTypeEnum mtype)
            where TDev : class, IDevice<TState>, new()
            where TState : class, IDeviceState<TData>, new()
            where TData : IPacketDeviceData, new()
        {
            return new devfactint<TDev, TState, TData>(mtype);
        }

        public abstract IEnumerable<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>> AvailableDeviceTypes { get; }

    }


    public sealed class AvrDeviceFactoryProvider
        : DeviceFactoryProvider
    {
        public AvrDeviceFactoryProvider() {}

        public static readonly IDeviceFactory<Motor, MotorState, MotorData> MotorModuleFactory
            = DefaultFactory<Motor, MotorState, MotorData>(ModuleTypeEnum.AvrMotor);
     
        public static readonly IDeviceFactory<Switch, SwitchState, SwitchData> SwitchModuleFactory
            = DefaultFactory<Switch, SwitchState, SwitchData>(ModuleTypeEnum.AvrSwitch);

        public static readonly IDeviceFactory<UsartSensor, SensorState, SensorData> SensorModuleFactory
            = DefaultFactory<UsartSensor, SensorState, SensorData>(ModuleTypeEnum.AvrSensor);

        public static readonly IDeviceFactory<UsartSetting, UsartSettingState, UsartSettingData> UsartSettingFactory
            = DefaultFactory<UsartSetting, UsartSettingState, UsartSettingData>(ModuleTypeEnum.AvrUartSetting);

        public static readonly IDeviceFactory<Kernel, KernelState, KernelData> KernelModuleFactory
            = DefaultFactory<Kernel, KernelState, KernelData>(ModuleTypeEnum.AvrKernel);

        private static readonly IEnumerable<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>> _AvailableDeviceTypes
            = new ReadOnlyCollection<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>>
            (
                new IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>[]
                { MotorModuleFactory, SwitchModuleFactory, SensorModuleFactory, KernelModuleFactory, UsartSettingFactory}
            );

        public override IEnumerable<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>,IDeviceState<IPacketDeviceData>,IPacketDeviceData>>  AvailableDeviceTypes
        {
	        get { return _AvailableDeviceTypes; }
        }
    }
}
