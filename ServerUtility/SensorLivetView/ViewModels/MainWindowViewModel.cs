
#define TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

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

        public int VenderID { get; set; }
        public int ProductID { get; set; }

        public MainWindowViewModel()
        {
            this.InitCandicateDevice();

            var sellist = new ObservableCollection<UsbDevicesViewModel>();
            sellist.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(sellist_CollectionChanged);

            this.SelectedDevices = sellist;
            this.OpeningServers = new ObservableCollection<PacketServer>();
            this.PacketDispatchers = new ObservableCollection<PacketDispatcherSingle>();

#if TEST
            if (!this.PacketDispatchers.Contains(this.testDisp))
                this.PacketDispatchers.Add(this.testDisp);
#endif
        }

        void sellist_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var item in e.NewItems.Cast<UsbDevicesViewModel>())
                    this.listenDevice(item);
            }
            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (var item in e.OldItems.Cast<UsbDevicesViewModel>())
                    this.closeDevice(item);
            }

        }

        public void InitCandicateDevice()
        {
            this.CandicateDevice = new UsbDevicesViewModel()
            {
                ProductID = this.ProductID,
                VenderID = this.VenderID,
            };
        }

        private PacketDispatcherSingle _cache_testdisp = null;
        private PacketDispatcherSingle testDisp
        {
            get
            {
                if (_cache_testdisp == null)
                {
                    var testenum = new TestEnumerable().SetMotherBoard(new DeviceID(1, 0))
                                                       .SetTrainSensors(new DeviceID(1, 1))
                                                       .SetTrainSensors(new DeviceID(1, 2))
                                                       .SetTrainSensors(new DeviceID(1, 3))
                                                       .SetTrainSensors(new DeviceID(1, 4))
                                                       .SetPointModules(new DeviceID(1,5))
                                                       .ToEnumerable();
                    var testserv = new TestServer(testenum);
                    var testdisp = new PacketDispatcherSingle();
                    testserv.AddAction(testdisp);
                    this.OpeningServers.Add(testserv);
                    this.PacketDispatchers.Add(testdisp);
                    //testdisp.ReceivedMotherBoardChanged += (sender, e) => this.RaisePropertyChanged("");
                    testserv.LoopStart();

                    this._cache_testdisp = testdisp;
                }
                return this._cache_testdisp;
            }
        }

        private void listenDevice(UsbDevicesViewModel usbvm)
        {
            if (usbvm == null || usbvm.SelectedDeviceReg == null)
                return;

            //device open to listen stream
            var dev = usbvm.SelectedDeviceReg.Device;

            if (!dev.IsOpen)
            {
                dev.Open();
            }
            if (!dev.IsOpen)
                return;

            var st = new USBStream(dev);
            st.Open();

            var serv = new PacketServer(st);
            var dispat = new PacketDispatcherSingle();

#if TEST
            var logging = new PacketServerAction((state) => Console.WriteLine(state.ToString()));
            serv.AddAction(logging);
#endif

            serv.AddAction(dispat);
            if (!serv.IsLooping)
                serv.LoopStart();
            this.OpeningServers.Add(serv);
            this.PacketDispatchers.Add(dispat);
            dispat.FoundDeviceList.CollectionChanged += (sender, e) => base.RaisePropertyChanged("");
            RaisePropertyChanged("");
        }

        private void closeDevice(UsbDevicesViewModel usbvm)
        {
            if (usbvm == null)
                return;

            var dev = usbvm.SelectedDeviceReg.Device;
            if (dev != null && dev.IsOpen)
            {
                dev.Close();
            }

        }

        IList<PacketServer> _OpeningServers;

        public IList<PacketServer> OpeningServers
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


        ObservableCollection<PacketDispatcherSingle> _PacketDispatchers;

        public ObservableCollection<PacketDispatcherSingle> PacketDispatchers
        {
            get
            { return _PacketDispatchers; }
            set
            {
                if (_PacketDispatchers == value)
                    return;
                _PacketDispatchers = value;
                RaisePropertyChanged("PacketDispatchers");
            }
        }

        public IList<MotherBoardViewModel> AvailableMotherBoardVMs
        {
            get
            {
                return GetAvailableModules<MotherBoardViewModel, MotherBoard>(this.PacketDispatchers);
            }
        }

        public IList<TrainSensorViewModel> AvailableTrainSensorVMs
        {
            get
            {
                return GetAvailableModules<TrainSensorViewModel, TrainSensor>(this.PacketDispatchers);
            }
        }

        public IList<TrainControllerViewModel> AvailableTrainControllerVMs
        {
            get
            {
                return GetAvailableModules<TrainControllerViewModel, TrainController>(this.PacketDispatchers);
            }
        }

        public IList<PointModuleViewModel> AvailablePointModuleVMs
        {
            get
            {
                return GetAvailableModules<PointModuleViewModel, PointModule>(this.PacketDispatchers);
            }

        }

        public IList<RemoteModuleViewModel> AvailableRemoteModuleVMs
        {
            get
            {
                return GetAvailableModules<RemoteModuleViewModel, RemoteModule>(this.PacketDispatchers);
            }
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

        private IList<TVM> GetAvailableModules<TVM, TM>(IEnumerable<PacketDispatcherSingle> disps)
            where TVM : class
            where TM : class
        {
            return disps.SelectMany((disp) => disp.FoundDeviceList)
                        .Where((cnt)=> cnt is TM)
                        .Select((cnt) => typeof(TVM).GetConstructor(new[] { typeof(TM) }).Invoke(new[] { cnt }) as TVM)
                        .ToList();
        }

        IList<UsbDevicesViewModel> _SelectedDevices;

        public IList<UsbDevicesViewModel> SelectedDevices
        {
            get
            { return _SelectedDevices; }
            set
            {
                if (_SelectedDevices == value)
                    return;
                _SelectedDevices = value;
                RaisePropertyChanged("SelectedDevices");
            }
        }


        UsbDevicesViewModel _CandicateDevice;

        public UsbDevicesViewModel CandicateDevice
        {
            get
            { return _CandicateDevice; }
            set
            {
                if (_CandicateDevice == value)
                    return;
                _CandicateDevice = value;
                RaisePropertyChanged("CandicateDevice");
            }
        }

        public void PickUpCandicateDevice()
        {
            if (this.CandicateDevice != null && this.CandicateDevice.SelectedDeviceReg != null)
            {
                if (!this.SelectedDevices.Any((vm) => vm.SelectedDeviceReg == this.CandicateDevice.SelectedDeviceReg))
                {
                    this.SelectedDevices.Add(this.CandicateDevice);
                    this.InitCandicateDevice();
                    RaisePropertyChanged("");
                }
            }
        }

    }
}
