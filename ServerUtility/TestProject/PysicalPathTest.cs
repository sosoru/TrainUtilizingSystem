using RouteVisualizer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;
using System.Windows.Media;
using RouteVisualizer.EF;
using RouteVisualizer.Models;
using System.Collections.Generic;

namespace TestProject
{
    
    
    /// <summary>
    ///PysicalPathTest のテスト クラスです。すべての
    ///PysicalPathTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class PysicalPathTest
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

        public PysicalPath samplepath
        {
            get
            {
                var railid = 1;
                var gateid = 2;
                var gateid2 = 3;
                var gateid3 = 5;
                var pathid = 4;
                var pathid2 = 6;

                var gateA = new GateData()
                {
                    GateName = "A",
                    RailID = railid,
                    ID = gateid,
                };

                var gateB = new GateData()
                {
                    GateName = "B",
                    RailID = railid,
                    ID = gateid2,
                };

                var gateC = new GateData()
                {
                    GateName = "C",
                    RailID = railid,
                    ID = gateid3,
                };

                var data = new RailData()
                {
                    ID = railid,
                    BottomGate = gateA,
                    RailName = "sample",
                    Manifacturer = "sampleManifactuer",
                    Gates = new List<GateData>(),
                    Pathes = new List<PathData>(),
                };

                var pathA = new PathData()
                {
                    ID = pathid,
                    GateStart = gateA,
                    GateEnd = gateB,
                    RailID = railid,
                    IsStraight = true,
                    StraightLength = 240,
                };

                var pathB = new PathData()
                {
                    ID = pathid2,
                    GateStart = gateA,
                    GateEnd = gateC,
                    RailID = railid,
                    IsStraight = false,
                    Angle = 15,
                    Radius = 340,
                };

                data.Gates.Add(gateA);
                data.Gates.Add(gateB);
                data.Gates.Add(gateC);

                data.Pathes.Add(pathA);
                data.Pathes.Add(pathB);
                data.BottomGate = gateA;

                var path = new PysicalPath()
                {
                    BaseData = pathA,
                    PreviousGate = new RailGate() { BaseData = gateA },
                    NextGate = new RailGate() { BaseData = gateB },
                };

                return path;
            }
        }


        /// <summary>
        ///Bound のテスト
        ///</summary>
        [TestMethod()]
        public void BoundTest()
        {
            var path = samplepath;
            var target = path.Bound;
        }

        /// <summary>
        ///CurrentGeometry のテスト
        ///</summary>
        [TestMethod()]
        public void CurrentGeometryTest()
        {
            var path = samplepath;
            var target = path.CurrentGeometry;
        }

        /// <summary>
        ///Length のテスト
        ///</summary>
        [TestMethod()]
        public void LengthTest()
        {
            var path = samplepath;

            var stlen = 10;
            path.BaseData.IsStraight = true;
            path.BaseData.StraightLength = stlen;
            Assert.IsTrue(path.Length == stlen);

            var cvlen = 10 * Math.PI;
            path.BaseData.IsStraight = false;
            path.BaseData.Radius = 10;
            path.BaseData.Angle = 180;
            Assert.IsTrue(path.Length == cvlen);
        }

        /// <summary>
        ///NextGate のテスト
        ///</summary>
        [TestMethod()]
        public void NextGateTest()
        {
            var path = samplepath;
            var gate = path.NextGate;

            path.NextGate = null;
            Assert.IsTrue(!gate.ConnectedPathes.Contains(path));

            path.NextGate = gate;
            Assert.IsTrue(gate.ConnectedPathes.Contains(path));
        }

        /// <summary>
        ///PreviousGate のテスト
        ///</summary>
        [TestMethod()]
        public void PreviousGateTest()
        {
            var path = samplepath;
            var gate = path.PreviousGate;

            path.PreviousGate = null;
            Assert.IsTrue(!gate.ConnectedPathes.Contains(path));

            path.PreviousGate = gate;
            Assert.IsTrue(gate.ConnectedPathes.Contains(path));
        }
    }
}
