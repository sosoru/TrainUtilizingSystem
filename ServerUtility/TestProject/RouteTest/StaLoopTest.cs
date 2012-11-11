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
using System.Threading;
using System.Reactive.Concurrency;

namespace TestProject
{
    /// <summary>
    /// StaLoopTest の概要の説明
    /// </summary>
    [TestClass]
    public class StaLoopTest
    {
        public StaLoopTest()
        {
            //
            // TODO: コンストラクター ロジックをここに追加します
            //
        }

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

        IEnumerable<BlockInfo> target_sheet
        {
            get
            {
                var yaml = new BlockYaml();
                var blocks = yaml.Parse("./SampleLayout/staloop.yaml");

                return blocks;
            }
        }

        Route GetRouteFirst(BlockSheet sht)
        {
            var route = new Route(sht, new[] {
                "AT2", "AT3", "AT4", "AT5", "AT6", "BAT6",
                "AT7", "AT8","AT9", "BAT9",
                "AT10", "AT11", "AT12", "BAT12",
                "AT13", "AT14", "AT15", "BAT16",
                "AT16", "AT1", "BAT1" });
            return route;
        }

        #region 追加のテスト属性
        //
        // テストを作成する際には、次の追加属性を使用できます:
        //
        // クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 各テストを実行する前に、TestInitialize を使用してコードを実行してください
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ReadSheetTest()
        {
            var sht = target_sheet;

            target_sheet.ToArray();
        }

        [TestMethod]
        public void RouteConstructTest()
        {
            var serv = new PacketServer();
            var sht = new BlockSheet(target_sheet, serv);

            Route rt = GetRouteFirst(sht);
            var units = rt.Units.ToArray();
            Assert.IsTrue(units[0].Blocks.Select(b => b.Name)
                            .SequenceEqual(new[] { "AT2", "AT3", "AT4", "AT5", "AT6", "BAT6" }));
            Assert.IsTrue(units[1].Blocks.Select(b => b.Name)
                            .SequenceEqual(new[] { "AT7", "AT8", "AT9", "BAT9" }));
            Assert.IsTrue(units[2].Blocks.Select(b => b.Name)
                            .SequenceEqual(new[] { "AT10", "AT11", "AT12", "BAT12" }));
            Assert.IsTrue(units[3].Blocks.Select(b => b.Name)
                            .SequenceEqual(new[] { "AT13", "AT14", "AT15", "BAT16" }));
            Assert.IsTrue(units[4].Blocks.Select(b => b.Name)
                            .SequenceEqual(new[] { "AT16", "AT1", "BAT1" }));


        }


        [TestMethod]
        public void RouteCommandTest()
        {
            var mockio = new Mock<IDeviceIO>();
            var written = new List<IDeviceState<IPacketDeviceData>>();
            mockio.Setup(e => e.GetReadingPacket()).Returns(Observable.Empty<DevicePacket>());
            mockio.Setup(e => e.GetWritingPacket(It.IsAny<DevicePacket>())).Callback<DevicePacket>(pack =>
                written.AddRange(pack.ExtractPackedPacket())
                )
                .Returns(Observable.Empty<Unit>());
            var serv = new PacketServer(new AvrDeviceFactoryProvider());
            serv.Controller = mockio.Object;
            var sht = new BlockSheet(target_sheet, serv);

            Route rt = GetRouteFirst(sht);

            var vh = new Vehicle(sht, rt);

            // vh will allocate the first control block of the route at Constructor
            vh.Run();

            serv.SendingObservable.Subscribe();

            //Assert.IsTrue(written.Count == 5);
            Assert.IsTrue(written.ExtractDevice<SwitchState>(1, 1, 1).Position == PointStateEnum.Straight);
            Assert.IsTrue(written.ExtractDevice<SwitchState>(1, 1, 2).Position == PointStateEnum.Straight);
            Assert.IsTrue(written.ExtractDevice<MotorState>(1, 2, 2).Duty > 0.0f);
        }

        [TestMethod]
        public void VehicleReduceSpeedTest()
        {
            var mockio = new Mock<IDeviceIO>();
            var written = new List<IDeviceState<IPacketDeviceData>>();
            mockio.Setup(e => e.GetReadingPacket()).Returns(Observable.Empty<DevicePacket>());
            mockio.Setup(e => e.GetWritingPacket(It.IsAny<DevicePacket>())).Callback<DevicePacket>(pack =>
                written.AddRange(pack.ExtractPackedPacket())
                )
                .Returns(Observable.Empty<Unit>());
            var serv = new PacketServer(new AvrDeviceFactoryProvider());
            serv.Controller = mockio.Object;
            var sht = new BlockSheet(target_sheet, serv);

            Route rt = GetRouteFirst(sht);
            var vh = new Vehicle(sht, rt);

            // N-th case : the vehicle goes at specified speed
            vh.Run(1.0f);
            Assert.IsTrue(written.ExtractDevice<MotorState>(1, 2, 2).Duty == 1.0f);

            // 2nd case : the vehicle keeps specified speed, but entering the next section, reduces its speed to half
            Route otherrt = GetRouteFirst(sht);
            rt.AllocateTrain(sht.GetBlock("AT15"));
            var othervh = new Vehicle(sht, rt);

            othervh.Run(1.0f);
            vh.Run(1.0f);

            Assert.IsTrue(written.ExtractDevice<MotorState>(1, 2, 2).Duty == 1.0f);
            Assert.IsTrue(Math.Round(written.ExtractDevice<MotorState>(1, 2, 3).Duty,1) == 0.5f);
          
            // 1st case : the vehicle reduces its speed to half immediately, and stops the next section
            otherrt.AllocateTrain(sht.GetBlock("AT12"));
            othervh.Run(1.0f);
            vh.Run(1.0f);

            Assert.IsTrue(Math.Round(written.ExtractDevice<MotorState>(1, 2, 2).Duty,1) == 0.5f);
            Assert.IsTrue(written.ExtractDevice<MotorState>(1, 2, 3).Duty == 0.0f);

            // zero case : the vehicle stops immediately
            otherrt.AllocateTrain(sht.GetBlock("AT9"));
            othervh.Run(1.0f);
            vh.Run(1.0f);

            Assert.IsTrue(written.ExtractDevice<MotorState>(1, 2, 2).Duty == 0.0f);

            
        }

    }
}
