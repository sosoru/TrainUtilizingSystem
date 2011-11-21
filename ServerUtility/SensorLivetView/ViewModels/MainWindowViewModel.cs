
//#define TEST
#define LINE2011

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
                                RaisePropertyChanged(() => this.Managers);
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
                                                       .SetPointModules(new DeviceID() { ParentPart = 1, ModuleAddr = 2 })
                                                       .SetTrainDetectingSensor(new DeviceID() { ParentPart = 1, ModuleAddr = 1, InternalAddr = 1 })
                                                       .SetTrainSensors(new DeviceID() { ParentPart = 1, ModuleAddr = 1, InternalAddr = 2 })
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

        private Dictionary<UsbRegistryModel, PacketServer> listeingList = new Dictionary<UsbRegistryModel, PacketServer>();

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
            this.listeingList.Add(usbm, serv);

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

            try
            {
                var devpair = this.listeingList.First(pair => pair.Key == usbm);
                devpair.Value.LoopStop();
                this.listeingList.Remove(devpair.Key);
            }
            catch (KeyNotFoundException) { }
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

        public IEnumerable<LineManagerViewModel> Managers
        {
            get
            {
                foreach (var i in new int [] { 2 })
                {
                    var convm = this.AvailableTrainControllerVMs.FirstOrDefault(vm => vm.DevID.IsMatched(i, 3, -1));

                    if (convm == null)
                        break;

                    var lvm = new LineManagerViewModel(convm);
                    lvm.Name = "B";
                    this.CreateStations(i).ForEach(lvm.Stations.Add);
                    this.CreatePointStrategy(i).ForEach(lvm.PointStrategies.Add);

                    yield return lvm;
                }
            }
        }

        public IEnumerable<StationViewModel> CreateStations(int num)
        {
            // todo : for 1117
#if LINE2011
            var idmap = new [] { new { name = "abiko", stp = new int [] { 1, 1, 0 }, halt = new int [] { 1, 1, 1 } } };
#else
            var idmap = new [] { new { name = "test", stp = new int [] { 1, 2, 0 }, halt = new int [] { 1, 2, 1 } } };
#endif

            var controller = this.AvailableTrainControllerVMs.FirstOrDefault(vm => vm.DevID.IsMatched(num, 3, -1));

            foreach (var map in idmap)
            {
                var stoppingsens = this.AvailableTrainSensorVMs.FirstOrDefault(vm => vm.DevID.IsMatched(map.stp [0], map.stp [1], map.stp [2]));
                var haltsens = this.AvailableTrainSensorVMs.FirstOrDefault(vm => vm.DevID.IsMatched(map.halt [0], map.halt [1], map.halt [2]));

                if (controller == null || stoppingsens == null || haltsens == null)
                    continue;//throw new InvalidOperationException("device not found");

                var sta = new StationViewModel(controller.Model, stoppingsens.Model, haltsens.Model)
                {
                    IntervalStoppingPos = 2000,
                    IsHalt = true,
                    StoppingTime = new TimeSpan(0, 0, 10),
                    StationName = map.name,
                    DutyWhenHalt = 20.0,
                    LeavingVoltage = 150,
                    StepResolution = 100
                };

                yield return sta;
            }
        }

        public IEnumerable<PointStrategyViewModel> CreatePointStrategy(int num)
        {
            // todo : for 1117
#if LINE2011
            var map = new [] { new {name = "C", points = new int[][] { new int[] {3,1,4}}, invpoints = Enumerable.Range(0,4).Select(i=> new int[]{1,2,i}).ToArray()},
                               new {name = "D", points = new int[][]{ new int[] {3,1,7}}, invpoints = Enumerable.Range(4,4).Select(i=>new int[]{1,2,i}).ToArray() },
                               new {name="A", points = new int[][] { new int[] {3,1,6}}, invpoints = Enumerable.Range(0,4).Select(i=>new int[]{2,1,i}).ToArray()},
                               new {name = "B", points = new int[][] { new int[]{ 3,1,0}},invpoints = Enumerable.Range(4,4).Select(i=> new int[]{2,1,i}).ToArray()},
                               };
#else
            var map = new [] { new { name = "test", points = new int [] [] { new int [] { 1, 1, 0 } }, invpoints = new int [] [] { new int [] { 1, 1, 7 } } } };
#endif

            foreach (var m in map)
            {

                var pvm = new ManyPointStrategyViewModel()
                {
                    StrategyName = m.name,
                };

                var candicates = this.AvailablePointModuleVMs.SelectMany(vm => vm.PointModels).Select(vm => vm.Model);

                m.points.Select(p => candicates.FirstOrDefault(c => c.Address == p [2] && c.Parent.DevID.IsMatched(p [0], p [1], -1)))
                        .Where(model => model != null)
                        .ForEach(pvm.Points.Add);

                m.invpoints.Select(p => candicates.FirstOrDefault(c => c.Address == p [2] && c.Parent.DevID.IsMatched(p [0], p [1], -1)))
                            .Where(model => model != null)
                            .ForEach(pvm.InversePoints.Add);

                yield return pvm;

            }
        }
    }
}
