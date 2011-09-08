using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SensorLivetView.ViewModels;
using SensorLivetView.ViewModels.Controls;
using SensorLibrary;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

namespace SensorLivetView
{
    /// <summary>
    /// TestWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TestWindow : Window
    {
        private PacketServer server;
        private PacketDispatcher dispatcher;

        public TestWindow()
        {
            this.InitializeComponent();

            // オブジェクト作成に必要なコードをこの下に挿入します。
            this.server = new TestServer(testEnumerable);
            this.dispatcher = new PacketDispatcher();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var tsensvm = new TrainSensorViewModel(new MeisuringTrainSensor() { DeviceID = new DeviceID(0x01, 0x01), });
            tsensvm.Model.Observe(this.dispatcher);
            this.tsensview.DataContext = tsensvm;

            var tctrlvm = new TrainControllerViewModel(new TrainController(){ DeviceID = new DeviceID(0x01, 0x06)});
            tctrlvm.Model.Observe(this.dispatcher);
            this.tctrl.DataContext = tctrlvm;

            this.server.AddAction(this.dispatcher);
            this.server.LoopStart();

        }

        private IEnumerable<DevicePacket> testEnumerable
        {
            get
            {
                var tsenses = Enumerable.Range(0, 256)
                                        .Select((i) => new TrainSensorState(){
                                                    BasePacket =  new DevicePacket()
                                                            {
                                                                ID = new DeviceID()
                                                                    {
                                                                        ParentPart = 0x01,
                                                                        ModulePart = 0x01,
                                                                    },
                                                                ModuleType = ModuleTypeEnum.TrainSensor,
                                                                ReadMark = 0xFF,

                                                            },
                                                    ReceivingServer = this.server,
                                                    Mode = TrainSensorMode.meisuring,
                                                    ReferenceVoltageMinus = 0.0F,
                                                    ReferenceVoltagePlus = 5.0F,
                                                    VoltageResolution = 10,
                                                    ThresholdVoltage = 2.0F,
                                                    CurrentVoltage = (float)(5.0 * (double)i / 256.0),
                                                    Timer = (ushort)(i * 256),
                                                    //IsDetected = i % 2 == 0,
                                                });

                var mbpacket = new DevicePacket()
                {
                    ID = new DeviceID()
                    {
                        ParentPart = 0x01,
                        ModulePart = 0x00,
                    },
                    ModuleType = ModuleTypeEnum.MotherBoard,
                    ReadMark = 0xFF,
                };
                var mbState = new MotherBoardState()//(mbpacket, null, this.server);
                {
                    BasePacket = mbpacket,
                    ReceivingServer = this.server,
                };
                mbState.ParentID = 0x01;
                mbState.Timer = 1000;
                mbState.Data.ModuleType[0] = 0x10; //sens, mb
                mbState.FlushDataState();

                var tctrlpacket = new DevicePacket()
                {
                    ID = new DeviceID()
                    {
                        ParentPart = 0x01,
                        ModulePart = 0x06,
                    },
                    ModuleType = ModuleTypeEnum.TrainController,
                    ReadMark = 0xFF,
                };
                var tctrlstate = new TrainControllerState();
                tctrlstate.BasePacket = tctrlpacket;
                tctrlstate.ReceivingServer = this.server;
                tctrlstate.Data.dutyEnabledBits = 10;
                tctrlstate.PreScale = 1;
                tctrlstate.Data.frequency = 48;
                tctrlstate.Data.duty = 0;
                tctrlstate.Data.period = 0;
                tctrlstate.FlushDataState();

                return tsenses.Select((state) => state.BasePacket)
                              .Concat(new [] { mbState.BasePacket, tctrlstate.BasePacket })
                              .ToList();
            }
        }
    }
}