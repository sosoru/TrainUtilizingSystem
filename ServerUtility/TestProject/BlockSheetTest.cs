using RouteLibrary.Base;
using RouteLibrary.Parser;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Data;
using SensorLibrary.Packet.IO;
using SensorLibrary.Packet.Control;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Moq;
using Moq.Linq;
using SensorLibrary;
using System.Reactive.Subjects;

namespace TestProject
{
    public static class ExtractExtension
    {
        public static TCast ExtractDevice<TCast>(this IEnumerable<IDevice<IDeviceState<IPacketDeviceData>>> list, ushort parent, byte module , byte inter)
        {
            var id = new DeviceID(parent, module, inter);
            return (TCast)list.First(p => p.DeviceID == id);
        }

        public static TCast ExtractDevice<TCast>(this IDictionary<DeviceID, IDevice<IDeviceState<IPacketDeviceData>>> list, ushort parent, byte module, byte inter)
        {
            var id = new DeviceID(parent, module, inter);
            return (TCast) list[id];
        }

    }


    /// <summary>
    ///BlockSheetTest のテスト クラスです。すべての
    ///BlockSheetTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class BlockSheetTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 追加のテスト属性
        // 
        //テストを作成するときに、次の追加属性を使用することができます:
        //
        //クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //各テストを実行する前にコードを実行するには、TestInitialize を使用
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //各テストを実行した後にコードを実行するには、TestCleanup を使用
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        IEnumerable<BlockInfo> sample_loop_sheet
        {
            get
            {
                var yaml = new BlockYaml();
                var blocks = yaml.Parse("./SampleLayout/loop.yaml");

                return blocks;
            }
        }

        BlockSheet sample_sheet
        {
            get
            {
                var infos = new[] { new BlockInfo { Name = "pero" }, new BlockInfo { Name = "hoge" } };
                var sheet = new BlockSheet(infos, new PacketServer(new AvrDeviceFactoryProvider()));

                sheet.Name = "pero";
                return sheet;
            }
        }

        PacketServer sample_server
        {
            get
            {
                var io = new TusEthernetIO(new IPAddress(new byte[] { 192, 168, 2, 24 }),
                            new IPAddress(new byte[] { 255, 255, 255, 0 }))
                            {
                                SourceID = new SensorLibrary.DeviceID(100, 1),
                            };
                var serv = new PacketServer(new AvrDeviceFactoryProvider()) { Controller = io };
                var disp = new PacketDispatcher();

                serv.LoopStart();

                return serv;
            }
        }

        [TestMethod()]
        public void ReadSampleDataTest()
        {
            var blocks = sample_loop_sheet.ToArray();

        }

        [TestMethod()]
        public void BlockEffect_BasicTest()
        {
            var written = new List<IDevice<IDeviceState<IPacketDeviceData>>>();
            var serv = new Mock<PacketServer>();
      
            serv.Setup(e => e.SendState(It.IsAny<IDevice<IDeviceState<IPacketDeviceData>>>()))
                .Callback<IDevice<IDeviceState<IPacketDeviceData>>>(d => written.Add(d));

            var sht = new BlockSheet(sample_loop_sheet, serv.Object);
            var route = new Route(sht, new[] { "AT1", "AT2", "AT3", "AT4", "AT5" });
            var cmd = new CommandInfo()
            {
                Route = route,
                Speed = 0.5f
            };
            
            // 1: after calling Effect, AT1 is positive and others are standby
            // then check the packet sending for AT1 is included 
            sht.Effect(cmd);

            var pack = written.First(p => p.DeviceID == new DeviceID(1, 1, 1));
            var state = (MotorState)pack.CurrentState;

            Assert.IsTrue(Math.Round( state.Duty,1) == cmd.Speed);

            var standbys = written.Where(p => p.DeviceID != new DeviceID(1, 1, 1));
            standbys.ToList().ForEach(s => Assert.IsTrue(((MotorState)s.CurrentState).Direction == MotorDirection.Standby));

            
            //var existblock = sht.GetBlock("AT2");
            //var detectormock = new Mock<SensorDetector>();
            //detectormock.Setup(d => d.IsDetected).Returns(true);
            //existblock.Detector = detectormock.Object
              
            //   ;

        }

        [TestMethod()]
        public void BlockEffectTest()
        {
            var written = new Dictionary<DeviceID, IDevice<IDeviceState<IPacketDeviceData>>>();
            var serv = new Mock<PacketServer>();

            serv.Setup(e => e.SendPacket(It.IsAny<DevicePacket>()))
                .Callback<DevicePacket>(p =>
                {
                    //p.ExtractDeviceState().select

                });

            var sht = new BlockSheet(sample_loop_sheet, serv.Object);
            var route = new Route(sht, new[] { "AT1", "AT2", "AT3", "AT4", "AT5" });
            var cmd = new CommandInfo()
            {
                Route = route,
                Speed = 0.5f
            };

            sht.Effect(cmd);
            Assert.IsTrue(sht.GetBlock("AT1").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.Controlling);
            Assert.IsTrue(sht.GetBlock("AT2").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.Waiting);
            Assert.IsTrue(sht.GetBlock("AT3").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.NoEffect);
            Assert.IsTrue(sht.GetBlock("AT4").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.NoEffect);
            Assert.IsTrue(sht.GetBlock("AT5").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.NoEffect);

            route.LockNextUnit();
            sht.Effect(cmd);

            Assert.IsTrue(sht.GetBlock("AT1").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.Locked);
            Assert.IsTrue(sht.GetBlock("AT2").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.Controlling);
            Assert.IsTrue(sht.GetBlock("AT3").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.Waiting);
            Assert.IsTrue(sht.GetBlock("AT4").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.NoEffect);
            Assert.IsTrue(sht.GetBlock("AT5").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.NoEffect);

            route.ReleaseBeforeUnit();
            sht.Effect(cmd);

            Assert.IsTrue(sht.GetBlock("AT1").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.NoEffect);
            Assert.IsTrue(sht.GetBlock("AT2").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.Controlling);
            Assert.IsTrue(sht.GetBlock("AT3").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.Waiting);
            Assert.IsTrue(sht.GetBlock("AT4").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.NoEffect);
            Assert.IsTrue(sht.GetBlock("AT5").MotorEffector.Device.CurrentMemory == MotorMemoryStateEnum.NoEffect);


            //Assert.IsTrue(Math.Round(written.ExtractDevice<Motor>(1, 1, 1).CurrentState.Duty, 1) == 0.5f);
            //Assert.IsTrue(Math.Round(written.ExtractDevice<Motor>(1, 1, 2).CurrentState.Duty, 1) == 0.5f);
            //Assert.IsTrue(Math.Round(written.ExtractDevice<Motor>(1, 1, 3).CurrentState.Duty, 1) == 0.0f);
            //Assert.IsTrue(Math.Round(written.ExtractDevice<Motor>(1, 1, 4).CurrentState.Duty, 1) == 0.0f);
            //Assert.IsTrue(Math.Round(written.ExtractDevice<Motor>(1, 1, 5).CurrentState.Duty, 1) == 0.0f);

            //written.Clear();
            //var existblock = sht.GetBlock("AT4");
            //var detectormock = new Mock<SensorDetector>();
            //detectormock.Setup(d => d.IsDetected).Returns(true);
            //existblock.Detector = detectormock.Object;

            //sht.Effect(cmd);

            //Assert.IsTrue(Math.Round(written.ExtractDevice<Motor>(1, 1, 1).CurrentState.Duty, 1) == 0.3f);
            //Assert.IsTrue(Math.Round(written.ExtractDevice<Motor>(1, 1, 2).CurrentState.Duty, 1) == 0.0f);
            //Assert.IsTrue(Math.Round(written.ExtractDevice<Motor>(1, 1, 3).CurrentState.Duty, 1) == 0.5f);
            //Assert.IsTrue(Math.Round(written.ExtractDevice<Motor>(1, 1, 4).CurrentState.Duty, 1) == 0.5f);
            //Assert.IsTrue(Math.Round(written.ExtractDevice<Motor>(1, 1, 5).CurrentState.Duty, 1) == 0.0f);

        }

        [TestMethod()]
        public void SampleBlockTest()
        {
            var serv = sample_server;
            var target = new BlockSheet(sample_loop_sheet, serv);
            var log = new List<IDeviceState<IPacketDeviceData>>();

            serv.GetDispatcher()
                .Subscribe(state => log.Add(state));

            var rt = new Route(target, new[] { "AT1", "AT2", "AT3", "AT4" });
            var cmd = new CommandInfo()
            {
                Route = rt,
                Speed = 0.5f
            };
            
            
        }

        [TestMethod()]
        public void PrepareVehiclesTest()
        {
            BlockSheet target = sample_sheet;

            var serv = sample_server;
            var log = new List<IDeviceState<IPacketDeviceData>>();

            serv.GetDispatcher()
                .Subscribe(state => log.Add(state));

            var sht = new BlockSheet(sample_loop_sheet, serv);
            var route = new Route(sht, new[] { "AT1", "AT2", "AT3", "AT4" });
            var cmd = new CommandInfo()
            {
                Route = route,
                Speed = 0.5f
            };

            //set dummy train
            //var train = new Mock<SensorDetector>();
            //train.Setup(e => e.IsDetected).Returns(() => true);

            //sht.GetBlock("AT3").Detector = train.Object;

            if(!serv.IsLooping)
                serv.LoopStart();

            sht.ChangeDetectingMode(); // todo: value check
            System.Threading.Thread.Sleep(2000);
            sht.InquiryAllMotors();
            System.Threading.Thread.Sleep(2000);

            sht.PrepareVehicles();

        }

        [TestMethod]
        public void VehiclesTest()
        {
            BlockSheet target = sample_sheet;

            var serv = sample_server;
            var log = new List<IDeviceState<IPacketDeviceData>>();

            serv.GetDispatcher()
                .Subscribe(state => log.Add(state));

            var sht = new BlockSheet(sample_loop_sheet, serv);
            var route = new Route(sht, new[] { "AT1", "AT2", "AT3", "AT4" });
            var cmd = new CommandInfo()
            {
                Route = route,
                Speed = 0.5f
            };

            IEnumerable<Vehicle> vehicles;
            Vehicle first, second;

            var mocktrue = new Mock<SensorDetector>();
            var mockfalse = new Mock<SensorDetector>();
            mocktrue.Setup(e => e.IsDetected).Returns(true);
            mockfalse.Setup(e => e.IsDetected).Returns(false);

            // 1: AT1 is detected -> one vehicles created
            sht.GetBlock("AT1").Detector = mocktrue.Object;
            sht.GetBlock("AT2").Detector = mockfalse.Object;
            vehicles = sht.Vehicles.ToArray();

            Assert.IsTrue(vehicles.Count() == 1);
            //Assert.IsTrue(vehicles.First().CurrentBlock == sht.GetBlock("AT1"));
            first = vehicles.First();

            // 2: AT1 leaves and AT2 is detected
            sht.GetBlock("AT1").Detector = mockfalse.Object;
            sht.GetBlock("AT2").Detector = mocktrue.Object;
            vehicles = sht.Vehicles.ToArray();

            Assert.IsTrue(vehicles.Count() == 1);
            //Assert.IsTrue(vehicles.First().CurrentBlock == sht.GetBlock("AT2"));
            second = vehicles.First();

            // these vehicle is equal to each other
            Assert.Equals(first, second);

        }

        /// <summary>
        ///Equals のテスト
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            BlockSheet target = sample_sheet;
            BlockSheet other = sample_sheet;
            target.Name = "test1";
            other.Name = "test2";

            var f = new Func<BlockSheet, bool>((o) => target.Equals(o));

            EqualTest.TestNotEqualNotDefault(other, f);

            other.Name = target.Name;
            EqualTest.TestEqualNotDefault(other, f);
        }

        /// <summary>
        ///Equals のテスト
        ///</summary>
        [TestMethod()]
        public void EqualsTest1()
        {
            BlockSheet target = sample_sheet;
            BlockSheet other = sample_sheet;
            target.Name = "test1";
            other.Name = "test2";

            var f = new Func<BlockSheet, bool>((o) => ((object)target).Equals((object)o));

            EqualTest.TestNotEqualNotDefault(other, f);

            other.Name = target.Name;
            EqualTest.TestEqualNotDefault(other, f);
        }

        /// <summary>
        ///GetHashCode のテスト
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest()
        {
            BlockSheet target = sample_sheet;
            BlockSheet other = sample_sheet;
            int expected = target.Name.GetHashCode();
            int actual;
            actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///op_Inequality のテスト
        ///</summary>
        [TestMethod()]
        public void op_InequalityTest()
        {
            BlockSheet A = sample_sheet;
            BlockSheet B = sample_sheet;
            var f = new Func<BlockSheet, BlockSheet, bool>((a, b) => !(a != b));

            EqualTest.TestEqualNotDefault(A, B, f);
        }

        /// <summary>
        ///op_Equality のテスト
        ///</summary>
        [TestMethod()]
        public void op_EqualityTest()
        {
            BlockSheet A = sample_sheet;
            BlockSheet B = sample_sheet;
            var f = new Func<BlockSheet, BlockSheet, bool>((a, b) => (a == b));

            EqualTest.TestEqualNotDefault(A, B, f);
        }
    }
}
