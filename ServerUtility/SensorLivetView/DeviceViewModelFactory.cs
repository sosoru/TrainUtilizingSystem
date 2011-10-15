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
        where TDevice : class, IDevice<IDeviceState<IPacketDeviceData>>
    {

        Func<TM> ModelCreate { get; }
        Func<TVM> ViewModelCreate { get; }
        ModuleTypeEnum ModuleType { get; }
    }

    public sealed class DeviceViewModelFactory
    {
        private DeviceViewModelFactory() { }

        private class internalfactry<TVM, TM, TDev>
            : IDeviceViewModelFactory<TVM, TM, TDev>
            where TVM : class, IDeviceViewModel<TM>, new()
            where TM : class, IDeviceModel<TDev>, new()
            where TDev : class, IDevice<IDeviceState<IPacketDeviceData>>, new()
        {
            public internalfactry(ModuleTypeEnum mtype)
            {
                this.ModuleType = mtype;

                this.ModelCreate = () => new TM();
                this.ViewModelCreate = () => new TVM();
            }

            public ModuleTypeEnum ModuleType { get; set; }
            public Func<TM> ModelCreate { get; set; }
            public Func<TVM> ViewModelCreate { get; set; }
        }

        public readonly static IDeviceViewModelFactory<MotherBoardViewModel, MotherBoardModel, MotherBoard> MotherBoardVmFactory
            = new internalfactry<MotherBoardViewModel, MotherBoardModel, MotherBoard>(ModuleTypeEnum.MotherBoard);

        public readonly static IDeviceViewModelFactory<TrainSensorViewModel, TrainSensorModel, TrainSensor> TrainSensorVmFactory
            = new internalfactry<TrainSensorViewModel, TrainSensorModel, TrainSensor>(ModuleTypeEnum.TrainSensor);

        public readonly static IDeviceViewModelFactory<PointModuleViewModel, PointModuleModel, PointModule> PointModuleVmFactory
            = new internalfactry<PointModuleViewModel, PointModuleModel, PointModule>(ModuleTypeEnum.PointModule);

        public readonly static IDeviceViewModelFactory<TrainControllerViewModel, TrainControllerModel, TrainController> TrainControllerVmFactory
            = new internalfactry<TrainControllerViewModel, TrainControllerModel, TrainController>(ModuleTypeEnum.TrainController);

        public static readonly IEnumerable<IDeviceViewModelFactory<IDeviceViewModel<IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>>, 
                                                                    IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>,
                                                                    IDevice<IDeviceState<IPacketDeviceData>>>> Factries
            =  new IDeviceViewModelFactory<IDeviceViewModel<IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>>,
                                                                    IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>,
                                                                    IDevice<IDeviceState<IPacketDeviceData>>> [] { MotherBoardVmFactory, TrainSensorVmFactory, PointModuleVmFactory, TrainControllerVmFactory };


    }
}
