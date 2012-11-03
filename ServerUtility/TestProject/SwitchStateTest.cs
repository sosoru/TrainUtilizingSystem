using SensorLibrary.Devices.TusAvrDevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Data;
using System.Linq;

namespace TestProject
{


    /// <summary>
    ///SwitchStateTest のテスト クラスです。すべての
    ///SwitchStateTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class SwitchStateTest
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

        SwitchState sample_state
        {
            get
            {
                var info = new SwitchData();
                var state = new SwitchState();

                state.Data = info;
                return state;
            }
        }


        /// <summary>
        ///DeadTime のテスト
        ///</summary>
        [TestMethod()]
        public void DeadTimeTest()
        {
            var target = sample_state;

            target.Data.DeadTime = 66;
            Assert.AreEqual(target.DeadTime, 166);

            target.DeadTime = 111;
            Assert.AreEqual(target.Data.DeadTime, 11);

            target.DeadTime = 100;
            try
            {
                target.DeadTime = 99;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e) { }

            target.DeadTime = 355;
            try
            {
                target.DeadTime = 356;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e) { }

        }

        /// <summary>
        ///ChangingTime のテスト
        ///</summary>
        [TestMethod()]
        public void ChangingTimeTest()
        {
            var target = sample_state;

            target.Data.ChangingTime = 1;
            Assert.AreEqual(target.ChangingTime, 10);

            target.ChangingTime = 55;
            Assert.AreEqual(target.Data.ChangingTime, 6);

            target.ChangingTime = 0;
            try
            {
                target.ChangingTime = -20;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e) { }

            target.ChangingTime = 1005;
            try
            {
                target.ChangingTime = 1006;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e) { }

        }

        /// <summary>
        ///Position のテスト
        ///</summary>
        [TestMethod()]
        public void PositionTest()
        {
            var target = sample_state;

            target.Position = PointStateEnum.Curve;
            Assert.AreEqual(target.Data.Position, (byte)PointStateEnum.Curve);

            target.Data.Position = (byte)PointStateEnum.Straight;
            Assert.AreEqual(target.Position, PointStateEnum.Straight);

        }

        [TestMethod()]
        public void SerializeTest()
        {
            var targetA = sample_state;
            var targetB = sample_state;

            targetA.DeadTime = 200;
            targetA.ChangingTime = 50;

            targetB.DeadTime = targetA.DeadTime;
            targetB.ChangingTime = targetA.ChangingTime;

            targetA.ID = new DeviceID(24, 1, 1);
            targetB.ID = new DeviceID(24, 1, 2);

            targetA.Position = PointStateEnum.Straight;
            targetB.Position = PointStateEnum.Curve;

            var swA = new Switch();
            var swB = new Switch();

            swA.CurrentState = targetA;
            swA.DeviceID = targetA.ID;
            swB.CurrentState = targetB;
            swB.DeviceID = targetB.ID;

            var packetA = DevicePacket.CreatePackedPacket(targetA);
            var packetB = DevicePacket.CreatePackedPacket(targetB);

            Assert.IsFalse(packetA.First().Data.SequenceEqual(packetB.First()));
        }
    }
}
