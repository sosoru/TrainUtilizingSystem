﻿using SensorLibrary.DeviceStates.AvrDevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SensorLibrary.Packet;

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

        /// <summary>
        ///Voltage のテスト
        ///</summary>
        [TestMethod()]
        public void VoltageTest()
        {
            SensorState target = sample_state;

            target.Data.Voltage = 127;
            Assert.AreEqual(target.Voltage, 127.0f / 255.0f);

            target.Voltage = 0.3f;
            Assert.AreEqual(target.Data.Voltage, Math.Round( 0.3f * 255.0f));

            target.Voltage = 0;
            try
            {
                target.Voltage = -1;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e) { }

            target.Voltage = 1;
            try
            {
                target.Voltage = 1.1f;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e) { }

                
        }
    }
}
