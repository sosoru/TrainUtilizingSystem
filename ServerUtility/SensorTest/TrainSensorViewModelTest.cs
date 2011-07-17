using SensorViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SensorLibrary;
using System.Windows.Input;

namespace SensorTest
{
    
    
    /// <summary>
    ///TrainSensorViewModelTest のテスト クラスです。すべての
    ///TrainSensorViewModelTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class TrainSensorViewModelTest
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
        ///ChangeMeisuringModeComand のテスト
        ///</summary>
        [TestMethod()]
        public void ChangeMeisuringModeComandTest()
        {
            TrainSensor cens = null; // TODO: 適切な値に初期化してください
            TrainSensorViewModel target = new TrainSensorViewModel(cens); // TODO: 適切な値に初期化してください
            ICommand actual;
            actual = target.ChangeMeisuringModeComand;
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        /// <summary>
        ///ChangeDetectingModeCommand のテスト
        ///</summary>
        [TestMethod()]
        public void ChangeDetectingModeCommandTest()
        {
            TrainSensor cens = null; // TODO: 適切な値に初期化してください
            TrainSensorViewModel target = new TrainSensorViewModel(cens); // TODO: 適切な値に初期化してください
            ICommand actual;
            actual = target.ChangeDetectingModeCommand;
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        /// <summary>
        ///TrainSensorViewModel コンストラクター のテスト
        ///</summary>
        [TestMethod()]
        public void TrainSensorViewModelConstructorTest()
        {
            TrainSensor cens = null; // TODO: 適切な値に初期化してください
            TrainSensorViewModel target = new TrainSensorViewModel(cens);
            Assert.Inconclusive("TODO: ターゲットを確認するためのコードを実装してください");
        }
    }
}
