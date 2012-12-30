
using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace TestProject
{
    
    
    /// <summary>
    ///TusEthernetIOTest のテスト クラスです。すべての
    ///TusEthernetIOTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class TusEthernetIOTest
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

        [TestMethod()]
        public void TusDispatcherTest()
        {
            var io = new TusEthernetIO(new IPAddress(new byte[] { 192, 168, 2, 24 }),
                                        new IPAddress(new byte[] { 255, 255, 255, 0 }));
            var serv = new PacketServer() { Controller = io };
            var disp = new PacketDispatcher();

            serv.AddAction(disp);

            var mtr = new Motor(serv) { DeviceID = new DeviceID(24, 1, 1), };
            mtr.Observe(disp);
            
            //serv.LoopStart(System.Reactive.Concurrency.Scheduler.NewThread);
            var state = new MotorState();
            state.Direction = MotorDirection.Positive;
            state.Duty = 0.5f;
            state.ControlMode = MotorControlMode.DutySpecifiedMode;

            mtr.States.Add(MotorMemoryStateEnum.Controlling, state);
            mtr.CurrentMemory = MotorMemoryStateEnum.Controlling;
            mtr.SendState();

        }


    }
}
