﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SensorLibrary
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

    public sealed class DeviceFactory
    {
        private DeviceFactory() { }

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
        public static readonly IDeviceFactory<MotherBoard, MotherBoardState, MotherBoardData> MotherBoardFactory
            = new devfactint<MotherBoard, MotherBoardState, MotherBoardData>(ModuleTypeEnum.MotherBoard);

        public static readonly IDeviceFactory<PointModule, PointModuleState, PointModuleData> PointModuleFactory
            = new devfactint<PointModule, PointModuleState, PointModuleData>(ModuleTypeEnum.PointModule);

        public static readonly IDeviceFactory<TrainSensor, TrainSensorState, TrainSensorData> TrainSensorFactory
            = new devfactint<TrainSensor, TrainSensorState, TrainSensorData>(ModuleTypeEnum.TrainSensor);

        public static readonly IDeviceFactory<TrainController, TrainControllerState, TrainControllerData> TrainControllerFactory
             = new devfactint<TrainController, TrainControllerState, TrainControllerData>(ModuleTypeEnum.TrainController);

        public static readonly IEnumerable<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>> AvailableDeviceTypes
            = new ReadOnlyCollection<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>>
            (
                new IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData> [] { MotherBoardFactory, PointModuleFactory, TrainSensorFactory, TrainControllerFactory }
            );
    }
}