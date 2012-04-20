using RouteVisualizer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using RouteVisualizer.Graph;

namespace TestProject
{


    /// <summary>
    ///LoopModelTest のテスト クラスです。すべての
    ///LoopModelTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class LoopModelTest
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

        public struct TestGateParams
        {
            public TestGate start;
            public TestGate end;
            public IEnumerable<TestGate> expected;
        }

        public class TestPath : IEdge<TestGate, TestPath>, IEquatable<TestPath>
        {
            public TestPath(TestGate prev,  TestGate next)
            {
                this.PreviousGate = prev;
                this.NextGate = next;
            }

            public double Length
            {
                get { throw new NotImplementedException(); }
            }

            public TestGate PreviousGate
            {
                get;
                set;
            }

            public TestGate NextGate
            {
                get;
                set;
            }

            public bool Equals(TestPath other)
            {
                return this.PreviousGate == other.PreviousGate && this.NextGate == other.NextGate;
            }
        }

        public class TestGate : IGate<TestGate, TestPath>
        {
            private IList<TestPath> _list = new List<TestPath>();
            static int g_id = 0;

            private int id;
            public TestGate()
            {
                this.id = g_id++;
            }

            public void TwowayConnect(IGate<TestGate, TestPath> gate)
            {
                _connect(this, gate);
                _connect(gate, this);
            }

            public void OnewayConnect(IGate<TestGate, TestPath> gate)
            {
                _connect(this, gate);
            }

            private static void _connect(IGate<TestGate, TestPath> by,  IGate<TestGate, TestPath> to)
            {
                var cby = by as TestGate;
                var cto = to as TestGate;

                var path = new TestPath(cby, cto);
                cby._list.Add(path);

            }

            public override int GetHashCode()
            {
                return this.id.GetHashCode();
            }

            public IEnumerator<TestPath> GetEnumerator()
            {
                foreach (var item in this._list)
                    yield return item;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        public TestGateParams straightRelation
        {
            get
            {
                var gates = new List<TestGate>();

                var firstgate = new TestGate();
                var middlegate = new TestGate();
                var lastgate = new TestGate();

                firstgate.TwowayConnect(middlegate);
                middlegate.TwowayConnect(lastgate);

                var exp = new [] { firstgate, middlegate, lastgate };

                return new TestGateParams() { start = firstgate, end = lastgate, expected = exp };
            }
        }

        public TestGateParams cloverRelation
        {
            get
            {
                var gates = new List<TestGate>();

                var crossGate = new TestGate();
                var rightHigherGate = new TestGate();
                var rightLowerGate = new TestGate();
                var leftHigherGate = new TestGate();
                var leftLowerGate = new TestGate();

                leftLowerGate.TwowayConnect(leftHigherGate);
                rightLowerGate.TwowayConnect(rightHigherGate);

                leftHigherGate.TwowayConnect(crossGate);
                leftLowerGate.TwowayConnect(crossGate);
                rightHigherGate.TwowayConnect(crossGate);
                rightLowerGate.TwowayConnect(crossGate);

                var start = leftLowerGate;
                var end = rightHigherGate;

                var exp  = new [] { leftLowerGate, crossGate, rightHigherGate };

                return new TestGateParams() { start = start, end = end, expected = exp };

            }
        }

        public TestGateParams loopRelation
        {
            get
            {
                var gates = new List<TestGate>();

                var firstgate = new TestGate();
                var middlegate = new TestGate();
                var lastgate = new TestGate();

                firstgate.TwowayConnect(middlegate);
                middlegate.TwowayConnect(lastgate);
                lastgate.TwowayConnect(firstgate);

                var exp = new [] { firstgate, lastgate };

                return new TestGateParams() { start = firstgate, end = lastgate, expected = exp };
            }
        }

        public TestGateParams OneWayRelation
        {
            get
            {
                var gates  = new List<TestGate>();

                var platformOne = new TestGate();
                var platformTwo = new TestGate();
                var rightPoint = new TestGate();
                var leftPoint = new TestGate();
                var mainroute = new TestGate();

                platformOne.OnewayConnect(mainroute);
                platformTwo.OnewayConnect(mainroute);
                rightPoint.OnewayConnect(platformOne);
                rightPoint.OnewayConnect(platformTwo);
                leftPoint.OnewayConnect(platformOne);
                leftPoint.OnewayConnect(platformTwo);
                mainroute.OnewayConnect(rightPoint);
                mainroute.OnewayConnect(leftPoint);

                var exp = new [] { platformOne, mainroute, rightPoint, platformTwo };

                return new TestGateParams { start = platformOne, end = platformTwo, expected = exp };
            }
        }

        /// <summary>
        ///SearchLoop のテスト
        ///</summary>
        public void _SearchLoopTest(TestGateParams prms)
        {
            var f = new RouteFactory<TestGate, TestPath>();

            var res = f.SearchLoop(prms.start, prms.end).Edges.ToList();

            var expgates = prms.expected.ToList();
            var exp = new List<TestPath>();
            for (int i=0; i < expgates.Count - 1; ++i)
            {
                exp.Add(new TestPath(expgates [i], expgates [i + 1]));
            }

            Assert.IsTrue(res.SequenceEqual(exp));
        }

        [TestMethod()]
        public void SearchLoopTest_straight()
        {
            _SearchLoopTest(straightRelation);
        }

        [TestMethod()]
        public void SearchLoopTest_loop()
        {
            _SearchLoopTest(loopRelation);
        }

        [TestMethod()]
        public void SearchLoopTest_clover()
        {
            _SearchLoopTest(cloverRelation);
        }
    }
}
