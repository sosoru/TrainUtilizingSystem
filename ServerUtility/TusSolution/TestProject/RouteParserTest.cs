using Tus.Route.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tus.Route;
using System.Collections.Generic;
using System.Linq;

namespace TestProject
{


    /// <summary>
    ///RouteParserTest のテスト クラスです。すべての
    ///RouteParserTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class RouteParserTest
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

        public BlockInfo[] blocks
        {
            get
            {
                var arr = new BlockInfo[]
                {
                    new BlockInfo() {Name = "pero0"},
                    new BlockInfo() { Name = "pero1" },
                    new BlockInfo() { Name = "pero2" },
                    new BlockInfo() {Name = "pero3"},
                    new BlockInfo() {Name = "pero4"},
                    new BlockInfo() {Name = "pero5"},
                    new BlockInfo() {Name = "pero6"},
                    new BlockInfo() {Name = "pero7"},
                };

                return arr;
            }
        }

        public string sample_context
        {
            get
            {
                return "pero1, pero2 <> pero3, pero4; pero1 < pero2; pero3 > pero4;";
            }
        }

        /// <summary>
        ///FromString のテスト
        ///</summary>
        [TestMethod()]
        public void FromStringTest1()
        {
            RouteLiteralParser target = new RouteLiteralParser() { ReferencedBlocks = blocks };
            string context = sample_context;

            IEnumerable<RouteSegmentInfo> expected =
                new RouteSegmentInfo[] {
                    new RouteSegmentInfo() { From = blocks[1], To = blocks[3]},
                    new RouteSegmentInfo() { From = blocks[1], To = blocks[4]},
                    new RouteSegmentInfo() { From = blocks[2], To = blocks[3]},
                    new RouteSegmentInfo() { From = blocks[2], To = blocks[4]},
                    new RouteSegmentInfo() { From = blocks[3], To = blocks[1]},
                    new RouteSegmentInfo() { From = blocks[3], To = blocks[2]},
                    new RouteSegmentInfo() { From = blocks[4], To = blocks[1]},
                    new RouteSegmentInfo() { From = blocks[4], To = blocks[2]},

                    new RouteSegmentInfo() { From = blocks[2], To = blocks[1]},
                    new RouteSegmentInfo() { From = blocks[3], To = blocks[4]},
                };

            IEnumerable<RouteSegmentInfo> actual;
            actual = target.FromString(context).ToList();

            var c = actual.Select(s => expected.FirstOrDefault(r => r.From.Name == s.From.Name && r.To.Name == s.To.Name))
                          .Where(s => s != null);

            Assert.IsTrue(c.Count() == expected.Count());
                                
        }
    }
}
