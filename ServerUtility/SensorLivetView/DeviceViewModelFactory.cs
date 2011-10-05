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
    public interface IDeviceViewModelFactory< in TDev, out TVM>
    {
        Func<TDev, TVM> ViewModelCreate { get; }
        ModuleTypeEnum ModuleType { get; }
    }

    public sealed class DeviceViewModelFactory
    {
        private DeviceViewModelFactory() { }

        private class internalfactry<TDev, TVM>
            : IDeviceViewModelFactory<TDev, TVM>
            where TDev : IDevice<IDeviceState<IPacketDeviceData>>
            where TVM : ViewModel
        {
            public Func<TDev, TVM> ViewModelCreate { get; set; }
            public ModuleTypeEnum ModuleType { get; set; }
        }

        public readonly static IDeviceViewModelFactory<IDevice<IDeviceState<IPacketDeviceData>>, MotherBoardViewModel> MotherBoardVmFactory
            = new internalfactry<IDevice<IDeviceState<IPacketDeviceData>>, MotherBoardViewModel>()
            {
                ViewModelCreate = (mb) =>
                {
                    var model = new MotherBoardModel(mb as MotherBoard);
                    var vm = new MotherBoardViewModel(model);
                    return vm;
                },
                ModuleType = ModuleTypeEnum.MotherBoard,
            };

        public readonly static IDeviceViewModelFactory<IDevice<IDeviceState<IPacketDeviceData>>, TrainSensorViewModel> TrainSensorVmFactory
            = new internalfactry<IDevice<IDeviceState<IPacketDeviceData>>, TrainSensorViewModel>()
            {
                ViewModelCreate = (tsens) =>
                    {
                        return new TrainSensorViewModel(new TrainSensorModel(tsens as TrainSensor));
                    },
                ModuleType = ModuleTypeEnum.TrainSensor,
            };

        public readonly static IDeviceViewModelFactory<IDevice<IDeviceState<IPacketDeviceData>>, PointModuleViewModel> PointModuleVmFactry
            = new internalfactry<IDevice<IDeviceState<IPacketDeviceData>>, PointModuleViewModel>()
            {
                ViewModelCreate = (pm) =>
                    {
                        return new PointModuleViewModel(new PointModuleModel(pm as PointModule));
                    },
                ModuleType = ModuleTypeEnum.PointModule,
            };

        public readonly static IDeviceViewModelFactory<IDevice<IDeviceState<IPacketDeviceData>>,TrainControllerViewModel> TrainControllerVmFactry
            = new internalfactry<IDevice<IDeviceState<IPacketDeviceData>>, TrainControllerViewModel>()
            {
                ViewModelCreate = (tcont) =>
                {
                    return new TrainControllerViewModel(new TrainControllerModel(tcont as TrainController));
                },
                ModuleType = ModuleTypeEnum.TrainController,
            };

        public static readonly IEnumerable<IDeviceViewModelFactory<IDevice<IDeviceState<IPacketDeviceData>>, ViewModel>> Factries
            =  new IDeviceViewModelFactory<IDevice<IDeviceState<IPacketDeviceData>>, ViewModel> [] { MotherBoardVmFactory, TrainSensorVmFactory, PointModuleVmFactry, TrainControllerVmFactry };


    }
}
