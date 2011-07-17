using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SensorTest
{
    
    
    /// <summary>
    ///PacketExtensionTest のテスト クラスです。すべての
    ///PacketExtensionTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class PacketExtensionTest
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
        ///ReadPacket のテスト
        ///</summary>
        [TestMethod()]
        public void ReadPacketTest()
        {
            var servtest = new PacketServerTest();
            var serv = servtest.GetTestServer();
            
            var actual = PacketExtension.ReadPacket(serv.BaseStream);
        }

        /// <summary>
        ///WritePacket のテスト
        ///</summary>
        [TestMethod()]
        public void WritePacketTest()
        {
            Stream st = new MemoryStream(); // TODO: 適切な値に初期化してください
            DevicePacket pack = new DevicePacket()
            {
                ID = new DeviceID() { ParentPart = 1, ModulePart = 1 },
                ModuleType = ModuleTypeEnum.TrainSensor,
                Data = "test"
            };

            PacketExtension.WritePacket(st, pack);
            bool res = false;

            st.Seek(0, SeekOrigin.Begin);
            var read = st.ReadPacket();
            res = read.ID == pack.ID;
            res = read.ModuleType == pack.ModuleType;
            res = read.Data == pack.Data;

            Assert.IsTrue(res);
        }
    }
}
