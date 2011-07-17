using SensorLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    
    
    /// <summary>
    ///ConvertExtenstionsTest のテスト クラスです。すべての
    ///ConvertExtenstionsTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ConvertExtenstionsTest
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
        ///ToByteArray のテスト
        ///</summary>
        [TestMethod()]
        public void ToByteArrayTest()
        {
            var obj = new DevicePacket();

            obj.ID.ParentPart = 1;
            obj.ID.ModulePart = 1;
            obj.ModuleType = ModuleTypeEnum.TrainSensor;
            for (int i = 0; i < obj.Data.Length; i++)
                obj.Data[i] = (byte)i;

            byte[] expected = new byte[32];
            expected[0] = 0xFF;
            expected[1] = 1;
            expected[2] = 1;
            expected[3] = 1;
            for (int i = 4; i < expected.Length; i++)
                expected[i] = (byte)(i-4);

            byte[] actual;
            actual = ConvertExtenstions.ToByteArray(obj);

            Assert.AreEqual(actual.Length, expected.Length);
            for(int i = 0 ; i  <actual.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

        [TestMethod()]
        public void ByteArrayValidationCheck()
        {
            //var pack = new DevicePacket()
            //{
            //    ID = new DeviceID()
            //    {
            //        ModulePart = 1,
            //        ParentPart = 1,
            //    },
            //    ModuleType = ModuleTypeEnum.TrainSensor,
            //};
            //var state = new TrainSensorData  ()
            //{
            //    Timer = 0xCCCC,
            //    Mode = TrainSensorMode.detecting,
            //    ThresholdVoltage = 1.1F,
            //    CurrentVoltage = 1.1F,
            //    IsDetected = 0x01,
            //};
            //pack.CopyToData<TrainSensorData>(state);

            //var arr = pack.ToByteArray();
            //var actual = arr.ToObject<DevicePacket>(0);

            //Assert.AreEqual(pack.ID, actual.ID);
            //Assert.AreEqual(pack.ModuleType, actual.ModuleType);
            //Assert.AreEqual(pack.ReadMark, actual.ReadMark);
            //for (int i = 0; i < pack.Data.Length; i++)
            //    Assert.AreEqual(pack.Data[i], actual.Data[i]);

            //var actualstate = new TrainSensorState(pack);
            //Assert.AreEqual(actualstate.DataState.Timer, state.Timer);
            //Assert.AreEqual(actualstate.DataState.Mode, state.Mode);
            //Assert.AreEqual(actualstate.DataState.ThresholdVoltage, state.ThresholdVoltage);
            //Assert.AreEqual(actualstate.DataState.CurrentVoltage, state.CurrentVoltage);
            //Assert.AreEqual(actualstate.DataState.IsDetected, state.IsDetected);

        }

    }
}
