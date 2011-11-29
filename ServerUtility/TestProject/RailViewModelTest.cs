using RouteVisualizer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RouteVisualizer.Models;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Media;

using RailroaderIO;
using RouteVisualizer.Railroader;
using System.Linq;

using Livet;

namespace TestProject
{


    /// <summary>
    ///RailViewModelTest のテスト クラスです。すべての
    ///RailViewModelTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class RailViewModelTest
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

        LayoutModel sampleLayout
        {
            get
            {
                DispatcherHelper.UIDispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;

                var map = new RailroaderMap(@"C:\Users\root\Desktop\rail\cu.rrf");
                var l = map.ToLayout();

                return l;
            }
        }

        RailModel sampleModel
        {
            get
            {
                var map = new RailroaderMap(@"C:\Users\root\Desktop\rail\cu.rrf");
                var l = map.ToLayout();

                return l.Rails.First();
            }
        }

        /// <summary>
        ///LocateGate のテスト
        ///</summary>
        [TestMethod()]
        public void LocateGateTest()
        {
            RailModel model = sampleModel;
            LayoutViewModel lyout = new LayoutViewModel(sampleLayout);
            RailViewModel target = new RailViewModel(model, lyout);
            IDictionary<GateViewModel, Point> actual;
            actual = target.LocateGate();
        }

        /// <summary>
        ///Bound のテスト
        ///</summary>
        [TestMethod()]
        public void BoundTest()
        {
            RailModel model = sampleModel;
            LayoutViewModel lyout = new LayoutViewModel(sampleLayout);
            RailViewModel target = new RailViewModel(model, lyout);
            Rect actual;
            actual = target.Bound;
        }

        /// <summary>
        ///CurrentGeometry のテスト
        ///</summary>
        [TestMethod()]
        public void CurrentGeometryTest()
        {
            RailModel model = sampleModel;
            LayoutViewModel lyout = new LayoutViewModel(sampleLayout);
            RailViewModel target = new RailViewModel(model, lyout);
            Geometry actual;
            actual = target.CurrentGeometry;
        }

        /// <summary>
        ///IsMirrored のテスト
        ///</summary>
        [TestMethod()]
        public void IsMirroredTest()
        {
            RailModel model = sampleModel;
            LayoutViewModel lyout = new LayoutViewModel(sampleLayout);
            RailViewModel target = new RailViewModel(model, lyout);
            bool expected = false;
            bool actual;
            target.IsMirrored = expected;
            actual = target.IsMirrored;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///IsPathValidated のテスト
        ///</summary>
        [TestMethod()]
        public void IsPathValidatedTest()
        {
            RailModel model = sampleModel;
            LayoutViewModel lyout = new LayoutViewModel(sampleLayout);
            RailViewModel target = new RailViewModel(model, lyout);
            bool actual;
            actual = target.IsPathValidated;
        }
    }
}
