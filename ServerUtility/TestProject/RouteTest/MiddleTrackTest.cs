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
    /// MiddleTrackTest の概要の説明
    /// </summary>
    [TestClass]
    public class MiddleTrackTest
    {
        public MiddleTrackTest()
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

        IEnumerable<BlockInfo> target_sheet
        {
            get
            {
                var yaml = new BlockYaml();
                var blocks = yaml.Parse("./SampleLayout/middletrack.yaml");

                return blocks;
            }
        }

        Route GetFirstRoute(BlockSheet sht)
        {
            return new Route(sht, new[] { "AT4", "AT3", "AT2", "CT2", "CT1" });
        }

        Route GetSecondRoute(BlockSheet sht)
        {
            return new Route(sht, new[] { "CT1", "CT2", "BT2", "BT3", "BT4" });
        }

        [TestMethod]
        public void ReadSheetTest()
        {
            var serv = new PacketServer(new AvrDeviceFactoryProvider());
            var sht = new BlockSheet(target_sheet, serv);

            var rfirst = GetFirstRoute(sht);
            var rsec = GetSecondRoute(sht);
        }

        [TestMethod]
        public void HaltTest()
        {
            var mockio = new Mock<IDeviceIO>();
            var written = new List<IDeviceState<IPacketDeviceData>>();
            var received = new List<IDevice<IDeviceState<IPacketDeviceData>>>();
            mockio.Setup(e => e.GetReadingPacket()).Returns(DevicePacket.CreatePackedPacket(received).ToObservable());
            mockio.Setup(e => e.GetWritingPacket(It.IsAny<DevicePacket>())).Callback<DevicePacket>(pack =>
                written.AddRange(pack.ExtractPackedPacket())
                )
                .Returns(Observable.Empty<Unit>());
            var serv = new PacketServer(new AvrDeviceFactoryProvider());
            serv.Controller = mockio.Object;
            var sht = new BlockSheet(target_sheet, serv);

            Route rt = GetRouteFirst(sht);

            var vh = new Vehicle(sht, rt);
            var halt = new Halt(sht.GetBlock("CT1"));
            vh.Halt = halt;

            written.Clear();
            vh.CurrentBlock = sht.GetBlock("AT4");
            vh.Refresh();
            serv.SendingObservable.Subscribe();
            Assert.IsTrue(Math.Round(written.ExtractDevice<MotorState>(1, 1, 1).Duty,1) > 0.25f);

            var sens = new UsartSensor() { DeviceID = new DeviceID(1, 3, 3) };
            sens.CurrentState.Threshold = 0.5f;
            sens.CurrentState.OnVoltage = 0.9f;
            received.Add(sens);
            serv.ReceivingObservable.Subscribe();

            written.Clear();
            vh.Refresh();
            serv.SendingObservable.Subscribe();
            Assert.IsTrue(Math.Round(written.ExtractDevice<MotorState>(1, 1, 1).Duty, 1) == 0.0f);

           

        }
        
    }
}
