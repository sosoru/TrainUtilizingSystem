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
    ///GateViewModelTest のテスト クラスです。すべての
    ///GateViewModelTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class GateViewModelTest
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

        GateModel sampleGate
        {
            get
            {
                var map = new RailroaderMap(@"C:\Users\root\Desktop\rail\cu.rrf");
                var l = map.ToLayout();

                return l.Rails.First().Pathes.First().PreviousGate;
            }
        }


        /// <summary>
        ///Bound のテスト
        ///</summary>
        [TestMethod()]
        public void BoundTest()
        {
            GateModel model = sampleGate;
            GateViewModel target = new GateViewModel(model);
            Rect actual;
            actual = target.Bound;
        }

        /// <summary>
        ///CurrentGeometry のテスト
        ///</summary>
        [TestMethod()]
        public void CurrentGeometryTest()
        {
            GateModel model = sampleGate;
            GateViewModel target = new GateViewModel(model);
            Geometry actual;
            actual = target.CurrentGeometry;
        }

        /// <summary>
        ///Name のテスト
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            GateModel model = sampleGate;
            GateViewModel target = new GateViewModel(model);
            string actual;
            actual = target.Name;
        }

        /// <summary>
        ///Position のテスト
        ///</summary>
        [TestMethod()]
        public void PositionTest()
        {
            GateModel model = sampleGate;
            GateViewModel target = new GateViewModel(model);
            Point actual;
            actual = target.Position;
        }
    }
}
