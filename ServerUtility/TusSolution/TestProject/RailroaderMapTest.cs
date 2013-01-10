using RailroaderIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    
    
    /// <summary>
    ///RailroaderMapTest のテスト クラスです。すべての
    ///RailroaderMapTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class RailroaderMapTest
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

        string path = @"Sample2.rrf";

        /// <summary>
        ///RailroaderMap コンストラクター のテスト
        ///</summary>
        [TestMethod()]
        public void RailroaderMapConstructorTest()
        {
            string path = this.path ;
            RailroaderMap target = new RailroaderMap(path);
            
        }

        /// <summary>
        ///LayoutWidth のテスト
        ///</summary>
        [TestMethod()]
        public void LayoutWidthTest()
        {
            string path = this.path;
            RailroaderMap target = new RailroaderMap(path);

            int actual = 2200;
            actual = target.LayoutWidth;

            Assert.AreNotEqual(0, actual);
        }

        /// <summary>
        ///LayoutHeight のテスト
        ///</summary>
        [TestMethod()]
        public void LayoutHeightTest()
        {
            string path = this.path;
            RailroaderMap target = new RailroaderMap(path);
            int actual = 1000;
            actual = target.LayoutHeight;

            Assert.AreNotEqual(0, actual);
        }
    }
}
