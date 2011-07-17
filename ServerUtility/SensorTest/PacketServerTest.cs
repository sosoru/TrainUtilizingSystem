﻿using SerialPortTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace SensorTest
{
    
    
    /// <summary>
    ///PacketServerTest のテスト クラスです。すべての
    ///PacketServerTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class PacketServerTest
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

        public PacketServer GetTestServer()
        {
            var testContext = new DevicePacket[1];
            testContext[0] = new DevicePacket()
                {
                    ID = new DeviceID() { ParentPart = 1, ModulePart = 1 },
                    ModuleType = ModuleTypeEnum.TrainSensor,
                    Data = "test string"
                };

            var ms = new MemoryStream(256);
            for (int i = 0; i < 50; i++)
            {
                ms.WritePacket(testContext[0]);
            }

            ms.Seek(0, SeekOrigin.Begin);
            return new PacketServer(ms);
        }

        /// <summary>
        ///LoopStart のテスト
        ///</summary>
        [TestMethod()]
        public void LoopStartTest()
        {
            PacketServer target = GetTestServer();
            target.LoopStart();
            System.Threading.Thread.Sleep(10);
            Assert.IsTrue(target.IsLooping);
        }

        /// <summary>
        ///LoopStop のテスト
        ///</summary>
        [TestMethod()]
        public void LoopStopTest()
        {
            PacketServer target = GetTestServer();
            target.LoopStart();

            System.Threading.Thread.Sleep(10);
            target.LoopStop();

            int counter = 0;
            while (target.IsLooping)
            {
                System.Threading.Thread.Sleep(100);
                counter++;
                if (counter > 10)
                    Assert.Inconclusive("time out");
            }
        }

        /// <summary>
        ///AddAction のテスト
        ///</summary>
        [TestMethod()]
        public void AddActionTest()
        {
            PacketServer target = GetTestServer();
            bool test = false;
            var actual = target.AddAction((state)=> test = true);
            target.LoopStart();

            System.Threading.Thread.Sleep(1000);
            Assert.IsTrue(test);
        }
    }
}
