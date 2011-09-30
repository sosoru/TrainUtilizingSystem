using SensorLivetView.ViewModels.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using System.Collections.Generic;

namespace TestProject
{
    
    
    /// <summary>
    ///UsbDevicesViewModelTest のテスト クラスです。すべての
    ///UsbDevicesViewModelTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class UsbDevicesViewModelTest
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
        ///UsbDevicesViewModel コンストラクター のテスト
        ///</summary>
        [TestMethod()]
        public void UsbDevicesViewModelConstructorTest()
        {
            UsbDevicesViewModel target = new UsbDevicesViewModel();
        }

        /// <summary>
        ///SelectedDevice のテスト
        ///</summary>
        [TestMethod()]
        public void SelectedDeviceTest()
        {
            UsbDevicesViewModel target = new UsbDevicesViewModel(); // TODO: 適切な値に初期化してください
            UsbRegistry expected = null; // TODO: 適切な値に初期化してください
            UsbRegistry actual;
            target.SelectedDeviceReg = expected;
            actual = target.SelectedDeviceReg;
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///DeviceCandicates のテスト
        ///</summary>
        //[TestMethod()]
        //public void DeviceCandicatesTest()
        //{
        //    UsbDevicesViewModel target = new UsbDevicesViewModel(); // TODO: 適切な値に初期化してください
        //    IEnumerable<UsbRegistry> actual;
        //    actual = target.DeviceCandicates;
        //}
    }
}
