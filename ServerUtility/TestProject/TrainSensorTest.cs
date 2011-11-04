using SensorLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Linq;
using SensorLivetView;
using System.IO;

namespace TestProject
{
    
    
    /// <summary>
    ///TrainSensorTest のテスト クラスです。すべての
    ///TrainSensorTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class TrainSensorTest
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

        private TrainSensor testsensor
        {
            get
            {
                var id = new DeviceID() { ParentPart = 1, ModulePart =1};
                var packets = new TestEnumerable().SetTrainDetectingSensor(id);
                
                var st = new TestPacketStream(packets.ToEnumerable());
                var serv = new PacketServer(st);
                var disp = new PacketDispatcher();
                serv.LoopStart();
                serv.AddAction(disp);

                var tsens = new TrainSensor() { DeviceID = id };
                tsens.Observe(disp);
                System.Threading.Thread.Sleep(100);
                return tsens as TrainSensor ;
            }
        }



        /// <summary>
        ///CalculateSpeed のテスト
        ///</summary>
        [TestMethod()]
        public void CalculateSpeedTest()
        {
            var target = testsensor.ChangeDetectingMode();
            double leninterval = 10;
            double expected = 480000F; 
            double actual;

            System.Threading.Thread.Sleep(500);

            actual = target.CalculateSpeed(leninterval);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///GetSpeedChangedObservable のテスト
        ///</summary>
        [TestMethod()]
        public void GetSpeedChangedObservableTest()
        {
            var target = testsensor.ChangeDetectingMode();
            IObservable<TrainSensor> actual;
            actual = target.GetSpeedChangedObservable();

            try
            {
                bool state = false;
                actual.Timeout(new DateTimeOffset(DateTime.Now.Ticks, new TimeSpan(0, 0, 10))).Subscribe((i) => state = true);
            }
            catch { Assert.Fail(); }
        }
    }
}
