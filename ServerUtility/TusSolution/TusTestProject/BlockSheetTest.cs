using Tus.TransControl.Parser;
using Tus.TransControl;
using Tus.TransControl.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Moq;
using Moq.Linq;
using System.Reactive.Subjects;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using Microsoft.Reactive.Testing;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace TestProject
{
    public static class ExtractExtension
    {
        public static TCast ExtractDevice<TCast>(this IEnumerable<IDevice<IDeviceState<IPacketDeviceData>>> list, ushort parent, byte module, byte inter)
        {
            var id = new DeviceID(parent, module, inter);
            return (TCast)list.First(p => p.DeviceID == id);
        }

        public static TCast ExtractDevice<TCast>(this IDictionary<DeviceID, IDevice<IDeviceState<IPacketDeviceData>>> list, ushort parent, byte module, byte inter)
        {
            var id = new DeviceID(parent, module, inter);
            return (TCast)list[id];
        }

        public static TCast ExtractDevice<TCast>(this IEnumerable<IDeviceState<IPacketDeviceData>> list, ushort parent, byte module, byte inter)
        {
            return ExtractDevices<TCast>(list, parent, module, inter).First();
        }

        public static IEnumerable<TCast> ExtractDevices<TCast>(this IEnumerable<IDeviceState<IPacketDeviceData>> list, ushort parent, byte module, byte inter)
        {
            var id = new DeviceID(parent, module, inter);
            return list.Where(s => s is TCast).Where(p => p.ID == id).Cast<TCast>();

        }

    }


    /// <summary>
    ///BlockSheetTest のテスト クラスです。すべての
    ///BlockSheetTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    [DeploymentItem("SampleLayout/loop.yaml")]
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
                var blocks = yaml.Parse("loop.yaml");

                return blocks;
            }
        }

        BlockSheet sample_sheet
        {
            get
            {
                var infos = new[] { new BlockInfo { Name = "pero" }, new BlockInfo { Name = "hoge" } };
                var sheet = new BlockSheet(infos, new PacketServer());

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
                                SourceID = new DeviceID(100, 1),
                            };
                var serv = new PacketServer() { Controller = io };
                var disp = new PacketDispatcher();

                serv.LoopStart(System.Reactive.Concurrency.Scheduler.NewThread);

                return serv;
            }
        }

        [TestMethod()]
        public void ReadSampleDataTest()
        {
            var blocks = sample_loop_sheet.ToArray();

        }

        //[TestMethod()]
        //public void BlockEffect_BasicTest()
        //{
        //    var written = new List<IDevice<IDeviceState<IPacketDeviceData>>>();
        //    var serv = new Mock<PacketServer>();

        //    serv.Setup(e => e.EnqueueState(It.IsAny<IDevice<IDeviceState<IPacketDeviceData>>>()))
        //        .Callback<IDevice<IDeviceState<IPacketDeviceData>>>(d => written.Add(d));

        //    var sht = new BlockSheet(sample_loop_sheet, serv.Object);

        //    var route = new AssociatedRoute(sht, new[] { "AT1", "AT2", "AT3", "AT4", });
        //    var cmd = new CommandInfo()
        //    {
        //        AssociatedRoute = route,
        //        Speed = 0.5f,
        //        MotorMode = MotorMemoryStateEnum.Controlling,
        //    };
        //    var factory = new CommandFactory() { CreateCommand = b => cmd, };

        //    // 1: after calling Effect, AT1 is positive and others are standby
        //    // then check the packet sending for AT1 is included 
        //    route.LockNextUnit();
        //    sht.Effect(factory, route.LockedBlocks);
        //    var pack = written.First(p => p.DeviceID == new DeviceID(1, 1, 1));
        //    var state = (MotorState)pack.CurrentState;

        //    Assert.IsTrue(Math.Round(state.Duty, 1) == cmd.Speed);

        //    var standbys = written.Where(p => p.DeviceID != new DeviceID(1, 1, 1));
        //    standbys.ToList().ForEach(s => Assert.IsTrue(((MotorState)s.CurrentState).Direction == MotorDirection.Standby));


        //    //var existblock = sht.GetBlock("AT2");
        //    //var detectormock = new Mock<SensorDetector>();
        //    //detectormock.Setup(d => d.IsDetected).Returns(true);
        //    //existblock.Detector = detectormock.Object

        //    //   ;

        //}

        //[TestMethod]
        //public void VehiclesTest()
        //{
        //    BlockSheet target = sample_sheet;

        //    var serv = sample_server;
        //    var log = new List<IDeviceState<IPacketDeviceData>>();

        //    serv.GetDispatcher()
        //        .Subscribe(state => log.Add(state));

        //    var sht = new BlockSheet(sample_loop_sheet, serv);
        //    var route = new AssociatedRoute(sht, new[] { "AT1", "AT2", "AT3", "AT4" });
        //    var cmd = new CommandInfo()
        //    {
        //        AssociatedRoute = route,
        //        Speed = 0.5f
        //    };

        //    IList<Vehicle> vehicles;
        //    Vehicle first, second;

        //    var mocktrue = new Mock<SensorDetector>();
        //    var mockfalse = new Mock<SensorDetector>();
        //    mocktrue.Setup(e => e.IsDetected).Returns(true);
        //    mockfalse.Setup(e => e.IsDetected).Returns(false);

        //    // 1: AT1 is detected -> one vehicles created
        //    sht.GetBlock("AT1").Detector = mocktrue.Object;
        //    sht.GetBlock("AT2").Detector = mockfalse.Object;
        //    vehicles = new List<Vehicle>();
        //    vehicles.Add(new Vehicle(sht, route));

        //    //Assert.IsTrue(vehicles.Count() == 1);
        //    //Assert.IsTrue(vehicles.First().CurrentBlock == sht.GetBlock("AT1"));
        //    first = vehicles.First();

        //    // 2: AT1 leaves and AT2 is detected
        //    sht.GetBlock("AT1").Detector = mockfalse.Object;
        //    sht.GetBlock("AT2").Detector = mocktrue.Object;
        //    vehicles = sht.VehiclesStatus.ToArray();

        //    Assert.IsTrue(vehicles.Count() == 1);
        //    //Assert.IsTrue(vehicles.First().CurrentBlock == sht.GetBlock("AT2"));
        //    second = vehicles.First();

        //    // these vehicle is equal to each other
        //    Assert.Equals(first, second);

        //}

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
