using SensorLibrary.Packet.IO;
using SensorLibrary.Devices.TusAvrDevices;
using SensorLibrary.Packet.Data;
using SensorLibrary.Packet;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Linq;
using SensorLibrary;
using SensorLibrary.Devices;
using Moq;
using System.Collections.Generic;

namespace TestProject
{


    /// <summary>
    ///EthClientTest のテスト クラスです。すべての
    ///EthClientTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class EthClientTest
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

        private EthClient sample
        {
            get
            {
                EthClient target = new EthClient();
                target.Address = new IPAddress(new byte[] { 192, 168, 2, 24 });
                return target;
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

        private IObservable<MotorState> mtr_check(EthClient target, EthPacket mtrpacket, MotorState mtrstate)
        {
            return target.AsyncSend(mtrpacket)
                .SelectMany(s => target.AsyncReceive())
                    .Repeat(2)
                .Timeout(TimeSpan.FromSeconds(1))
                .SelectMany(s => s.DataPacket.ExtractPackedPacket())
                .Where(state => state.Data.InternalAddr == mtrstate.Data.InternalAddr)
                .Cast<MotorState>()
                .Do(state =>
                            {
                                Assert.AreEqual(state.Duty, mtrstate.Duty);
                                Assert.AreEqual(state.Direction, mtrstate.Direction);
                                Assert.AreEqual(state.ControlMode, mtrstate.ControlMode);
                            });
        }

        //private IObservable<EthPacket> sw_check(EthClient target, EthPacket swpacket, SwitchState swstate)
        //{
        //    return target.AsyncSend(swpacket)
        //        .SelectMany(s => target.AsyncReceive())
        //        .Do(s =>
        //            {
        //                var data = s.DataPacket.CopyFromData<SwitchData>();
        //                var state = new SwitchState { Data = data };

        //                Assert.AreEqual(state.ChangingTime, swstate.ChangingTime);
        //                Assert.AreEqual(state.DeadTime, swstate.DeadTime);
        //                Assert.AreEqual(state.Position, swstate.Position);
        //            })
        //        .Timeout(TimeSpan.FromSeconds(3));
        //}

        [TestMethod()]
        public void UartTrainSensorControlTest()
        {
            var target = sample;
            target.Connect();

            var ethpacket = new EthPacket()
            {
                srcId = new DeviceID(100, 0),
                destId = new DeviceID(24, 1, 17)
            };
            var sens = new UsartSensor() { DeviceID = ethpacket.destId };
            var sensstate = sens.CurrentState;
            var sessetting = sens.CreateSettingDevice(2);
            var inq = Kernel.InquiryState(ethpacket.destId);

            ethpacket.DataPacket = DevicePacket.CreatePackedPacket(sessetting).First();
            target.AsyncSend(ethpacket).First();

            System.Threading.Thread.Sleep(1000);

            ethpacket.DataPacket = DevicePacket.CreatePackedPacket(sens, inq).First();
            var states = target.AsyncSend(ethpacket)
                .SelectMany(ob => target.AsyncReceive())
                .Repeat(2)
                .Do(pack =>
                    {
                        Assert.IsTrue(pack.srcId.ModuleAddr == ethpacket.destId.ModuleAddr);
                        Assert.IsTrue(pack.srcId.ParentPart == ethpacket.destId.ParentPart);
                    })
                .SelectMany(pack => pack.DataPacket.ExtractPackedPacket())
                .Timeout(TimeSpan.FromSeconds(1))
                .ToArray().First();

            foreach (var state in states)
            {
                if (state.ModuleType == ModuleTypeEnum.AvrUartSetting
                        && state.Data.InternalAddr == 16)
                {
                    var setting = (UsartSettingState)state;
                    Assert.IsTrue(setting.ModuleCount == 2);
                }
            }

        }

        [TestMethod()]
        public void MotorControl_WaitTest()
        {
            var target = sample;
            target.Connect();

            var mtrpacket = new EthPacket()
            {
                srcId = new DeviceID(100, 0),
                destId = new DeviceID(24, 1, 0),
            };
            var mtr = new Motor() { DeviceID = mtrpacket.destId };
            var mtrstate = mtr.CurrentState;
            var inqiry = Kernel.InquiryState(mtrpacket.destId);

            var memch = Kernel.MemoryState(mtrpacket.destId, new MemoryState(0));
            var memstate = (MemoryState)memch.CurrentState;

            mtr.DeviceID = new DeviceID(24, 1, 2);
            mtrstate.ID = mtr.DeviceID;
            mtrstate.ControlMode = MotorControlMode.DutySpecifiedMode;
            mtrstate.Duty = 0.5f;
            mtrstate.Direction = MotorDirection.Positive;
            memch = Kernel.MemoryState(mtrstate.ID, new MemoryState(1));
            mtrpacket.DataPacket = DevicePacket.CreatePackedPacket(memch, mtr).First();
            target.AsyncSend(mtrpacket).Subscribe();
            System.Threading.Thread.Sleep(100);

            mtr.DeviceID = new DeviceID(24, 1, 2);
            mtrstate.ID = mtr.DeviceID;
            mtrstate.ControlMode = MotorControlMode.DutySpecifiedMode;
            mtrstate.Duty = 0.5f;
            mtrstate.Direction = MotorDirection.Negative;
            memch = Kernel.MemoryState(mtrstate.ID, new MemoryState(2));
            mtrpacket.DataPacket = DevicePacket.CreatePackedPacket(memch, mtr).First();
            target.AsyncSend(mtrpacket).Subscribe();
            System.Threading.Thread.Sleep(100);

            mtr.DeviceID = new DeviceID(24, 1, 1);
            mtrstate.ID = mtr.DeviceID;
            mtrstate.ControlMode = MotorControlMode.DutySpecifiedMode;
            mtrstate.Duty = 0.5f;
            mtrstate.Direction = MotorDirection.Positive;
            memch = Kernel.MemoryState(mtrstate.ID, new MemoryState(1));
            mtrpacket.DataPacket = DevicePacket.CreatePackedPacket(memch, mtr).First();
            target.AsyncSend(mtrpacket).Subscribe();
            System.Threading.Thread.Sleep(100);

            mtr.DeviceID = new DeviceID(24, 1, 1);
            mtrstate.ID = mtr.DeviceID;
            mtrstate.ControlMode = MotorControlMode.WaitingPulseMode;
            mtrstate.MemoryWhenEntered = MotorMemoryStateEnum.NoEffect;
            mtrstate.DestinationID = new DeviceID(24, 1, 2);
            mtrstate.DestinationMemory = MotorMemoryStateEnum.NoEffect;
            mtrstate.ThresholdCurrent = 0.5f;
            memch = Kernel.MemoryState(mtrstate.ID, new MemoryState(2));
            mtrpacket.DataPacket = DevicePacket.CreatePackedPacket(memch, mtr).First();
            target.AsyncSend(mtrpacket).Subscribe();
            System.Threading.Thread.Sleep(100);





        }

        [TestMethod()]
        public void MotorControl_MemoryTest()
        {
            var target = sample;
            target.Connect();

            var mtrpacket = new EthPacket()
            {
                srcId = new DeviceID(100, 0),
                destId = new DeviceID(24, 1, 1),
            };
            var mtr = new Motor() { DeviceID = mtrpacket.destId };
            var mtrstate = mtr.CurrentState;
            var inqiry = Kernel.InquiryState(mtrpacket.destId);

            var memch = Kernel.MemoryState(mtrpacket.destId, new MemoryState(0));
            var memstate = (MemoryState)memch.CurrentState;

            memstate.CurrentMemory = 0;
            mtrpacket.DataPacket
                = DevicePacket.CreatePackedPacket(memch).First();

            mtr_check(target, mtrpacket, mtrstate);

            // 1: write mtrstate to memory 0, and check the remote state is changed
            mtrstate.ControlMode = MotorControlMode.DutySpecifiedMode;
            mtrstate.Direction = MotorDirection.Positive;
            mtrstate.Duty = 0.5f;

            mtrpacket.DataPacket
                = DevicePacket.CreatePackedPacket(mtr, inqiry).First();

            mtr_check(target, mtrpacket, mtrstate);
            System.Threading.Thread.Sleep(1000);

            // 2: change to memory 1, and check the memory is changed
            mtrstate.Direction = MotorDirection.Negative;
            memstate.CurrentMemory = 1;

            mtrpacket.DataPacket
                = DevicePacket.CreatePackedPacket(memch, mtr, inqiry).First();

            mtr_check(target, mtrpacket, mtrstate);
            System.Threading.Thread.Sleep(1000);

            // 3: change to memory 0, and check the memory is saved 
            memstate.CurrentMemory = 0;

            mtrpacket.DataPacket
                = DevicePacket.CreatePackedPacket(memch, inqiry).First();

            mtr_check(target, mtrpacket, mtrstate);
            System.Threading.Thread.Sleep(1000);

        }

        [TestMethod()]
        public void MotorCurrentTest()
        {
            var target = sample;
            target.Connect();

            var mtrpacket = new EthPacket()
            {
                srcId = new DeviceID(111, 0),
                destId = new DeviceID(24, 1, 0),
            };
            var mtrA = new Motor() { DeviceID = new DeviceID(24, 1, 1) };
            var mtrB = new Motor() { DeviceID = new DeviceID(24, 1, 2) };

            var kernal = Kernel.InquiryState(mtrpacket.destId);

            mtrA.CurrentState.Direction = MotorDirection.Positive;
            mtrA.CurrentState.ControlMode = MotorControlMode.DutySpecifiedMode;
            mtrA.CurrentState.Duty = 0.3f;

            mtrB.CurrentState.Direction = MotorDirection.Positive;
            mtrB.CurrentState.ControlMode = MotorControlMode.DutySpecifiedMode;
            mtrB.CurrentState.Duty = 0;

            mtrpacket.DataPacket = DevicePacket.CreatePackedPacket(mtrA, mtrB).First();

            target.Send(mtrpacket);
        }

        [TestMethod()]
        public void MotorControlTest()
        {
            var target = sample;
            target.Connect();

            var mtrpacket = new EthPacket()
            {
                srcId = new DeviceID(111, 0),
                destId = new DeviceID(24, 1, 1),
            };
            var mtr = new Motor() { DeviceID = mtrpacket.destId };
            var mtrstate = mtr.CurrentState;
            var kernal = Kernel.InquiryState(mtrpacket.destId);

            mtrstate.Duty = 0.5f;
            mtrstate.Direction = MotorDirection.Positive;
            mtrstate.ControlMode = MotorControlMode.DutySpecifiedMode;

            var param = Enumerable.Range(1, 4)
                                .SelectMany(i =>
                                    new[] { MotorDirection.Standby, MotorDirection.Positive, MotorDirection.Negative }
                                        .Select(p => new { devnum = i, dir = p })
                                        );
            var osb = param.Select(s =>
            {
                mtrpacket.destId.InternalAddr = (byte)s.devnum;
                mtr.DeviceID = mtrpacket.destId;
                mtrstate.Direction = s.dir;
                mtrstate.Data.InternalAddr = (byte)s.devnum;

                mtrpacket.DataPacket
                    = DevicePacket.CreatePackedPacket(new IDevice<IDeviceState<IPacketDeviceData>>[] { mtr, kernal }).First();

                System.Threading.Thread.Sleep(1000);

                return mtr_check(target, mtrpacket, mtrstate)
                    .Any();
            }).ToArray();
        }

        [TestMethod()]
        public void sw_test()
        {
            //var target = sample;
            //target.Connect();

            var log = new List<IDeviceState<IPacketDeviceData>>();
            var logeth = new List<EthPacket>();
            var target = new Mock<EthClient>();
            target.Setup(e => e.AsyncSend(It.IsAny<EthPacket>()))
                .Returns(() => Observable.Empty<Unit>())
                .Callback<EthPacket>(e =>
                {
                    log.AddRange(e.DataPacket.ExtractPackedPacket());
                    logeth.Add(e);
                });

            var ptpacket = new EthPacket()
            {
                srcId = new DeviceID(100, 0),
                destId = new DeviceID(24, 1, 1),
            };

            var pt = new Switch();
            pt.CurrentState.DeadTime = 150;
            pt.CurrentState.ChangingTime = 200;


            var prm = Enumerable.Range(1, 8)
                        .SelectMany(i =>
                            new[] { PointStateEnum.Straight, PointStateEnum.Curve }
                                .Select(p => new { devnum = i, position = p })
                                );

            var ob = prm.Select(a =>
                {
                    var id = ptpacket.destId;
                    id.InternalAddr = (byte)a.devnum;

                    pt.DeviceID = id;
                    pt.CurrentState.ID = id;
                    pt.CurrentState.Position = a.position;

                    ptpacket.DataPacket = DevicePacket.CreatePackedPacket(
                            pt,
                            Kernel.InquiryState(id)
                            ).First();

                    System.Threading.Thread.Sleep(1000);

                    return target.Object.AsyncSend(ptpacket)
                        //.SelectMany(a => target.AsyncReceive())
                        //.SelectMany(a => a.DataPacket.ExtractPackedPacket())
                        //.Where(state => state.ID == pt.DeviceID)
                        //.Cast<SwitchState>()
                        //.Do(state => Assert.IsTrue(state.Position == pt.CurrentState.Position))
                        //.Timeout(TimeSpan.FromSeconds(1))
                        .Subscribe();

                }).ToArray();

        }

        /// <summary>
        ///Send のテスト
        ///</summary>
        //[TestMethod()]
        //public void SendTest()
        //{
        //    var target = sample;
        //    EthPacket packet = new EthPacket()
        //    {
        //        srcId = new DeviceID(100, 0),
        //        destId = new DeviceID(24, 3),
        //    };

        //    var ptpacket = new EthPacket()
        //                       {
        //                           srcId = new DeviceID(100, 0),
        //                           destId = new DeviceID(24, 1)
        //                       };

        //    var ptstate = new SwitchState() { Data = ptpacket.DataPacket.CopyFromData<SwitchData>() };

        //    ptstate.ChangingTime = 200;
        //    ptstate.DeadTime = 100;
        //    ptstate.Position = PointStateEnum.Straight;
        //    ptstate.FlushDataState();

        //    for (int i = 0; i < packet.DataPacket.Data.Length; ++i)
        //        packet.DataPacket.Data[i] = 0xCC;

        //    for (int i = 0; i < 30000; ++i)
        //    {
        //        //packet.Message = string.Format("pero {0} times", i);

        //        target.Send(packet);

        //        //ptstate.Position = (i % 2 == 0) ? PointStateEnum.Straight : PointStateEnum.Curve;
        //        //ptstate.FlushDataState();

        //        System.Threading.Thread.Sleep(100);
        //        target.Send(ptpacket);
        //        System.Threading.Thread.Sleep(100);
        //        //target.Send(mtrpacket);
        //        //System.Threading.Thread.Sleep(100);
        //    }
        //    Assert.Inconclusive("値を返さないメソッドは確認できません。");
        //}

    }
}
