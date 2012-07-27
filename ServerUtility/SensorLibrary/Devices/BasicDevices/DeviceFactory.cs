﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SensorLibrary.Packet.Data;

using SensorLibrary.Devices.PicUsbDevices;
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

    public sealed class PicDeviceFactoryProvider
        : DeviceFactoryProvider
    {
        public PicDeviceFactoryProvider() { }

        public static readonly IDeviceFactory<MotherBoard, MotherBoardState, MotherBoardData> MotherBoardFactory
            = DefaultFactory<MotherBoard, MotherBoardState, MotherBoardData>(ModuleTypeEnum.MotherBoard);

        public static readonly IDeviceFactory<PointModule, PointModuleState, PointModuleData> PointModuleFactory
            = DefaultFactory<PointModule, PointModuleState, PointModuleData>(ModuleTypeEnum.PointModule);

        public static readonly IDeviceFactory<TrainSensor, TrainSensorState, TrainSensorData> TrainSensorFactory
            = DefaultFactory<TrainSensor, TrainSensorState, TrainSensorData>(ModuleTypeEnum.TrainSensor);

        public static readonly IDeviceFactory<TrainController, TrainControllerState, TrainControllerData> TrainControllerFactory
             = DefaultFactory<TrainController, TrainControllerState, TrainControllerData>(ModuleTypeEnum.TrainController);

        private static readonly IEnumerable<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>> _AvailableDeviceTypes
            = new ReadOnlyCollection<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>>
            (
                new IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData> [] { MotherBoardFactory, PointModuleFactory, TrainSensorFactory, TrainControllerFactory }
            );

        public override IEnumerable<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>> AvailableDeviceTypes
        {
            get { return _AvailableDeviceTypes; }
        }
    }

    public sealed class AvrDeviceFactoryProvider
        : DeviceFactoryProvider
    {
        private AvrDeviceFactoryProvider() {}

        public static readonly IDeviceFactory<Motor, MotorState, MotorData> MotorModuleFactory
            = DefaultFactory<Motor, MotorState, MotorData>(ModuleTypeEnum.AvrMotor);
     
        public static readonly IDeviceFactory<Switch, SwitchState, SwitchData> SwitchModuleFactory
            = DefaultFactory<Switch, SwitchState, SwitchData>(ModuleTypeEnum.AvrSwitch);

        public static readonly IDeviceFactory<Sensor, SensorState, SensorData> SensorModuleFactory
            = DefaultFactory<Sensor, SensorState, SensorData>(ModuleTypeEnum.AvrSensor);

        private static readonly IEnumerable<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>> _AvailableDeviceTypes
            = new ReadOnlyCollection<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>>
            (
                new IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>[]
                { MotorModuleFactory, SwitchModuleFactory, SensorModuleFactory}
            );

        public override IEnumerable<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>,IDeviceState<IPacketDeviceData>,IPacketDeviceData>>  AvailableDeviceTypes
        {
	        get { return _AvailableDeviceTypes; }
        }
    }
}
