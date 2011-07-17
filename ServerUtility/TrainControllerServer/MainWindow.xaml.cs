using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.IO.Ports;
using SensorLibrary;
using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace TrainControllerServer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        const string SYMBOL_TEST = "test Server";

        //private SerialPort sport = null;
        private Stream usingStream = null;

        public MainWindow()
        {
            InitializeComponent();

            foreach (UsbRegistry dev in UsbDevice.AllDevices.FindAll(new UsbDeviceFinder(0x04D8, 0x0204)))
                this.PortListView.Items.Add(dev.SymbolicName);

#if DEBUG
            this.PortListView.Items.Add(SYMBOL_TEST);
#endif

            //foreach (var port in SerialPort.GetPortNames())
            //    this.PortListView.Items.Add(port);

        }

        private void PortListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (//this.sport != null ||
                this.usingStream != null ||
                e.AddedItems == null || e.AddedItems.Count != 1)
                return;

            this.Title = "opening...";

            //var sport = new SerialPort((string)e.AddedItems[0], 9600, Parity.None, 8, StopBits.One);
            //sport.Encoding = Encoding.UTF8;
            //sport.NewLine = "\n";
            //sport.RtsEnable = true; 
            //sport.DtrEnable = true;
            //sport.DiscardNull = false;
            //sport.ReadTimeout = 10000;

            //int counter = 0;
            //while (true)
            //{
            //    try
            //    {
            //        sport.Open();
            //        break;
            //    }
            //    catch (IOException)
            //    {
            //        Console.WriteLine(counter);
            //        if (++counter > 10)
            //            throw;
            //    }
            //}

            var selected = (string)e.AddedItems[0];
            Stream st = null;
            if (selected == SYMBOL_TEST)
            {
                var timerlist = new List<DevicePacket>();
                var rnd = new Random();
                int timerlistlen = 500;
                for (int i = 0; i < timerlistlen; i++)
                {
                    var basepack = new DevicePacket()
                    {
                        ID = new DeviceID() { ModulePart = 1, ParentPart = 1 },
                        ModuleType = ModuleTypeEnum.TrainSensor,
                    };

                    var sensorstate = new TrainSensorState(basepack)
                    {
                        CurrentVoltage = (float)(rnd.NextDouble() * 5.0),
                        ThresholdVoltage = 3.0F,
                        Timer = (ushort)((float)ushort.MaxValue * ((float)i / (float)timerlistlen)),
                    };
                    sensorstate.FlushDataState();
                    timerlist.Add(basepack);
                }
                // bm packet
                var mblist = new List<DevicePacket>();
                {
                    var packet = new DevicePacket()
                    {
                        ID = new DeviceID() { ModulePart = 0, ParentPart = 1 },
                        ModuleType= ModuleTypeEnum.MotherBoard,
                    };
                    var mbstate = new MotherBoardState(packet)
                    {
                        ParentID = 1,
                        Timer = 0xFFFF
                    };
                    Enumerable.Range(0, mbstate.Data.ModuleType.Length - 1).ToList().ForEach((i) => mbstate[i] = ModuleTypeEnum.Unknown) ;
                    mbstate[0] = ModuleTypeEnum.MotherBoard;
                    mbstate[1] = ModuleTypeEnum.TrainSensor;

                    mbstate.FlushDataState();
                }
                st = new TestPacketStream( Enumerable.Concat((timerlist.OrderBy((pack) => (new TrainSensorState(pack)).Data.Timer)),
                                                             mblist)
                                                             .ToArray());
            }
            else
            {
                var dev = UsbDevice.OpenUsbDevice((reg) => reg.SymbolicName == selected);
                var idev = dev as IUsbDevice;
                idev.SetConfiguration(1);
                idev.ClaimInterface(0);

                st = new USBStream(dev);
                (st as USBStream).Open();
            }
            this.usingStream = st;

            this.Title = "opened successfully";

            var server = new PacketServer(this.usingStream);
            var dispatcher = new PacketDispatcher();
            var mb = dispatcher.GetMotherBoard(new DeviceID() { ParentPart = 1, ModulePart = 0 });
            var cens = dispatcher.GetTrainSensor(new DeviceID() { ParentPart = 1, ModulePart = 1 });
            server.AddAction(dispatcher);
            server.LoopStart();

            this.sensor.DataContext = new TrainSensorViewModel(cens);
            this.board.DataContext = new MotherBoardViewModel(mb);
        }

    }
}
