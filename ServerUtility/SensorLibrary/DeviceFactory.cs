using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    public interface IDeviceFactory<out TDev, out TState, out TData>
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
            where TData : class, IPacketDeviceData
            where TState : IDeviceState<TData>
            where TDev : IDevice<TState>
        {
            public Func<TDev> DeviceCreate { get; set; }
            public Func<TState> DeviceStateCreate { get; set; }
            public Func<TData> DeviceDataCreate { get; set; }
            public ModuleTypeEnum ModuleType { get; set; }
        }

        public static readonly IDeviceFactory<MotherBoard, MotherBoardState, MotherBoardData> MotherBoardFactory
            = new devfactint<MotherBoard, MotherBoardState, MotherBoardData>
            {
                DeviceCreate = () => new MotherBoard(),
                DeviceStateCreate = () => new MotherBoardState(),
                DeviceDataCreate = () => new MotherBoardData(),
                ModuleType = ModuleTypeEnum.MotherBoard,
            };

        public static readonly IDeviceFactory<PointModule, PointModuleState, PointModuleData> PointModuleFactory
            = new devfactint<PointModule, PointModuleState, PointModuleData>
            {
                DeviceCreate = () => new PointModule(),
                DeviceStateCreate = () => new PointModuleState(),
                DeviceDataCreate = () => new PointModuleData(),
                ModuleType = ModuleTypeEnum.PointModule,
            };

        public static readonly IDeviceFactory<TrainSensor, TrainSensorState, TrainSensorData> TrainSensorFactory
            = new devfactint<TrainSensor, TrainSensorState, TrainSensorData>
            {
                DeviceCreate = () => new TrainSensor(),
                DeviceStateCreate = () => new TrainSensorState(),
                DeviceDataCreate = () => new TrainSensorData(),
                ModuleType = ModuleTypeEnum.TrainSensor,
            };

        public static readonly IDeviceFactory<TrainController, TrainControllerState, TrainControllerData> TrainControllerFactory
             = new devfactint<TrainController, TrainControllerState, TrainControllerData>
             {
                 DeviceCreate = () => new TrainController(),
                 DeviceStateCreate = () => new TrainControllerState(),
                 DeviceDataCreate = () => new TrainControllerData(),
                 ModuleType = ModuleTypeEnum.TrainController,
             };

        public static readonly IDeviceFactory<RemoteModule, RemoteModuleState, RemoteModuleData> RemoteModuleFactory
             = new devfactint<RemoteModule, RemoteModuleState, RemoteModuleData>
             {
                 DeviceCreate = () => new RemoteModule(),
                 DeviceStateCreate = () => new RemoteModuleState(),
                 DeviceDataCreate = () => new RemoteModuleData(),
                 ModuleType = ModuleTypeEnum.RemoteModule,
             };

        public static readonly IEnumerable<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>> AvailableDeviceTypes
            = new ReadOnlyCollection<IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>>
            (
                new IDeviceFactory<IDevice<IDeviceState<IPacketDeviceData>>, IDeviceState<IPacketDeviceData>, IPacketDeviceData>[] 
                { MotherBoardFactory, PointModuleFactory, TrainSensorFactory, TrainControllerFactory, RemoteModuleFactory }
            );
    }
}
