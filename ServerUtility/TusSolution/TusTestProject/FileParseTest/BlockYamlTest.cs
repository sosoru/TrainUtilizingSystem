using Tus.TransControl.Parser;
using Tus.TransControl.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Tus.TransControl;
using System.Linq;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace TestProject
{


    /// <summary>
    ///BlockYamlTest のテスト クラスです。すべての
    ///BlockYamlTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    [DeploymentItem("SampleLayout/route_test.yaml")]
    public class BlockYamlTest
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

        public static string PathSample
        {
            get
            {
                return @"815.yaml";
            }
        }

        public static string LoopSample
        {
            get { return @"route_test.yaml"; }
        }

        /// <summary>
        ///ParseFrom のテスト
        ///</summary>
        [TestMethod()]
        public void ParseFromTest()
        {
            BlockYaml target = new BlockYaml(); // TODO: 適切な値に初期化してください
            string path = PathSample; // TODO: 適切な値に初期化してください
            IEnumerable<object> expected = null; // TODO: 適切な値に初期化してください
            IEnumerable<object> actual;
            actual = target.ParseFrom(path);

            actual = actual;
        }

        /// <summary>
        ///ParseAbstract のテスト
        ///</summary>
        [TestMethod()]
        public void ParseAbstractTest()
        {
            BlockYaml target = new BlockYaml();
            IEnumerable<object> src = target.ParseFrom(PathSample).ToList();
        }

        /// <summary>
        ///Parse のテスト
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            BlockYaml target = new BlockYaml();
            IEnumerable<object> src = target.ParseFrom(LoopSample);
            //IEnumerable<Block> expected = ; 
            IEnumerable<BlockInfo> actual;
            actual = target.Parse(src).ToList();

            actual = actual;
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///BlockYaml コンストラクター のテスト
        ///</summary>
        [TestMethod()]
        public void BlockYamlConstructorTest()
        {
            BlockYaml target = new BlockYaml();
            Assert.Inconclusive("TODO: ターゲットを確認するためのコードを実装してください");
        }
    }
}
