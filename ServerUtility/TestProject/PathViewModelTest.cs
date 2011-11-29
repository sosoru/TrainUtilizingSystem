using RouteVisualizer.ViewModels;
using RouteVisualizer.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RouteVisualizer.Models;
using RouteVisualizer.Railroader;
using System.Windows;
using System.Windows.Media;


using RailroaderIO;

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;

namespace TestProject
{
    
    
    /// <summary>
    ///PathViewModelTest のテスト クラスです。すべての
    ///PathViewModelTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class PathViewModelTest
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

        PathModel samplePathModel
        {
            get
            {
                var map = new RailroaderMap(@"C:\Users\root\Desktop\rail\cu.rrf");
                var l = map.ToLayout();

                return l.Rails.First().Pathes.First();
            }
        }            
               


        /// <summary>
        ///Bound のテスト
        ///</summary>
        [TestMethod()]
        public void BoundTest()
        {
            PathModel path = samplePathModel;
            PathViewModel target = new PathViewModel(path); 
            Rect actual;
            actual = target.Bound;
        }

        /// <summary>
        ///Angle のテスト
        ///</summary>
        [TestMethod()]
        public void AngleTest()
        {
            PathModel path = samplePathModel;
            PathViewModel target = new PathViewModel(path);
            double actual;
            actual = target.Angle;
        }

        /// <summary>
        ///CenterPosition のテスト
        ///</summary>
        [TestMethod()]
        public void CenterPositionTest()
        {
            PathModel path = samplePathModel;
            PathViewModel target = new PathViewModel(path);
            Point actual;
            actual = target.CenterPosition;
        }

        /// <summary>
        ///CurrentGeometry のテスト
        ///</summary>
        [TestMethod()]
        public void CurrentGeometryTest()
        {
            PathModel path = samplePathModel;
            PathViewModel target = new PathViewModel(path);
            Geometry actual;
            actual = target.CurrentGeometry;
        }

        /// <summary>
        ///IsStraight のテスト
        ///</summary>
        [TestMethod()]
        public void IsStraightTest()
        {
            PathModel path = samplePathModel;
            PathViewModel target = new PathViewModel(path);
            bool actual;
            actual = target.IsStraight;
        }

        /// <summary>
        ///Length のテスト
        ///</summary>
        [TestMethod()]
        public void LengthTest()
        {
            PathModel path = samplePathModel;
            PathViewModel target = new PathViewModel(path);
            double actual;
            actual = target.Length;
        }

        /// <summary>
        ///Radius のテスト
        ///</summary>
        [TestMethod()]
        public void RadiusTest()
        {
            PathModel path = samplePathModel;
            PathViewModel target = new PathViewModel(path);
            double actual;
            actual = target.Radius;
        }

        /// <summary>
        ///StartAngle のテスト
        ///</summary>
        [TestMethod()]
        public void StartAngleTest()
        {
            PathModel path = samplePathModel;
            PathViewModel target = new PathViewModel(path);
            double actual;
            actual = target.StartAngle;
        }
    }
}
