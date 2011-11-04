
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
#if TEST
                if (!this.OpeningServers.Contains(this.testserv))
                {
                    this.OpeningServers.Add(this.testserv);
                    LoggingStart(this.testserv);
                }
#endif
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
                    foreach (var dispat in new PacketDispatcher [] { this.motherboardVmDispat.dispat, this.tsensorVmDispat.dispat, this.tcontrollerDispat.dispat, pmoduleDispat.dispat })
                    {
                        ViewModelHelper.BindNotifyCollectionChanged(dispat.DeviceFoundNotifier, this, (obj, args) =>
                            {
                                RaisePropertyChanged(() => this.Manager);
                            });
                        newserv.AddAction(dispat);
                    }
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
        private PacketServer _cache_testserv = null;
        private PacketServer testserv
        {
            get
            {
                if (_cache_testserv == null)
                {
                    var testenum = new TestEnumerable().SetMotherBoard(new DeviceID() { ParentPart = 1, ModuleAddr = 0 })
                                                       .SetPointModules(new DeviceID() { ParentPart = 1, ModuleAddr = 1 })
                                                       .SetTrainDetectingSensor(new DeviceID() { ParentPart = 1, ModuleAddr = 2, InternalAddr = 1 })
                                                       .SetTrainSensors(new DeviceID() { ParentPart = 1, ModuleAddr = 2, InternalAddr = 2 })
                                                       .SetController(new DeviceID() { ParentPart = 1, ModuleAddr = 3 })
                                                       .ToEnumerable();
                    var testserv = new PacketServer() { Controller = new DeviceIoByEnumerable(testenum) };
                    //testdisp.ReceivedMotherBoardChanged += (sender, e) => this.RaisePropertyChanged("");
                    testserv.LoopStart();
                    this._cache_testserv = testserv;
                }
                return this._cache_testserv;
            }
        }


#endif
        System.IO.StreamWriter logsw = new System.IO.StreamWriter("log.csv");
        private void LoggingStart(PacketServer serv)
        {

            //var logging = new PacketServerAction((state) => Console.WriteLine(state.ToString()));
            var putting = new PacketServerAction(state =>
                {
                    var id = state.BasePacket.ID;

                    if (id.InternalAddr == 1 && id.ModuleAddr == 1)
                        logsw.WriteLine(DateTime.Now.ToString() + state.ToString());
                });

            //serv.AddAction(logging);
            serv.AddAction(putting);
        }

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

            //#if TEST
            LoggingStart(serv);
            //#endif

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

        public LineManagerViewModel Manager
        {
            get
            {
                var convm = this.AvailableTrainControllerVMs.FirstOrDefault(vm => vm.DevID.IsMatched(1, 3, -1) );

                if (convm == null)
                    return null;

                var lvm = new LineManagerViewModel(convm);
                this.CreateStations(0).ForEach(sta => lvm.Stations.Add(sta));
                this.CreatePointStrategy(0).ForEach(pt => lvm.PointStrategies.Add(pt));

                return lvm;
            }
        }

        public IEnumerable<StationViewModel> CreateStations(int num)
        {
            // todo : for 1117

            var controller = this.AvailableTrainControllerVMs.FirstOrDefault(vm => vm.DevID.IsMatched(1, 3, -1) );
            var stoppingsens = this.AvailableTrainSensorVMs.FirstOrDefault(vm => vm.DevID.IsMatched(1, 2, 1) );
            var haltsens = this.AvailableTrainSensorVMs.FirstOrDefault(vm => vm.DevID.IsMatched(1, 2, 2) );

            if (controller == null || stoppingsens == null || haltsens == null)
                throw new InvalidOperationException("device not found");

            var sta = new StationViewModel(controller.Model, stoppingsens.Model, haltsens.Model)
            {
                IntervalStoppingPos = 2000,
                IsHalt = true,
                StoppingTime = new TimeSpan(0, 0, 10),
                StationName = "test sta",
            };

            yield return sta;
        }

        public IEnumerable<PointStrategyViewModel> CreatePointStrategy(int num)
        {
            // todo : for 1117

            var points = this.AvailablePointModuleVMs.FirstOrDefault(vm => vm.DevID.IsMatched(1, 1, -1));
            if (points == null)
                throw new InvalidOperationException("point not found");

            var pvm = new ManyPointStrategyViewModel()
            {
                StrategyName = "test point",
            };

            pvm.Points.Add(points.PointModels.First().Model);
            pvm.InversePoints.Add(points.PointModels.Last().Model);

            yield return pvm;
        }
    }
}
