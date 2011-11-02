using SensorLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    
    
    /// <summary>
    ///TrainSensorStateTest のテスト クラスです。すべての
    ///TrainSensorStateTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class TrainSensorStateTest
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

        public static TrainSensorState TestState
        {
            get
            {
                return TestPacketProvider.TestTrainSensorState;
            }
        }

        /// <summary>
        ///ToString のテスト
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            TrainSensorState target = TestState;
            string actual;
            actual = target.ToString();
            this.TestContext.WriteLine(actual);
            Assert.IsTrue(true);
        }

        /// <summary>
        ///CurrentVoltage のテスト
        ///</summary>
        [TestMethod()]
        public void CurrentVoltageTest()
        {
            TrainSensorState target = TestState;
            foreach(float expected in new float[] {1.0F, 2.0F, 2.5F, 3.0F, 4.0F} )
            {
            float actual;
            target.CurrentVoltage = expected;
            actual = (float)Math.Round(target.CurrentVoltage, 1);
            Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///IsDetected のテスト
        ///</summary>
        [TestMethod()]
        public void IsDetectedTest()
        {
            TrainSensorState target = TestState;
            target.Mode = TrainSensorMode.detecting;
            bool expected = false; 
            bool actual;
            target.IsDetected = expected;
            actual = target.IsDetected;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Mode のテスト
        ///</summary>
        [TestMethod()]
        public void ModeTest()
        {
            TrainSensorState target = TestState;
            TrainSensorMode expected = TrainSensorMode.detecting;
            TrainSensorMode actual;
            target.Mode = expected;
            actual = target.Mode;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///OverflowedCount のテスト
        ///</summary>
        [TestMethod()]
        public void OverflowedCountTest()
        {
            TrainSensorState target = TestState;
            ushort expected = 11; // no means
            ushort actual;
            target.OverflowedCount = expected;
            actual = target.OverflowedCount;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///ThresholdVoltage のテスト
        ///</summary>
        [TestMethod()]
        public void ThresholdVoltageTest()
        {
            TrainSensorState target = TestState;
            foreach (float expected in new float[] { 1.0F, 2.0F, 2.5F, 3.0F, 4.0F })
            {
                float actual;
                target.ThresholdVoltageLower = expected;
                actual = (float)Math.Round(target.ThresholdVoltageLower,1);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///Timer のテスト
        ///</summary>
        [TestMethod()]
        public void TimerTest()
        {
            TrainSensorState target = TestState;
            ushort expected = 60000; 
            ushort actual;
            target.Timer = expected;
            actual = target.Timer;
            Assert.AreEqual(expected, actual);
        }
    }
}
