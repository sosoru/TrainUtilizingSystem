using SensorLivetView.ViewModels;
using SensorLivetView.ViewModels.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SensorLibrary;

namespace TestProject
{
    
    
    /// <summary>
    ///MotherBoardViewModelTest のテスト クラスです。すべての
    ///MotherBoardViewModelTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class MotherBoardViewModelTest
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
        ///MotherBoardViewModel コンストラクター のテスト
        ///</summary>
        [TestMethod()]
        public void MotherBoardViewModelConstructorTest()
        {
            MotherBoardViewModel target = new MotherBoardViewModel();
        }

        /// <summary>
        ///PortMappingEnumerable のテスト
        ///</summary>
        [TestMethod()]
        public void PortMappingEnumerableTest()
        {
            MotherBoardViewModel target = new MotherBoardViewModel();
            var state = TestPacketProvider.TestMotherBoardState;
            var dev = new MotherBoard(state.BasePacket.ID);
            target.Model = dev;
            dev.OnNext(state);

            IEnumerable<object> actual;
            actual = target.PortMappingEnumerable;
            Assert.IsFalse(actual == null);
        }
    }
}
