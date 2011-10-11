using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

using SensorLibrary;
using SensorLivetView.ViewModels;
using SensorLivetView.ViewModels.Controls;
using SensorLivetView.Models;
using SensorLivetView.Models.Devices;

namespace SensorLivetView
{
    public interface IDeviceViewModelFactory<out TVM, out TM, out TDevice>
        where TVM : class, IDeviceViewModel<TM>
        where TM : class, IDeviceModel<TDevice>
    {
        public Func<TDevice, TM> ModelCreate { get; }
        public Func<TM, TVM> ViewModelCreate { get; }
        public ModuleTypeEnum ModuleType { get; }
    }

    public sealed class DeviceViewModelFactory
    {
        private DeviceViewModelFactory() { }

        private class internalfactry<TVM, TM, TDev>
            : IDeviceViewModelFactory<TVM, TM, TDev>
            where TVM : class, IDeviceViewModel<TM>
            where TM :class, IDeviceModel<TDev>
            where TDev : class, IDevice<IDeviceState<IPacketDeviceData>>
        {
            public ModuleTypeEnum ModuleType { get;set;}
            public Func<TDev, TM> ModelCreate { get; set; }
            public Func<TM, TVM> ViewModelCreate { get; set; }
        }

        public readonly static IDeviceViewModelFactory<MotherBoardViewModel,
                                                       MotherBoardModel,
                                                       MotherBoard                                                       > MotherBoardVmFactory
            = new internalfactry<MotherBoardViewModel,
                                MotherBoardModel,
                                MotherBoard>
            {
                ModuleType = ModuleTypeEnum.MotherBoard,
                ViewModelCreate = (model) => new MotherBoardViewModel(model??DeviceViewModelFactory.MotherBoardVmFactory.ModelCreate(null)),
                ModelCreate = (device) => new MotherBoardModel(device ?? DeviceFactory.MotherBoardFactory.DeviceCreate()),
            };

        public readonly static IDeviceViewModelFactory<TrainSensorViewModel,
                                                       TrainSensorModel, 
                                                       TrainSensor>            TrainSensorVmFactory
            = new internalfactry<TrainSensorViewModel, TrainSensorModel, TrainSensor> 
            {
                ModuleType = ModuleTypeEnum.TrainSensor,
                ViewModelCreate = (model) => new TrainSensorViewModel(model ?? DeviceViewModelFactory.TrainSensorVmFactory.ModelCreate(null)),
                ModelCreate = dev => new TrainSensorModel(dev ?? DeviceFactory.TrainSensorFactory.DeviceCreate()),
            };

        public readonly static IDeviceViewModelFactory<PointModuleViewModel,
                                                       PointModuleModel,
                                                       PointModule>            PointModuleVmFactry
            = new internalfactry<PointModuleViewModel, PointModuleModel, PointModule>()
            {
                ModuleType = ModuleTypeEnum.PointModule,
                ViewModelCreate = model => new PointModuleViewModel(model ?? DeviceViewModelFactory.PointModuleVmFactry.ModelCreate(null)),
                ModelCreate = dev => new PointModuleModel(dev ?? DeviceFactory.PointModuleFactory.DeviceCreate()),
            };

        public readonly static IDeviceViewModelFactory<TrainControllerViewModel,
                                                       TrainControllerModel,
                                                       TrainController                                                       > TrainControllerVmFactry
            = new internalfactry<TrainControllerViewModel, TrainControllerModel, TrainController>()
            {
                ModuleType = ModuleTypeEnum.TrainController
                ViewModelCreate = model => new TrainControllerViewModel(model ?? DeviceViewModelFactory.TrainControllerVmFactry.ModelCreate(null)),
                ModelCreate = dev => new TrainControllerModel(dev ?? DeviceFactory.TrainControllerFactory.DeviceCreate()),
            };

        public static readonly IEnumerable<IDeviceViewModelFactory<IDeviceViewModel<IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>>, 
                                                                    IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>,
                                                                    IDevice<IDeviceState<IPacketDeviceData>>>> Factries
            =  new IDeviceViewModelFactory<IDeviceViewModel<IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>>,
                                                                    IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>,
                                                                    IDevice<IDeviceState<IPacketDeviceData>>> [] { MotherBoardVmFactory, TrainSensorVmFactory, PointModuleVmFactry, TrainControllerVmFactry };


    }
}
