
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
            get{

                return this._associatedDispatcher;
            }
            set{
                this._associatedDispatcher = value;

                this.motherboardVmDispat = this.createDispat<MotherBoard, MotherBoardState, MotherBoardViewModel>();
                this.tsensorVmDispat = this.createDispat<TrainSensor, TrainSensorState, TrainSensorViewModel>();
                this.tcontrollerDispat = this.createDispat<TrainController, TrainControllerState, TrainControllerViewModel>();
                this.pmoduleDispat = this.createDispat<PointModule, PointModuleState, PointModuleViewModel>();
                this.remmoduleDispat = this.createDispat<RemoteModule, RemoteModuleState, RemoteModuleViewModel>();
                RaisePropertyChanged("");
            }
        }

        public MainWindowViewModel()
        {
            this.InitUsbPeripherals();

            this.OpeningServers = new ObservableCollection<PacketServer>();
            this.OpeningServers.CollectionChanged += servlist_CollectionChanged;

//#if TEST
//            if (!this.OpeningServers.Contains(this.testserv))
//                this.OpeningServers.Add(this.testserv);
//#endif
            
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
                    newserv.AddAction(this.remmoduleDispat.dispat);
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

        private PacketServer _cache_testserv = null;
        private PacketServer testserv
        {
            get
            {
                if (_cache_testserv == null)
                {
                    var testenum = new TestEnumerable().SetMotherBoard(new DeviceID(1, 0))
                                                       .SetTrainSensors(new DeviceID(1, 1))
                                                       .SetTrainSensors(new DeviceID(1, 2))
                                                       .SetTrainSensors(new DeviceID(1, 3))
                                                       .SetTrainSensors(new DeviceID(1, 4))
                                                       .SetPointModules(new DeviceID(1,5))
                                                       .ToEnumerable();
                    var testserv = new TestServer(testenum);
                    //testdisp.ReceivedMotherBoardChanged += (sender, e) => this.RaisePropertyChanged("");
                    testserv.LoopStart();
                    this._cache_testserv = testserv;
                }
                return this._cache_testserv;
            }
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

            var st = new USBStream(dev);
            st.Open();

            var serv = new PacketServer(st);

#if TEST
            var logging = new PacketServerAction((state) => Console.WriteLine(state.ToString()));
            serv.AddAction(logging);
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

        private DeviceViewModelDispatcher<MotherBoard, MotherBoardState, MotherBoardViewModel> motherboardVmDispat
            = new DeviceViewModelDispatcher<MotherBoard, MotherBoardState, MotherBoardViewModel>();
        public IList<MotherBoardViewModel> AvailableMotherBoardVMs
        {
            get
            {
                return this.motherboardVmDispat.projected;
            }
        }

        private DeviceViewModelDispatcher<TrainSensor, TrainSensorState, TrainSensorViewModel> tsensorVmDispat;
        public IList<TrainSensorViewModel> AvailableTrainSensorVMs
        {
            get
            {
                return this.tsensorVmDispat.projected;
            }
        }

        private DeviceViewModelDispatcher<TrainController, TrainControllerState, TrainControllerViewModel> tcontrollerDispat;
        public IList<TrainControllerViewModel> AvailableTrainControllerVMs
        {
            get
            {
                return this.tcontrollerDispat.projected;
            }
        }

        private DeviceViewModelDispatcher<PointModule, PointModuleState, PointModuleViewModel> pmoduleDispat;
        public IList<PointModuleViewModel> AvailablePointModuleVMs
        {
            get
            {
                return this.pmoduleDispat.projected;
            }

        }

        private DeviceViewModelDispatcher<RemoteModule, RemoteModuleState, RemoteModuleViewModel> remmoduleDispat;
        public IList<RemoteModuleViewModel> AvailableRemoteModuleVMs
        {
            get
            {
                return this.remmoduleDispat.projected;
            }
        }

        private DeviceViewModelDispatcher<TDevice, TState, TVM> createDispat<TDevice, TState, TVM> ()
            where TDevice : class, IDevice<TState>
            where TState : class, IDeviceState<IPacketDeviceData>
            where TVM : DeviceViewModel<TDevice>
        {
            return new DeviceViewModelDispatcher<TDevice, TState, TVM>()
            {
                dispat = new PacketDispatcherSingle<TDevice, TState>(),
                projected = new ObservableWrappingCollectionOnDispat<TDevice, TVM>()
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
