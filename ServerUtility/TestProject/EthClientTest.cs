﻿using SensorLibrary.Packet.IO;
using SensorLibrary.Devices.TusAvrDevices;
using SensorLibrary.Packet.Data;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Linq;
using SensorLibrary;

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

        private IObservable<EthPacket> mtr_check(EthClient target, EthPacket mtrpacket, MotorState mtrstate)
        {
            return target.AsyncSend(mtrpacket)
                .SelectMany(s => target.AsyncReceive())
                .Do(s =>
                            {
                                var data = s.DataPacket.CopyFromData<MotorData>();
                                var state = new MotorState() { Data = data };

                                Assert.AreEqual(state.Duty, mtrstate.Duty);
                                Assert.AreEqual(state.Direction, mtrstate.Direction);
                                Assert.AreEqual(state.ControlMode, mtrstate.ControlMode);
                            })
                .Timeout(TimeSpan.FromSeconds(1));
        }

        private IObservable<EthPacket> sw_check(EthClient target, EthPacket swpacket, SwitchState swstate)
        {
            return target.AsyncSend(swpacket)
                .SelectMany(s => target.AsyncReceive())
                .Do(s =>
                    {
                        var data = s.DataPacket.CopyFromData<SwitchData>();
                        var state = new SwitchState { Data = data };

                        Assert.AreEqual(state.ChangingTime, swstate.ChangingTime);
                        Assert.AreEqual(state.DeadTime, swstate.DeadTime);
                        Assert.AreEqual(state.Position, swstate.Position);
                    })
                .Timeout(TimeSpan.FromSeconds(1));
        }

        [TestMethod()]
        public void MotorControlTest()
        {
            var target = sample;
            target.Connect();

            var mtrpacket = new EthPacket()
            {
                srcId = new DeviceID(102, 0),
                destId = new DeviceID(24, 2, 4),
            };
            var mtrdata = new MotorData();
            var mtrstate = new MotorState()
            {
                BasePacket = mtrpacket.DataPacket,
                Duty = 0.5f,
                Direction = MotorDirection.Positive,
                ControlMode = MotorControlMode.DutySpecifiedMode,
            };
            mtrstate.FlushDataState();

            var param = Enumerable.Range(1, 4)
                                .SelectMany(i =>
                                    new[] { MotorDirection.Standby, MotorDirection.Positive, MotorDirection.Negative }
                                        .Select(p => new { devnum = i, dir = p })
                                        );
            var osb = param.Select(s =>
            {
                mtrpacket.destId.InternalAddr = (byte)s.devnum;
                mtrstate.Direction = s.dir;
                mtrstate.FlushDataState();

                return mtr_check(target, mtrpacket, mtrstate)
                            .First();
            }).ToArray();
        }

        [TestMethod()]
        public void sw_test()
        {
            var target = sample;
            target.Connect();

            var ptpacket = new EthPacket()
            {
                srcId = new DeviceID(100, 0),
                destId = new DeviceID(24, 1, 1),
            };
            var ptdata = new SensorLibrary.Packet.Data.SwitchData();
            var ptstate = new SwitchState()
            {
                BasePacket = ptpacket.DataPacket,
                Data = ptdata,
                Position = SensorLibrary.Packet.Data.PointStateEnum.Curve,
                DeadTime = 150,
                ChangingTime = 200,
            };
            ptstate.FlushDataState();

            var prm = Enumerable.Range(1, 8)
                        .SelectMany(i =>
                            new[] { PointStateEnum.Straight, PointStateEnum.Curve }
                                .Select(p => new { devnum = i, position = p })
                                );

            var ob = prm.Select(a =>
                {
                    ptpacket.destId.InternalAddr = (byte)a.devnum;
                    ptstate.Position = a.position;
                    ptstate.FlushDataState();

                    return sw_check(target, ptpacket, ptstate).First();
                }).ToArray();

        }

        /// <summary>
        ///Send のテスト
        ///</summary>
        [TestMethod()]
        public void SendTest()
        {
            var target = sample;
            EthPacket packet = new EthPacket()
            {
                srcId = new DeviceID(100, 0),
                destId = new DeviceID(24, 3),
            };

            var ptpacket = new EthPacket()
                               {
                                   srcId = new DeviceID(100, 0),
                                   destId = new DeviceID(24, 1)
                               };

            var ptstate = new SwitchState() { Data = ptpacket.DataPacket.CopyFromData<SwitchData>() };

            ptstate.ChangingTime = 200;
            ptstate.DeadTime = 100;
            ptstate.Position = PointStateEnum.Straight;
            ptstate.FlushDataState();

            for (int i = 0; i < packet.DataPacket.Data.Length; ++i)
                packet.DataPacket.Data[i] = 0xCC;

            for (int i = 0; i < 30000; ++i)
            {
                //packet.Message = string.Format("pero {0} times", i);

                target.Send(packet);

                //ptstate.Position = (i % 2 == 0) ? PointStateEnum.Straight : PointStateEnum.Curve;
                //ptstate.FlushDataState();

                System.Threading.Thread.Sleep(100);
                target.Send(ptpacket);
                System.Threading.Thread.Sleep(100);
                //target.Send(mtrpacket);
                //System.Threading.Thread.Sleep(100);
            }
            Assert.Inconclusive("値を返さないメソッドは確認できません。");
        }

    }
}
