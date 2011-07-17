using SensorLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TestProject
{


    /// <summary>
    ///PointModuleStateTest のテスト クラスです。すべての
    ///PointModuleStateTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class PointModuleStateTest
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

        public static PointModuleState TestState
        {
            get
            {
                return TestPacketProvider.TestPointModuleState;
            }

        }


        /// <summary>
        ///PointModuleState コンストラクター のテスト
        ///</summary>
        [TestMethod()]
        public void PointModuleStateConstructorTest()
        {
            var packet = new DevicePacket();
            packet.ModuleType = ModuleTypeEnum.PointModule;
            PointModuleState target = new PointModuleState(packet);
        }

        /// <summary>
        ///Item のテスト
        ///</summary>
        [TestMethod()]
        public void ItemTest()
        {
            PointModuleState target = TestState;
            var expected = Enumerable.Repeat(PointStateEnum.Curve, target.Data.Directions.Length * 8).ToList();
            PointStateEnum actual;
            foreach (var exp in expected)
            {
                int addr = expected.IndexOf(exp);
                target[addr] = exp;
                actual = target[addr];
                Assert.AreEqual(exp, actual);
            }
        }
    }
}
