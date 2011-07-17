using SensorLivetView.ViewModels.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace TestProject
{
    
    
    /// <summary>
    ///UsbDeviceConverterTest のテスト クラスです。すべての
    ///UsbDeviceConverterTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class UsbDeviceConverterTest
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
        ///Convert のテスト
        ///</summary>
        [TestMethod()]
        public void ConvertTest()
        {
            UsbRegistryConverter target = new UsbRegistryConverter(); // TODO: 適切な値に初期化してください
            object value = null;
            Type targetType = null; // TODO: 適切な値に初期化してください
            object parameter = null; // TODO: 適切な値に初期化してください
            CultureInfo culture = null; // TODO: 適切な値に初期化してください
            //object expected = null; // TODO: 適切な値に初期化してください
            object actual;
            actual = target.Convert(value, targetType, parameter, culture);
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///ConvertBack のテスト
        ///</summary>
        [TestMethod()]
        public void ConvertBackTest()
        {
            UsbRegistryConverter target = new UsbRegistryConverter(); // TODO: 適切な値に初期化してください
            object value = null; // TODO: 適切な値に初期化してください
            Type targetType = null; // TODO: 適切な値に初期化してください
            object parameter = null; // TODO: 適切な値に初期化してください
            CultureInfo culture = null; // TODO: 適切な値に初期化してください
            //object expected = null; // TODO: 適切な値に初期化してください
            object actual;
            actual = target.ConvertBack(value, targetType, parameter, culture);
            //Assert.AreEqual(expected, actual);
        }
    }
}
