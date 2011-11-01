
#define TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using SensorLibrary;
using SensorLivetView.Models;
using SensorLivetView.Models.Devices;
using SensorLivetView.ViewModels.Controls;

using LibUsbDotNet;

namespace SensorLivetView.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        /*コマンド、プロパティの定義にはそれぞれ 
         * 
         *  ldcom   : DelegateCommand(パラメータ無)
         *  ldcomn  : DelegateCommand(パラメータ無・CanExecute無)
         *  ldcomp  : DelegateCommand(型パラメータ有)
         *  ldcompn : DelegateCommand(型パラメータ有・CanExecute無)
         *  lprop   : 変更通知プロパティ
         *  
         * を使用してください。
         */

        /*ViewModelからViewを操作したい場合は、
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信してください。
         */

        /*
         * UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         */

        /*
         * Modelからの変更通知などの各種イベントをそのままViewModelで購読する事はメモリリークの
         * 原因となりやすく推奨できません。ViewModelHelperの各静的メソッドの利用を検討してください。
         */

        public UsbDevicesViewModel usbdevVm { get; private set; }
        private Dispatcher _associatedDispatcher;
        public Dispatcher AssociatedDispatcher
        {
            get
            {

                return this._associatedDispatcher;
            }
            set
            {
                this._associatedDispatcher = value;

                this.motherboardVmDispat = this.createDispat<MotherBoardModel, MotherBoard, MotherBoardState>();
                this.tsensorVmDispat = this.createDispat<TrainSensorModel, TrainSensor, TrainSensorState>();
                this.tcontrollerDispat = this.createDispat<TrainControllerDeviceModel, TrainController, TrainControllerState>();
                this.pmoduleDispat = this.createDispat<PointModuleModel, PointModule, PointModuleState>();

                this.motherboardVmDispat.projected.CollectionChanged += (sender, e) => RaisePropertyChanged(() => AvailableMotherBoardVMs);
                this.tsensorVmDispat.projected.CollectionChanged += (sender, e) => RaisePropertyChanged(() => AvailableTrainSensorVMs);
                this.tcontrollerDispat.projected.CollectionChanged += (sender, e) => RaisePropertyChanged(() => AvailableTrainControllerVMs);
                this.pmoduleDispat.projected.CollectionChanged += (sender, e) => RaisePropertyChanged(() => AvailablePointModuleVMs);
//#if TEST
//                if (!this.OpeningServers.Contains(this.testserv))
//                {
//                    this.OpeningServers.Add(this.testserv);
//                    LoggingStart(this.testserv);
//                }
//#endif
            }
        }

        public MainWindowViewModel()
        {
            this.InitUsbPeripherals();

            this.OpeningServers = new ObservableCollection<PacketServer>();
            this.OpeningServers.CollectionChanged += servlist_CollectionChanged;


        }

        void servlist_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var newserv in e.NewItems.Cast<PacketServer>())
                {
                    newserv.AddAction(this.motherboardVmDispat.dispat);
                    newserv.AddAction(this.tsensorVmDispat.dispat);
                    newserv.AddAction(this.tcontrollerDispat.dispat);
                    newserv.AddAction(this.pmoduleDispat.dispat);
                }
            }
        }

        public void InitUsbPeripherals()
        {
            var model = new UsbDevicesModel()
            {
                ProductId = 0x0204,
                VenderId = 0x04D8,
                ModelRegisteredStateChanged = new Notificator<UsbDeviceRegisteredEventArgs>()
            };
            model.ModelRegisteredStateChanged.Raised += (sender, e) =>
                {
                    if (e.RegistryModel.IsRegistered)
                        listenDevice(e.RegistryModel);
                    else
                        closeDevice(e.RegistryModel);
                };

            this.usbdevVm = new UsbDevicesViewModel()
            {
                Model = model,
            };
        }

#if TEST
        //private PacketServer _cache_testserv = null;
        //private PacketServer testserv
        //{
        //    get
        //    {
        //        if (_cache_testserv == null)
        //        {
        //            var testenum = new TestEnumerable().SetMotherBoard(new DeviceID(1, 0))
        //                                               .SetTrainSensors(new DeviceID(1, 1))
        //                                               .SetTrainSensors(new DeviceID(1, 2))
        //                                               .SetTrainSensors(new DeviceID(1, 3))
        //                                               .SetTrainSensors(new DeviceID(1, 4))
        //                                               .SetPointModules(new DeviceID(1, 5))
        //                                               .SetController(new DeviceID(1, 6))
        //                                               .ToEnumerable();
        //            var testserv = new TestServer(testenum);
        //            //testdisp.ReceivedMotherBoardChanged += (sender, e) => this.RaisePropertyChanged("");
        //            testserv.LoopStart();
        //            this._cache_testserv = testserv;
        //        }
        //        return this._cache_testserv;
        //    }
        //}


        System.IO.StreamWriter logsw = new System.IO.StreamWriter("log.csv");
        private void LoggingStart(PacketServer serv)
        {

            var logging = new PacketServerAction((state) => Console.WriteLine(state.ToString()));
            var putting = new PacketServerAction(state =>
                {
                    if (state.BasePacket.ModuleType == ModuleTypeEnum.TrainController)
                        logsw.WriteLine(state.ToString());
                });

            serv.AddAction(logging);
            serv.AddAction(putting);
        }
#endif

        private void listenDevice(UsbRegistryModel usbm)
        {
            if (usbm == null)
                return;

            //device open to listen stream
            var dev = usbm.Registry.Device;

            if (!dev.IsOpen)
            {
                dev.Open();
            }
            if (!dev.IsOpen)
                return;

            var cnt = new USBDeviceController(dev);
            cnt.Open();

            var serv = new PacketServer() { Controller = cnt };

#if TEST
            LoggingStart(serv);
#endif

            if (!serv.IsLooping)
                serv.LoopStart();
            this.OpeningServers.Add(serv);
        }

        private void closeDevice(UsbRegistryModel usbm)
        {
            if (usbm == null)
                return;

            var dev = usbm.Registry.Device;
            if (dev != null && dev.IsOpen)
            {
                dev.Close();
            }

        }

        ObservableCollection<PacketServer> _OpeningServers;

        public ObservableCollection<PacketServer> OpeningServers
        {
            get
            { return _OpeningServers; }
            set
            {
                if (_OpeningServers == value)
                    return;
                _OpeningServers = value;
                RaisePropertyChanged("OpeningServers");
            }
        }

        private DeviceViewModelDispatcher<MotherBoardModel, MotherBoard, MotherBoardState> motherboardVmDispat;
        public IEnumerable<MotherBoardViewModel> AvailableMotherBoardVMs
        {
            get
            {
                if (this.motherboardVmDispat == null)
                    return new MotherBoardViewModel [] { };
                else
                    return this.motherboardVmDispat.projected.Cast<MotherBoardViewModel>();
            }
        }

        private DeviceViewModelDispatcher<TrainSensorModel, TrainSensor, TrainSensorState> tsensorVmDispat;
        public IEnumerable<TrainSensorViewModel> AvailableTrainSensorVMs
        {
            get
            {
                if (this.tsensorVmDispat == null)
                    return new TrainSensorViewModel [] { };
                else
                    return this.tsensorVmDispat.projected.Cast<TrainSensorViewModel>();
            }
        }

        private DeviceViewModelDispatcher<TrainControllerDeviceModel, TrainController, TrainControllerState> tcontrollerDispat;
        public IEnumerable<TrainControllerDeviceViewModel> AvailableTrainControllerVMs
        {
            get
            {
                if (this.tcontrollerDispat == null)
                    return new TrainControllerDeviceViewModel [] { };
                else
                    return this.tcontrollerDispat.projected.Cast<TrainControllerDeviceViewModel>();
            }
        }

        private DeviceViewModelDispatcher<PointModuleModel, PointModule, PointModuleState> pmoduleDispat;
        public IEnumerable<PointModuleViewModel> AvailablePointModuleVMs
        {
            get
            {
                if (this.pmoduleDispat == null)
                    return new PointModuleViewModel [] { };
                else
                    return this.pmoduleDispat.projected.Cast<PointModuleViewModel>();
            }

        }

        private DeviceViewModelDispatcher<TModel, TDevice, TState> createDispat<TModel, TDevice, TState>()
            where TModel : class, IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>
            where TDevice : class, IDevice<IDeviceState<IPacketDeviceData>>
            where TState : class, IDeviceState<IPacketDeviceData>
        //where TVM : DeviceViewModel<DeviceModel<TDevice>>
        {
            return new DeviceViewModelDispatcher<TModel, TDevice, TState>()
            {
                dispat = new PacketDispatcherSingle<TDevice, TState>(),
                projected = new ObservableWrappingCollectionOnDispat<TDevice, IDeviceViewModel<IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>>>()
                {
                    Dispatcher = this.AssociatedDispatcher,
                },
            };
        }

        public TrainSpeedTransitionTestViewModel SpeedTestVM
        {
            get
            {
                var vm = new TrainSpeedTransitionTestViewModel()
                {
                    ControllerCandicates = this.AvailableTrainControllerVMs,
                    SensorCandicates = this.AvailableTrainSensorVMs,
                    Model = new TrainSpeedTransitionTest(),
                };

                return vm;
            }
        }


    }
}
