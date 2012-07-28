using RouteLibrary.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestProject
{


    /// <summary>
    ///BlockSheetTest のテスト クラスです。すべての
    ///BlockSheetTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class BlockSheetTest
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

        BlockSheet sample_sheet
        {
            get
            {
                var infos = new[] { new BlockInfo { Name = "pero" }, new BlockInfo { Name = "hoge" } };
                var sheet = new BlockSheet(infos, null);

                sheet.Name = "pero";
                return sheet;
            }
        }

        /// <summary>
        ///Equals のテスト
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            BlockSheet target = sample_sheet;
            BlockSheet other = sample_sheet;
            target.Name = "test1";
            other.Name = "test2";

            var f = new Func<BlockSheet, bool>((o) => target.Equals(o));

            EqualTest.TestNotEqualNotDefault(other, f);

            other.Name = target.Name;
            EqualTest.TestEqualNotDefault(other, f);
        }

        /// <summary>
        ///Equals のテスト
        ///</summary>
        [TestMethod()]
        public void EqualsTest1()
        {
            BlockSheet target = sample_sheet;
            BlockSheet other = sample_sheet;
            target.Name = "test1";
            other.Name = "test2";

            var f = new Func<BlockSheet, bool>((o) => ((object)target).Equals((object)o));

            EqualTest.TestNotEqualNotDefault(other, f);

            other.Name = target.Name;
            EqualTest.TestEqualNotDefault(other, f);
        }

        /// <summary>
        ///GetHashCode のテスト
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest()
        {
            BlockSheet target = sample_sheet;
            BlockSheet other = sample_sheet;
            int expected = target.Name.GetHashCode();
            int actual;
            actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///op_Inequality のテスト
        ///</summary>
        [TestMethod()]
        public void op_InequalityTest()
        {
            BlockSheet A = sample_sheet;
            BlockSheet B = sample_sheet;
            var f = new Func<BlockSheet, BlockSheet, bool>((a, b) => !(a != b));

            EqualTest.TestEqualNotDefault(A, B, f);
        }

        /// <summary>
        ///op_Equality のテスト
        ///</summary>
        [TestMethod()]
        public void op_EqualityTest()
        {
            BlockSheet A = sample_sheet;
            BlockSheet B = sample_sheet;
            var f = new Func<BlockSheet, BlockSheet, bool>((a, b) => (a == b));

            EqualTest.TestEqualNotDefault(A, B, f);
        }
    }
}
