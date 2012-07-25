using SensorLibrary.DeviceStates.AvrDevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SensorLibrary.Packet;
using SensorLibrary.Devices;
using SensorLibrary;

namespace TestProject
{


    /// <summary>
    ///MotorStateTest のテスト クラスです。すべての
    ///MotorStateTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class MotorStateTest
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

        /// <summary>
        ///ControlMode のテスト
        ///</summary>
        [TestMethod()]
        public void ControlModeTest()
        {
            var info = new MotorData();
            var actual = new MotorState();
            actual.Data = info;

            actual.Data.ControlMode = 1;
            var ex1 = actual.ControlMode;
            Assert.AreEqual((byte)ex1, 1);

            actual.ControlMode = MotorControlMode.CurrentFeedBackMode;
            Assert.AreEqual((byte)MotorControlMode.CurrentFeedBackMode, actual.Data.ControlMode);

        }


        /// <summary>
        ///Direction のテスト
        ///</summary>
        [TestMethod()]
        public void DirectionTest()
        {
            var info = new MotorData();
            var state = new MotorState();
            state.Data = info;

            state.Data.Direction = 66;
            Assert.AreEqual(state.Direction, (MotorDirection)66);

            state.Direction = MotorDirection.Positive;
            Assert.AreEqual(state.Data.Direction, (byte)MotorDirection.Positive);
        }

        /// <summary>
        ///Duty のテスト
        ///</summary>
        [TestMethod()]
        public void DutyTest()
        {
            var info = new MotorData();
            var state = new MotorState();
            state.Data = info;

            state.Data.Duty = 127;
            Assert.AreEqual(state.Duty, 127.0f / 255.0f);

            state.Duty = 0.3f;
            Assert.AreEqual(state.Data.Duty, Math.Round(0.3f * 255.0f));

            try
            {
                state.Duty = -1.0f;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e) { }

            try
            {
                state.Duty = 100.0f;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e) { }

        }
    }
}
