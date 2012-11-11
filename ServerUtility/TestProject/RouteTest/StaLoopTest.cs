using RouteLibrary.Base;
using RouteLibrary.Parser;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Data;
using SensorLibrary.Packet.IO;
using SensorLibrary.Packet.Control;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Moq;
using Moq.Linq;
using SensorLibrary;
using System.Reactive.Subjects;

namespace TestProject
{
    /// <summary>
    /// StaLoopTest の概要の説明
    /// </summary>
    [TestClass]
    public class StaLoopTest
    {
        public StaLoopTest()
        {
            //
            // TODO: コンストラクター ロジックをここに追加します
            //
        }

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

        IEnumerable<BlockInfo> target_sheet
        {
            get
            {
                var yaml = new BlockYaml();
                var blocks = yaml.Parse("./SampleLayout/staloop.yaml");

                return blocks;
            }
        }

        Route GetRouteFirst(BlockSheet sht)
        {
            var route = new Route(sht, new[] { "AT2", "AT3", "AT4", "AT5", "AT6", "BAT6", "AT7", "AT8", "AT1", "BAT1" });
            return route;
        }

        #region 追加のテスト属性
        //
        // テストを作成する際には、次の追加属性を使用できます:
        //
        // クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 各テストを実行する前に、TestInitialize を使用してコードを実行してください
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ReadSheetTest()
        {
            var sht = target_sheet;

            target_sheet.ToArray();
        }

        [TestMethod]
        public void RouteConstructTest()
        {
            var serv = new PacketServer();
            var sht = new BlockSheet(target_sheet, serv);

            Route rt = GetRouteFirst(sht);
            var lockedunits = rt.LockedUnits.ToArray();
            Assert.IsTrue(lockedunits[0].Blocks.Select(b => b.Name)
                            .SequenceEqual(new[] { "AT2", "AT3", "AT4", "AT5", "AT6", "BAT6"}));
            Assert.IsTrue(lockedunits[1].Blocks.Select(b => b.Name)
                            .SequenceEqual(new[] { "AT7", "AT8", "AT1", "BAT1" }));

            
        }
    }
}
