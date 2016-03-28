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
                                                })
            { IsRepeatable = true };
            return route;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var mockio = new Mock<IDeviceIO>();
            _written = new List<IDeviceState<IPacketDeviceData>>();
            mockio.Setup(e => e.GetReadingPacket()).Returns(Observable.Empty<DevicePacket>());
            mockio.Setup(e =>
                         e.WritePacket(It.IsAny<DevicePacket>())).Callback<DevicePacket>(pack =>
                                                                                         _written.AddRange(
                                                                                             pack
                                                                                                 .ExtractPackedPacket
                                                                                                 ())
                );
            _serv = new PacketServer { Controller = mockio.Object };
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
            _sht.TimeWaitingSwitchChanged = TimeSpan.FromSeconds(5.0);
            var rt = GetRouteFirst(_sht);
            var vh = new Vehicle(_sht, rt);

            // init switch position for this test
            foreach (var sw in _sht.AllSwitches)
            {
                sw.CurrentState.Position = PointStateEnum.Any;
            }

            // vh will allocate the first control block of the route at Constructor
            vh.Accelation = 100.0f; // ignoring speed control
            vh.Run();
            var waitingTicks1 = _sht.TimeWaitingSwitchChanged + TimeSpan.FromSeconds(0.5);

            _scheduler.Schedule(TimeSpan.FromSeconds(0.5), () =>
                                                              {
                                                                  _serv.SendAll();
                                                                  Assert.IsTrue(
                                                                      _written.ExtractDevice<SwitchState>(1, 1, 1)
                                                                             .Position == PointStateEnum.Straight);
                                                                  Assert.IsTrue(
                                                                      _written.ExtractDevice<SwitchState>(1, 1, 2)
                                                                             .Position == PointStateEnum.Straight);
                                                              });

            _scheduler.Schedule(waitingTicks1, () =>
                                                  {
                                                      _serv.SendAll();
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
            vh.Accelation = 10000.0f;

            // N-th case : the vehicle goes at specified speed
            vh.Run(1.0f, _sht.GetBlock("AT2"));
            _scheduler.Start();
            _serv.SendAll();
            bool any = _written.ExtractDevices<MotorState>(1, 2, 2).Any(s => s.Duty == 1.0f);
            Assert.IsTrue(any);


            // 2nd case : the vehicle keeps specified speed, but entering the next section, this vehicle will stop
            var otherrt = GetRouteFirst(_sht);
            var othervh = new Vehicle(_sht, otherrt);

            _written.Clear();
            othervh.Run(1.0f, _sht.GetBlock("AT15"));
            vh.Run(1.0f, _sht.GetBlock("AT2"));

            _scheduler.Start();
            _serv.SendAll();
            Assert.IsTrue(_written.ExtractDevices<MotorState>(1, 2, 2).Any(s => s.Duty == 1.0f));
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
            vh.Run(0);
            vh.AssociatedRoute.UnReserveHead();
            vh.AssociatedRoute.InitLockingPosition();
            othervh.Run(1.0f, _sht.GetBlock("AT9"));

            _scheduler.Start();
            _serv.SendAll();
            Assert.IsTrue(_written.ExtractDevices<MotorState>(1, 2, 2).Any(s => s.Duty == 0.0f));
        }

        [TestMethod]
        public void HaltTest()
        {
            Assert.Inconclusive(); // HaltHere等のHaltの他に，センサーを用いて閉塞を開放する機能があったが，現在使用していない
            var rt = GetRouteFirst(_sht);
            var vh = new Vehicle(_sht, rt);

            vh.Halt.Add(new Halt(_sht.GetBlock("AT4")));

            vh.Accelation = 10000.0f;
            vh.Run(0.5f, _sht.GetBlock("AT2"));
            //vh.Refresh();
            _serv.SendAll();
            Assert.IsTrue(_written.ExtractDevices<MotorState>(1, 2, 2).Any(d => d.Duty == 1.0f));
            Assert.IsTrue(_written.ExtractDevices<MotorState>(1, 2, 3).Any(d => Math.Round(d.Duty, 1) == 0.5f));
        }

        [TestMethod]
        public void HaltHereTest()
        {
            var rt = GetRouteFirst(_sht);
            var vh = new Vehicle(_sht, rt);

            vh.Halt.Add(new Halt(_sht.GetBlock("AT4")));

            vh.Accelation = 1.0f;
            vh.Run(0.0f, _sht.GetBlock("AT2"));
            Assert.IsTrue(vh.CanHaltHere);
            vh.HaltHere();

            //LockedBlock is only one
            Assert.IsFalse(rt.Blocks.Except(new[] { _sht.GetBlock("AT4") }).Any(b => b.IsLocked));
            Assert.IsTrue(vh.Speed == 0.0f);
            Assert.IsTrue(vh.IsHalted);

            Assert.IsTrue(vh.CanLeaveHere);
            vh.LeaveHere();
            Assert.IsFalse(vh.IsHalted);

            vh.Run(0.5f);
            vh.Run(0.0f);
            Assert.IsTrue(vh.CanHaltHere);
            vh.HaltHere();
            Assert.IsTrue(vh.CanLeaveHere);
            vh.Run(0.5f);
            Assert.IsFalse(vh.IsHalted); // RunでHalt状態は解除される
        }

        [TestMethod]
        public void CanLeaveHere_If_Other_Vehicle_Comming()
        {
            var rt = GetRouteFirst(_sht);
            var vh = new Vehicle(_sht, rt);

            vh.Halt.Add(new Halt(_sht.GetBlock("AT4")));

            vh.Accelation = 1.0f;
            vh.Run(0.0f, _sht.GetBlock("AT2"));
            Assert.IsTrue(vh.CanHaltHere);
            vh.HaltHere();

            var rt2 = GetRouteFirst(_sht);
            var vh2 = new Vehicle(_sht, rt2);
            vh2.Accelation = 1.0f;
            vh2.Run(0.0f, "AT16");
            Assert.IsFalse(vh.CanLeaveHere); // 他の列車が前の閉塞にいるのでLeaveできない
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