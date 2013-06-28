using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;

namespace TestProject
{
    
    
    /// <summary>
    ///TrainSensorDataTest のテスト クラスです。すべての
    ///TrainSensorDataTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class TrainSensorDataTest
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
        ///CurrentVoltage のテスト
        ///</summary>
        //[TestMethod()]
        //public void CurrentVoltageTest()
        //{
        //    var target = new TrainSensorState();
        //    target.VoltageResolution = 10;
        //    target.ReferenceVoltageMinus = 0;
        //    target.ReferenceVoltagePlus = 5;

        //    target.CurrentVoltage = 12.34f;
        //    Assert.AreEqual(target.CurrentVoltage, 12.34f);
        //}

        ///// <summary>
        /////ThresholdVoltage のテスト
        /////</summary>
        //[TestMethod()]
        //public void ThresholdVoltageTest()
        //{
        //    TrainSensorData target = new TrainSensorData();
        //    target.VoltageResolution = 10;
        //    target.ReferenceVoltageMinus = 0;
        //    target.ReferenceVoltagePlus = 5;

        //    target.ThresholdVoltage = 12.34f;
        //    Assert.AreEqual(target.ThresholdVoltage, 12.34f);

        //}

        /// <summary>
        ///TrainSensorData コンストラクター のテスト
        ///</summary>
    }
}
