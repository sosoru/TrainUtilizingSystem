using SensorLibrary.Packet.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
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
        ///Send のテスト
        ///</summary>
        [TestMethod()]
        public void SendTest()
        {
            EthClient target = new EthClient();
            target.Address = new IPAddress(new byte [] { 192, 168, 2, 24 });
            EthPacket packet = new EthPacket()
            {
                srcId = new DeviceID(100, 0),
                destId = new DeviceID(24, 1),
            };
            
            for (int i=0; i < packet.Data.Length; ++i)
                packet.Data [i] = 0xCC;

            for (int i = 0; i < 30000; ++i)
            {
                //packet.Message = string.Format("pero {0} times", i);

                target.Send(packet);
                System.Threading.Thread.Sleep(500);
            }
            Assert.Inconclusive("値を返さないメソッドは確認できません。");
        }

    }
}
