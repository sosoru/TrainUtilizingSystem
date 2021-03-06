﻿using Tus.TransControl.Parser;
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
using System.Threading;
using System.Reactive.Concurrency;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace TestProject
{
    /// <summary>
    /// MiddleTrackTest の概要の説明
    /// </summary>
    [TestClass]
    [DeploymentItem("SampleLayout/middletrack.yaml")]
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
                var blocks = yaml.Parse("middletrack.yaml");

                return blocks;
            }
        }

        RouteOrder GetFirstRoute(BlockSheet sht)
        {
            return new RouteOrder(sht, new[] { "AT4", "AT3", "AT2", "CT2", "CT1" });
        }

        RouteOrder GetSecondRoute(BlockSheet sht)
        {
            return new RouteOrder(sht, new[] { "CT1", "CT2", "BT2", "BT3", "BT4" });
        }

        RouteOrder GetConcatedRoute(BlockSheet sht)
        {
            var first = GetFirstRoute(sht);
            var second = GetSecondRoute(sht);
            return new RouteOrder(sht, first.Blocks.Select(b => b.Name).Concat(second.Blocks.Select(b => b.Name)));
        }

        [TestMethod]
        public void ReadSheetTest()
        {
            var serv = new PacketServer();
            var sht = new BlockSheet(target_sheet, serv);

            var rfirst = GetFirstRoute(sht);
            var rsec = GetSecondRoute(sht);
        }

        /// <summary>
        /// Vehicleは，HaltableなBlockに進入したときに，減速しながら停止すること．
        /// </summary>
        [TestMethod]
        public void HaltTest()
        {
            Assert.Inconclusive(); // HaltHere等のHaltの他に，センサーを用いて閉塞を開放する機能があったが，現在使用していない

            var mockio = new Mock<IDeviceIO>();
            var written = new List<IDeviceState<IPacketDeviceData>>();
            var received = new List<IDevice<IDeviceState<IPacketDeviceData>>>();
            mockio.Setup(e => e.GetReadingPacket()).Returns(PacketExtension.CreatePackedPacket(received).ToObservable());
            mockio.Setup(e => e.GetWritingPacket(It.IsAny<DevicePacket>())).Callback<DevicePacket>(pack =>
                written.AddRange(pack.ExtractPackedPacket())
                )
                .Returns(Observable.Empty<DevicePacket>());
            var serv = new PacketServer();
            serv.Controller = mockio.Object;
            var sht = new BlockSheet(target_sheet, serv);

            RouteOrder rt = this.GetConcatedRoute(sht);

            var vh = new Vehicle(sht, rt);
            var halt = new Halt(sht.GetBlock("CT1"));
            vh.Speed = 1.0f;
            vh.Halt.Add(halt);

            written.Clear();
            vh.Run(1.0f, sht.GetBlock("AT4"));
            vh.Refresh();
            serv.SendAll();
            Assert.IsTrue(written.ExtractDevices<MotorState>(1,1,1).Any(s => Math.Round(s.Duty, 1) == 0.5f));

            var sens = new UsartSensor() { DeviceID = new DeviceID(1, 3, 3) };
            sens.CurrentState.Data.VoltageOn = 200;
            sens.CurrentState.Data.Threshold = 100;
            received.Add(sens);
            serv.DispatchState(sens.CurrentState);

            written.Clear();
            vh.Refresh();
            serv.SendAll();
            Assert.IsTrue(written.ExtractDevices<MotorState>(1, 1, 1).Any(s => Math.Round(s.Duty, 1) == 0.0f));

           

        }
        
    }
}
