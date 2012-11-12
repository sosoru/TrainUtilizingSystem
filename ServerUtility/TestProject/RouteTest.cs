using RouteLibrary.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RouteLibrary.Parser;
using SensorLibrary.Packet.Control;
using SensorLibrary.Packet.Data;
using System.Linq;
using Moq;
using Moq.Linq;

namespace TestProject
{


    /// <summary>
    ///RouteTest のテスト クラスです。すべての
    ///RouteTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class RouteTest
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

        private BlockSheet test_sheet
        {
            get
            {
                var p = new BlockYaml();
                var infos = p.Parse(BlockYamlTest.LoopSample);

                var sheet = new BlockSheet(infos, new PacketServer(new SensorLibrary.Devices.AvrDeviceFactoryProvider()));

                return sheet;
            }
        }

        private IEnumerable<Block> test_blocks
        {
            get
            {
                var bstrs = new string[] { "T1", "B2", "T2", "T3", "T4", "B1", "T5", "T1" };
                var s = test_sheet;

                return bstrs.Select(name => s.InnerBlocks.First(b => b.Name == name));
            }
        }

        /// <summary>
        ///Route コンストラクター のテスト
        ///</summary>
        [TestMethod()]
        public void RouteConstructorTest()
        {
            Route target = new Route(test_blocks.ToList());
            Assert.IsTrue(!target.LockedBlocks.Any());
        }

        /// <summary>
        ///LockNextUnit のテスト
        ///</summary>
        [TestMethod()]
        public void LockNextUnitTest()
        {
            Route target = new Route(test_blocks.ToList());

            target.LockNextUnit();
            Assert.IsTrue(target.Units[0].Blocks.SequenceEqual(target.LockedBlocks));
            Assert.IsFalse(target.Units[0].CanBeAllocated);

        }

        /// <summary>
        ///ReleaseBeforeUnit のテスト
        ///</summary>
        [TestMethod()]
        public void ReleaseBeforeUnitTest()
        {
            Route target = new Route(test_blocks.ToList());

            target.LockNextUnit();
            target.LockNextUnit();
            target.ReleaseBeforeUnit();

            Assert.IsTrue(target.LockedBlocks.SequenceEqual(target.Units[1].Blocks));
            Assert.IsTrue(target.LockedBlocks.All(b => !b.IsBlocked));
        }

        //[TestMethod]
        //public void LookUpTrainTest()
        //{
        //    target.LookUpTrain();
        //    Assert.IsTrue(target.LockedBlocks.Count() == 0);

        //    var detectedmock = new Mock<SensorDetector>();
        //    detectedmock.Setup(e => e.IsDetected).Returns(true);
        //    var sensoredblock = blocks[3];
        //    sensoredblock.Detector = detectedmock.Object;

        //    target = new Route(blocks);
        //    target.LookUpTrain();
        //    Assert.IsTrue(target.LockedBlocks.Contains(sensoredblock));
        //}

        [TestMethod]
        public void AllocateBlockTest()
        {
            var blocks = test_blocks.ToArray();
            Route target = new Route(blocks);

            Block allocblk = blocks[3];
            target.AllocateTrain(allocblk, 1);
            Assert.IsTrue(target.LockedBlocks.Contains(allocblk));

            allocblk = blocks[2];
            target.AllocateTrain(allocblk, 2);
            Assert.IsTrue(target.LockedBlocks.Contains(allocblk));
            Assert.IsTrue(target.LockedUnits.Count() == 2);

            allocblk = blocks.Last();
            target.AllocateTrain(allocblk, 2);
            Assert.IsTrue(target.LockedBlocks.Contains(allocblk));

            try
            {
                allocblk = new Block(new BlockInfo(), blocks.First().Sheet);
                target.AllocateTrain(allocblk, 1);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException ex) { }

            allocblk = blocks.First();
            target.AllocateTrain(allocblk, 2);

        }
    }
}
