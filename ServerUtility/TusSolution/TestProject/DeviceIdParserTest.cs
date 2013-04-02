using Tus.Parser;
using Tus.Route.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;

using System.Collections.Generic;
using System.Linq;
using Tus.TransControl.Parser;

namespace TestProject
{
    
    
    /// <summary>
    ///DeviceIdParserTest のテスト クラスです。すべての
    ///DeviceIdParserTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class DeviceIdParserTest
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
        ///FromString のテスト
        ///</summary>
        [TestMethod()]
        public void FromStringTest1()
        {
            DeviceIdParser target = new DeviceIdParser();
            string context = "(1, 2, 3); (4, 5, 6);";
            IEnumerable<DeviceID> expected = new DeviceID[] { new DeviceID() { ParentPart = 1, ModuleAddr = 2, InternalAddr = 3},
                                                              new DeviceID() { ParentPart = 4, ModuleAddr = 5, InternalAddr = 6}};
            IEnumerable<DeviceID> actual;
            actual = target.FromString(context).ToList();
            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}
