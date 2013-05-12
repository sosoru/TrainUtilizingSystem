using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;

namespace TestProject
{


    /// <summary>
    ///SensorStateTest のテスト クラスです。すべての
    ///SensorStateTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class SensorStateTest
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

        SensorState sample_state
        {
            get
            {
                var data = new SensorData();
                var state = new SensorState();

                state.Data = data;
                return state;
            }
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
              "|DataDirectory|\\VoltageTestCase.csv",
              "VoltageTestCase#csv",
              DataAccessMethod.Sequential),
          DeploymentItem("VoltageTestCase.csv"),
          TestMethod]
        public void VoltageOnTest()
        {
            var target = new SensorState();
            VoltageTest(v => target.VoltageOn = (float)v, () => target.VoltageOn);
        }

        
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\VoltageTestCase.csv",
            "VoltageTestCase#csv",
            DataAccessMethod.Sequential),
         DeploymentItem("VoltageTestCase.csv"),
         TestMethod]
        public void VoltageOffTest()
        {
            var target = new SensorState();
            VoltageTest(v => target.VoltageOff = (float) v, () => target.VoltageOff);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\VoltageTestCase.csv",
            "VoltageTestCase#csv",
            DataAccessMethod.Sequential),
         DeploymentItem("VoltageTestCase.csv"),
         TestMethod]
        public void VoltageThresholdTest()
        {
            var target = new SensorState();
            VoltageTest(v => target.Threshold = (float) v, () => target.Threshold);
        }


        private void VoltageTest(Action<double> setfunc, Func<double> getfunc)
        {
            var expected = (double) this.TestContext.DataRow["Expected"];
            var actual = (double) this.TestContext.DataRow["Result"];

            try
            {
                setfunc(expected);
                var result = getfunc();
                Assert.AreEqual(actual, Math.Round(result, 1));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                if (!(expected < 0.0f || 1.0f < expected))
                {
                    Assert.Fail("not thrown argument out of range exception");
                }
            }
        }
    }
}
