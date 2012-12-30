using Tus.Route;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using System.Linq;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;

namespace TestProject
{
    /// <summary>
    ///BlockTest のテスト クラスです。すべての
    ///BlockTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class BlockTest
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
                var sheet = new BlockSheet(infos, new PacketServer());

                sheet.Name = "test sheet";
                return sheet;
            }
        }


        /// <summary>
        ///Equals のテスト
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            var sheet = sample_sheet;
            var blocks = sheet.InnerBlocks;

            Block target = blocks.First();
            Block other = blocks.Last();
            var f = new Func<Block, bool>((a) => target.Equals(a));

            EqualTest.TestNotEqualNotDefault(other, f);

            target = blocks.First();
            other = target;
            EqualTest.TestEqualNotDefault(other, f);
        }

        /// <summary>
        ///Equals のテスト
        ///</summary>
        [TestMethod()]
        public void EqualsTest1()
        {
            var sheet = sample_sheet;
            var blocks = sheet.InnerBlocks;

            Block target = blocks.First();
            Block other = blocks.Last();
            var f = new Func<Block, bool>((a) => ((object)target).Equals((object)a));

            EqualTest.TestNotEqualNotDefault(other, f);

            target = blocks.First();
            other = target;
            EqualTest.TestEqualNotDefault(other, f);
        }

        /// <summary>
        ///GetHashCode のテスト
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest()
        {
            BlockSheet sheet = sample_sheet;
            Block target = sheet.InnerBlocks.First();
            int expected = target.Name.GetHashCode() ^ sheet.Name.GetHashCode();
            int actual;
            actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///op_Equality のテスト
        ///</summary>
        [TestMethod()]
        public void op_EqualityTest()
        {
            var s = sample_sheet;
            var f = new Func<Block, Block, bool>((a, b) => a == b);
            Block A = s.InnerBlocks.First();
            Block B = s.InnerBlocks.Last();
            EqualTest.TestNotEqualNotDefault(A, B, f);

            A = s.InnerBlocks.First();
            B = s.InnerBlocks.First();
            EqualTest.TestEqualNotDefault(A, B, f);
        }

        /// <summary>
        ///op_Inequality のテスト
        ///</summary>
        [TestMethod()]
        public void op_InequalityTest()
        {
            var s = sample_sheet;
            var f = new Func<Block, Block, bool>((a, b) => !(a != b));
            Block A = s.InnerBlocks.First();
            Block B = s.InnerBlocks.Last();
            EqualTest.TestNotEqualNotDefault(A, B, f);

            A = s.InnerBlocks.First();
            B = s.InnerBlocks.First();
            EqualTest.TestEqualNotDefault(A, B, f);
        }
    }
}
