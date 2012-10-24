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

namespace TestProject
{


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
                var sheet = new BlockSheet(infos, null);

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
        public void BlockEffectTest()
        {
            var io = new Mock<IDeviceIO>();
            var written = new List<DevicePacket>();
            io.Setup(e => e.GetWritingPacket(It.IsAny<DevicePacket>()))
                .Returns<DevicePacket>(pack => Observable.Start(() => written.Add(pack)));

            //1 : check reduce speed
            io.Setup(e => e.GetReadingPacket())
                .Returns(() =>
                    {
                        var sens = new Sensor() { DeviceID = new DeviceID(1, 2, 9) };
                        var packets = DevicePacket.CreatePackedPacket(sens);

                        return packets.ToObservable();
                    });

            var serv = new PacketServer(new AvrDeviceFactoryProvider()) { Controller = io.Object };
            serv.LoopStart();

            var sht = new BlockSheet(sample_loop_sheet, serv);
            var route = new Route(sht, new[] { "AT1", "AT2", "AT3", "AT4", "AT5" });
            

            var cmd = new CommandInfo()
            {
                Route = route,
                Speed = 0.5f
            };

            sht.Effect(cmd);
            

        }

        [TestMethod()]
        public void MotorTestFromSheet()
        {
            var serv = sample_server;
            var infos = new BlockYaml().Parse(BlockYamlTest.PathSample);
            var sheet = new BlockSheet(infos, serv);

            var r = new Route(sheet, new[] { "AT1", "PT2", "BT1", "PT3", "CT1", "PT4", "DT1", "PT1" });
            var cmd = new CommandInfo()
            {
                Route = r,
                Speed = 0.5f,
            };

            sheet.Effect(cmd);

            var cmd1 = new CommandInfo()
                          {
                              Route = r,
                              Speed = 0.3f,
                          };
            sheet.Effect(cmd1);
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
