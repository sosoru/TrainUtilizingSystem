using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Microsoft.Reactive.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tus.Communication;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace TestProject
{
    /// <summary>
    ///     StaLoopTest の概要の説明
    /// </summary>
    [TestClass]
    [DeploymentItem("SampleLayout/staloop.yaml")]
    public class StaLoopTest
    {
        private TestScheduler _scheduler;
        private PacketServer _serv;
        private BlockSheet _sht;
        private List<IDeviceState<IPacketDeviceData>> _written;

        /// <summary>
        ///     現在のテストの実行についての情報および機能を
        ///     提供するテスト コンテキストを取得または設定します。
        /// </summary>
        public TestContext TestContext { get; set; }

        private IEnumerable<BlockInfo> target_sheet
        {
            get
            {
                var yaml = new BlockYaml();
                var blocks = yaml.Parse("staloop.yaml");

                return blocks;
            }
        }

        private RouteOrder GetRouteFirst(BlockSheet sht)
        {
            var route = new RouteOrder(sht, new[]
                                                {
                                                    "AT2", "AT3", "AT4", "AT5", "AT6", "BAT6",
                                                    "AT7", "AT8", "AT9", "BAT9",
                                                    "AT10", "AT11", "AT12", "BAT12",
                                                    "AT13", "AT14", "AT15", "BAT16",
                                                    "AT16", "AT1", "BAT1"
                                                }) {IsRepeatable = true};
            return route;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var mockio = new Mock<IDeviceIO>();
            _written = new List<IDeviceState<IPacketDeviceData>>();
            mockio.Setup(e => e.GetReadingPacket()).Returns(Observable.Empty<DevicePacket>());
            mockio.Setup(e => e.GetWritingPacket(It.IsAny<DevicePacket>())).Callback<DevicePacket>(pack =>
                                                                                                   _written.AddRange(
                                                                                                       pack
                                                                                                           .ExtractPackedPacket
                                                                                                           ())
                )
                  .Returns(Observable.Empty<DevicePacket>());
            _serv = new PacketServer {Controller = mockio.Object};
            _sht = new BlockSheet(target_sheet, _serv);

            _scheduler = new TestScheduler();
            _sht.AssociatedScheduler = _scheduler;
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
            var rt = GetRouteFirst(_sht);
            var units = rt.Units.ToArray();
            Assert.IsTrue(units[0].Blocks.Select(b => b.Name)
                                  .SequenceEqual(new[] {"AT2", "AT3", "AT4", "AT5", "AT6", "BAT6"}));
            Assert.IsTrue(units[1].Blocks.Select(b => b.Name)
                                  .SequenceEqual(new[] {"AT7", "AT8", "AT9", "BAT9"}));
            Assert.IsTrue(units[2].Blocks.Select(b => b.Name)
                                  .SequenceEqual(new[] {"AT10", "AT11", "AT12", "BAT12"}));
            Assert.IsTrue(units[3].Blocks.Select(b => b.Name)
                                  .SequenceEqual(new[] {"AT13", "AT14", "AT15", "BAT16"}));
            Assert.IsTrue(units[4].Blocks.Select(b => b.Name)
                                  .SequenceEqual(new[] {"AT16", "AT1", "BAT1"}));
        }


        [TestMethod]
        public void RouteCommandTest()
        {
            _sht.TimeWaitingSwitchChanged = TimeSpan.FromSeconds(5.0);
            var rt = GetRouteFirst(_sht);

            var vh = new Vehicle(_sht, rt);

            // vh will allocate the first control block of the route at Constructor

            vh.Run();
            var waitingTicks1 = _sht.TimeWaitingSwitchChanged + TimeSpan.FromSeconds(0.5);

            _scheduler.Schedule(TimeSpan.FromSeconds(0.5), () =>
                                                              {
                                                                  _serv.SendingObservable.Repeat(50).Subscribe();
                                                                  Assert.IsTrue(
                                                                      _written.ExtractDevice<SwitchState>(1, 1, 1)
                                                                             .Position == PointStateEnum.Straight);
                                                                  Assert.IsTrue(
                                                                      _written.ExtractDevice<SwitchState>(1, 1, 2)
                                                                             .Position == PointStateEnum.Straight);
                                                              });

            _scheduler.Schedule(waitingTicks1, () =>
                                                  {
                                                      _serv.SendingObservable.Repeat(50).Subscribe();
                                                      Assert.IsTrue(
                                                          _written.ExtractDevices<MotorState>(1, 2, 2)
                                                                 .Any(s => s.Duty > 0.0f));
                                                  });

            _scheduler.Start();
        }

        [TestMethod]
        public void VehicleReduceSpeedTest()
        {
            var rt = GetRouteFirst(_sht);
            var vh = new Vehicle(_sht, rt);

            // N-th case : the vehicle goes at specified speed
            vh.Run(1.0f, _sht.GetBlock("AT2"));
            _scheduler.Start();
            _serv.SendingObservable.Repeat(50).Subscribe();
            bool any = _written.ExtractDevices<MotorState>(1, 2, 2).Any(s => s.Duty == 1.0f);
            Assert.IsTrue(any);


            // 2nd case : the vehicle keeps specified speed, but entering the next section, reduces its speed to half
            var otherrt = GetRouteFirst(_sht);
            var othervh = new Vehicle(_sht, otherrt);

            _written.Clear();
            othervh.Run(1.0f, _sht.GetBlock("AT15"));
            vh.Run(1.0f, _sht.GetBlock("AT2"));

            _scheduler.Start();
            _serv.SendingObservable.Repeat(50).Subscribe();
            Assert.IsTrue(_written.ExtractDevices<MotorState>(1, 2, 2).Any(s => s.Duty == 1.0f));
            Assert.IsTrue(_written.ExtractDevices<MotorState>(1, 2, 3).Any(s => Math.Round(s.Duty, 1) == 0.5f));

            // 1st case : the vehicle reduces its speed to half immediately, and stops the next section
            _written.Clear();
            othervh.Run(1.0f, _sht.GetBlock("AT12"));
            vh.Run(1.0f, _sht.GetBlock("AT2"));

            _scheduler.Start();
            _serv.SendingObservable.Repeat(50).Subscribe();
            Assert.IsTrue(_written.ExtractDevices<MotorState>(1, 2, 2).Any(s => Math.Round(s.Duty, 1) == 0.5f));
            Assert.IsTrue(_written.ExtractDevices<MotorState>(1, 2, 3).Any(s => s.Duty == 0.0f));

            // zero case : the vehicle stops immediately
            _written.Clear();
            try
            {
                othervh.Run(1.0f, _sht.GetBlock("AT9")); // AT9 is already blocked by the other vehicle
                Assert.Fail();
            }
            catch (InvalidOperationException ex)
            {
            }
            vh.AssociatedRoute.InitLockingPosition();
            othervh.Run(1.0f, _sht.GetBlock("AT9"));
            vh.Run(1.0f, _sht.GetBlock("AT2"));

            _scheduler.Start();
            _serv.SendingObservable.Repeat(50).Subscribe();
            Assert.IsTrue(_written.ExtractDevices<MotorState>(1, 2, 2).Any(s => s.Duty == 0.0f));
        }

        [TestMethod]
        public void HaltTest()
        {
            var rt = GetRouteFirst(_sht);
            var vh = new Vehicle(_sht, rt);

            vh.Halt.Add(new Halt(_sht.GetBlock("AT4")));

            vh.Run(0.5f, _sht.GetBlock("AT2"));
            vh.Refresh();
            _serv.SendingObservable.Repeat(50).Subscribe();
            Assert.IsTrue(_written.ExtractDevices<MotorState>(1, 2, 2).Any(d => d.Duty == 1.0f));
            Assert.IsTrue(_written.ExtractDevices<MotorState>(1, 2, 3).Any(d => Math.Round(d.Duty, 1) == 0.5f));
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
    }
}