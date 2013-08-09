using Tus.TransControl.Parser;
using Tus.TransControl;
using Tus.TransControl.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Moq;
using Moq.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Reactive.Concurrency;
using Microsoft.Reactive.Testing;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace TestProject
{


    /// <summary>
    ///RouteTest のテスト クラスです。すべての
    ///RouteTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    [DeploymentItem("SampleLayout/route_test.yaml")]
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

                var sheet = new BlockSheet(infos, new PacketServer());

                return sheet;
            }
        }

        private IEnumerable<Block> test_blocks
        {
            get
            {
                var bstrs = new string[] { "T2", "T3", "T4", "B1", "T5", "T7", "B3", "T1", "B2", };
                var s = test_sheet;

                return bstrs.Select(name => s.InnerBlocks.First(b => b.Name == name));
            }
        }

        /// <summary>
        ///AssociatedRoute コンストラクター のテスト
        ///</summary>
        [TestMethod()]
        public void RouteConstructorTest()
        {
            var target = new Route(new RouteOrder(test_blocks.ToArray()));
            Assert.IsTrue(!target.LockedBlocks.Any());
        }

        /// <summary>
        ///LockNextUnit のテスト
        ///</summary>
        [TestMethod()]
        public void LockNextUnitTest()
        {
            var target = new Route(new RouteOrder(test_blocks.ToArray()));

            target.AllocateTrain(test_blocks.First(), 1);
            Assert.IsTrue(target.RouteOrder.Units[0].Blocks.SequenceEqual(target.LockedBlocks));
            Assert.IsFalse(target.RouteOrder.Units[0].CanBeAllocated);

        }

        /// <summary>
        ///ReleaseLastUnit のテスト
        ///</summary>
        [TestMethod()]
        public void ReleaseBeforeUnitTest()
        {
            var target = new Route(new RouteOrder(test_blocks.ToArray()));

            target.AllocateTrain(test_blocks.First(), 1);
            target.LockNextUnit();
            target.ReleaseLastUnit();

            Assert.IsTrue(target.LockedBlocks.SequenceEqual(target.RouteOrder.Units[1].Blocks));
            Assert.IsTrue(target.RouteOrder.Units[0].Blocks.All(b => !b.IsLocked));
        }

        [TestMethod]
        public void GetNeighborUnitTest()
        {
            var target = new Route(new RouteOrder(test_blocks.ToArray()));

            target.AllocateTrain(test_sheet.GetBlock("T7"), 1);

            var before = target.HeadContainer.GetNeighbor(-1);
            var next = target.HeadContainer.GetNeighbor(1);

            Assert.IsTrue(before.Unit.ControlBlock.Name == "T4");
            Assert.IsTrue(next.Unit.ControlBlock.Name == "T1");
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

        //    target = new AssociatedRoute(blocks);
        //    target.LookUpTrain();
        //    Assert.IsTrue(target.LockedBlocks.Contains(sensoredblock));
        //}

        [TestMethod]
        public void ReservingTest()
        {
            var blocks = test_blocks.Where(b => b.HasMotor).ToArray();
            var target = new Route(new RouteOrder(test_blocks.ToArray()));
            target.RouteOrder.IsRepeatable = true;

            target.AllocateTrain(blocks[0], 1);
            target.ReserveHead();
            Assert.AreEqual(target.ReservedUnit.Unit.ControlBlock.Name, blocks[1].Name);
            Assert.AreEqual(target.LockedUnits.Count(), 2);
            Assert.AreEqual(target.LockedUnits.Any(b => b.ControlBlock.Name == blocks[0].Name), true);
            Assert.AreEqual(target.LockedUnits.Any(b => b.ControlBlock.Name == blocks[1].Name), true);

            target.LockNextUnit();
            Assert.AreEqual(target.ReservedUnit.Unit.ControlBlock.Name, blocks[2].Name);
            Assert.AreEqual(target.LockedUnits.Count(), 3);
            Assert.AreEqual(target.LockedUnits.Any(b => b.ControlBlock.Name == blocks[0].Name), true);
            Assert.AreEqual(target.LockedUnits.Any(b => b.ControlBlock.Name == blocks[1].Name), true);
            Assert.AreEqual(target.LockedUnits.Any(b => b.ControlBlock.Name == blocks[2].Name), true);

        }

        [TestMethod]
        public void AllocateBlockTest()
        {
            var blocks = test_blocks.ToArray();
            var target = new Route(new RouteOrder(test_blocks.ToArray()));
            target.RouteOrder.IsRepeatable = true;

            // allocate single block
            Block allocblk = blocks[1];
            target.AllocateTrain(allocblk, 1);
            Assert.IsTrue(target.LockedBlocks.Contains(allocblk));

            // allocate double block
            allocblk = blocks[4];
            target.AllocateTrain(allocblk, 2);
            Assert.IsTrue(target.LockedBlocks.Contains(allocblk));
            Assert.IsTrue(target.LockedUnits.Count() == 2);

            // should repeat blocks successfully
            allocblk = blocks.Last();
            target.AllocateTrain(allocblk, 2);
            Assert.IsTrue(target.LockedBlocks.Contains(allocblk));

            try
            {
                // should fail if index is over
                allocblk = new Block(new BlockInfo(), blocks.First().Sheet);
                target.AllocateTrain(allocblk, 1);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException ex) { }

            allocblk = blocks.First();
            target.AllocateTrain(allocblk, 2);

        }

        [TestMethod]
        public void RepeatableBlockTest()
        {
            var blocks = test_blocks.ToArray();

            var target = new Route(new RouteOrder(test_blocks.ToArray()));
            target.RouteOrder.IsRepeatable = true;

            target.AllocateTrain(test_blocks.First(), 1);
            target.LockNextUnit();
            Observable.Range(0, 5)
                .Subscribe(i =>
                {
                    Assert.IsTrue(target.LockNextUnit());
                    Assert.IsTrue(target.ReleaseLastUnit());

                });
        }

        [TestMethod]
        public void ValidateRouteSegmentWhenRepeating()
        {
            var blocks = test_blocks.ToArray();
            var target = new Route(new RouteOrder(test_blocks.ToArray()));
            target.RouteOrder.IsRepeatable = true;
        }

        //[TestMethod]
        //public void TryLockNeighborTest()
        //{
        //    var blocks = test_blocks.ToArray();
        //    var target = new Route(new RouteOrder(test_blocks.ToArray()));
        //    ControlUnit unit;

        //    target.LockNextUnit();

        //    var first = target.RouteOrder.Units.First();
        //    var second = target.RouteOrder.Units.ToArray()[1];

        //    target.TryLockNeighborUnit(1, out unit);
        //    Assert.IsTrue(unit.ControlBlock.Name == second.ControlBlock.Name);

        //}
    }
}
