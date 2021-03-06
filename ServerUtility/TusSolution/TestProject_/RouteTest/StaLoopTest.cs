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
using Microsoft.Reactive.Testing;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace TestProject
{
    /// <summary>
    /// StaLoopTest の概要の説明
    /// </summary>
    [TestClass]
    [DeploymentItem("SampleLayout/staloop.yaml")]
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
                var blocks = yaml.Parse("staloop.yaml");

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
            route.IsRepeatable = true;
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

        private List<IDeviceState<IPacketDeviceData>> written;
        private PacketServer serv;
        private BlockSheet sht;
        private TestScheduler scheduler;

        [TestInitialize]
        public void TestInitialize()
        {
            var mockio = new Mock<IDeviceIO>();
            written = new List<IDeviceState<IPacketDeviceData>>();
            mockio.Setup(e => e.GetReadingPacket()).Returns(Observable.Empty<DevicePacket>());
            mockio.Setup(e => e.GetWritingPacket(It.IsAny<DevicePacket>())).Callback<DevicePacket>(pack =>
                written.AddRange(pack.ExtractPackedPacket())
                )
                .Returns(Observable.Empty<DevicePacket>());
            serv = new PacketServer();
            serv.Controller = mockio.Object;
            sht = new BlockSheet(target_sheet, serv);

            this.scheduler = new TestScheduler();
            sht.AssociatedScheduler = scheduler;
        }

        [TestMethod]
        public void ReadSheetTest()
        {
            var sht = target_sheet;

            target_sheet.ToArray();
        }

        [TestMethod]
        public void RouteConstructTest()
        {
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
            sht.TimeWaitingSwitchChanged = TimeSpan.FromSeconds(5.0);
            Route rt = GetRouteFirst(sht);

            var vh = new Vehicle(sht, rt);

            // vh will allocate the first control block of the route at Constructor

            vh.Run();
            var waitingTicks1 = sht.TimeWaitingSwitchChanged + TimeSpan.FromSeconds(0.5);

            scheduler.Schedule(TimeSpan.FromSeconds(0.5), () =>
            {
                serv.SendingObservable.Repeat(50).Subscribe();
                Assert.IsTrue(written.ExtractDevice<SwitchState>(1, 1, 1).Position == PointStateEnum.Straight);
                Assert.IsTrue(written.ExtractDevice<SwitchState>(1, 1, 2).Position == PointStateEnum.Straight);

            });

            scheduler.Schedule(waitingTicks1, () =>
            {
                serv.SendingObservable.Repeat(50).Subscribe();
                Assert.IsTrue(written.ExtractDevices<MotorState>(1, 2, 2).Any(s => s.Duty > 0.0f));
            });

            scheduler.Start();
        }

        [TestMethod]
        public void VehicleReduceSpeedTest()
        {
            Route rt = GetRouteFirst(sht);
            var vh = new Vehicle(sht, rt);

            // N-th case : the vehicle goes at specified speed
            vh.Run(1.0f, sht.GetBlock("AT2"));
            scheduler.Start();
            serv.SendingObservable.Repeat(50).Subscribe();
            Assert.IsTrue(written.ExtractDevices<MotorState>(1, 2, 2).Any(s => s.Duty == 1.0f));


            // 2nd case : the vehicle keeps specified speed, but entering the next section, reduces its speed to half
            Route otherrt = GetRouteFirst(sht);
            var othervh = new Vehicle(sht, otherrt);

            written.Clear();
            othervh.Run(1.0f, sht.GetBlock("AT15"));
            vh.Run(1.0f, sht.GetBlock("AT2"));

            scheduler.Start();
            serv.SendingObservable.Repeat(50).Subscribe();
            Assert.IsTrue(written.ExtractDevices<MotorState>(1, 2, 2).Any(s => s.Duty == 1.0f));
            Assert.IsTrue(written.ExtractDevices<MotorState>(1, 2, 3).Any(s => Math.Round(s.Duty, 1) == 0.5f));

            // 1st case : the vehicle reduces its speed to half immediately, and stops the next section
            written.Clear();
            othervh.Run(1.0f, sht.GetBlock("AT12"));
            vh.Run(1.0f, sht.GetBlock("AT2"));

            scheduler.Start();
            serv.SendingObservable.Repeat(50).Subscribe();
            Assert.IsTrue(written.ExtractDevices<MotorState>(1, 2, 2).Any(s => Math.Round(s.Duty, 1) == 0.5f));
            Assert.IsTrue(written.ExtractDevices<MotorState>(1, 2, 3).Any(s => s.Duty == 0.0f));

            // zero case : the vehicle stops immediately
            written.Clear();
            try
            {
                othervh.Run(1.0f, sht.GetBlock("AT9")); // AT9 is already blocked by the other vehicle
                Assert.Fail();
            }
            catch (InvalidOperationException ex) { }
            vh.Route.InitLockingPosition();
            othervh.Run(1.0f, sht.GetBlock("AT9"));
            vh.Run(1.0f, sht.GetBlock("AT2"));

            scheduler.Start();
            serv.SendingObservable.Repeat(50).Subscribe();
            Assert.IsTrue(written.ExtractDevices<MotorState>(1, 2, 2).Any(s => s.Duty == 0.0f));


        }

        [TestMethod]
        public void HaltTest()
        {
            Assert.Inconclusive("halting feature is not implemented");

            Route rt = GetRouteFirst(sht);
            var vh = new Vehicle(sht, rt);

            vh.Halt.Add(new Halt(sht.GetBlock("AT4")));

            vh.CurrentBlock = sht.GetBlock("AT2");
            vh.Refresh();
            serv.SendingObservable.Repeat(50).Subscribe();
            Assert.IsTrue(written.ExtractDevices<MotorState>(1, 2, 2).Any(d => d.Duty == 1.0f));
            Assert.IsTrue(written.ExtractDevices<MotorState>(1, 2, 3).Any(d =>Math.Round(d.Duty, 1) == 0.5f));

        }

    }
}
