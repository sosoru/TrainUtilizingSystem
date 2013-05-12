
using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TestProject
{


    /// <summary>
    ///PacketDispatcherSingleTest のテスト クラスです。すべての
    ///PacketDispatcherSingleTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class PacketDispatcherSingleTest
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
        ///Notify のテスト
        ///</summary>
        //[TestMethod()]
        //public void NotifyTest()
        //{
        //    PacketDispatcherSingle target = new PacketDispatcherSingle();
        //    var packet = new DevicePacket();
        //    var state = new MotherBoardState() { BasePacket = packet };
        //    target.Notify(state);
        //}

        /// <summary>
        ///AvailableDevices のテスト
        ///</summary>
        //[TestMethod()]
        //public void AvailableDevicesTest()
        //{
        //    PacketDispatcherSingle target = new PacketDispatcherSingle();
        //    var packet = new DevicePacket();
        //    var state = new MotherBoardState() { BasePacket = packet };
        //    state[0] = ModuleTypeEnum.MotherBoard;
        //    state[1] = ModuleTypeEnum.TrainSensor;
        //    state[2] = ModuleTypeEnum.PointModule;
        //    for (int i = 3; i < state.ModuleTypeLength; i++)
        //        state[i] = ModuleTypeEnum.Unknown;

        //    target.Notify(state);
            
        //    IEnumerable<IDevice<IDeviceState<IPacketDeviceData>>> actual;
        //    actual = target.FoundDeviceList;
        //    Assert.AreNotEqual(actual, null);
        //    Assert.AreEqual(actual.Count(), 3);
        //}

    }
}
