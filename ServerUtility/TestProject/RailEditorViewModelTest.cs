using RouteVisualizer.RailEditor.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.Infrastructure;

using System.Data.Entity;
using RouteVisualizer.EF;
using Livet.Command;

namespace TestProject
{


    /// <summary>
    ///RailEditorViewModelTest のテスト クラスです。すべての
    ///RailEditorViewModelTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class RailEditorViewModelTest
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

        RailEditorViewModel sample
        {
            get
            {
                Database.DefaultConnectionFactory = new System.Data.Entity.Infrastructure.SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ModelingDatabase>());

                var context = new ModelingDatabase(string.Format("{0}\\test - {1}.sdf", this.TestContext.TestDir, DateTime.Now.ToString().Replace("/", "-").Replace(":", "-")));
                RailEditorViewModel target = new RailEditorViewModel()
                {
                    modeling = context,
                };
                return target;
            }
        }

        /// <summary>
        ///AddPathCommand のテスト
        ///</summary>
        [TestMethod()]
        public void AddPathCommandTest()
        {
            var target = sample;
            var path = new PathData()
            {
                GateStart = new GateData() { GateName = "start" },
                GateEnd = new GateData() { GateName = "end" },
            };

            Assert.IsTrue(target.AddPathCommand.CanExecute(path));

            target.AddPathCommand.Execute(path);

            Assert.IsFalse(target.AddPathCommand.CanExecute(path));
        }

        /// <summary>
        ///AddRailCommand のテスト
        ///</summary>
        [TestMethod()]
        public void AddRailCommandTest()
        {
            RailEditorViewModel target = sample;
            var rail = new RailData()
            {
                Manifacturer = "testman",
                RailName = "testrail",
                Gates = new [] { new GateData() { GateName = "A", }, new GateData() { GateName = "B" } },
            };

            Assert.IsTrue(target.AddRailCommand.CanExecute(rail));

            target.AddRailCommand.Execute(rail);

            Assert.IsFalse(target.AddRailCommand.CanExecute(rail));

        }

        /// <summary>
        ///RemoveRailCommand のテスト
        ///</summary>
        [TestMethod()]
        public void RemoveRailCommandTest()
        {
            RailEditorViewModel target = sample;
            var rail = new RailData()
            {
                Manifacturer = "testman",
                RailName = "removing",
            };

            Assert.IsTrue(target.AddRailCommand.CanExecute(rail));
            target.AddRailCommand.Execute(rail);

            Assert.IsTrue(target.RemoveRailCommand.CanExecute(rail));
            target.RemoveRailCommand.Execute(rail);

            Assert.IsFalse(target.RemoveRailCommand.CanExecute(rail));


        }

        /// <summary>
        ///RemovePathCommand のテスト
        ///</summary>
        [TestMethod()]
        public void RemovePathCommandTest()
        {
            RailEditorViewModel target = sample;
            var path  = new PathData()
            {
                GateStart = new GateData() { GateName = "removingA" },
                GateEnd = new GateData() { GateName = "removingB" },
            };

            Assert.IsTrue(target.AddPathCommand.CanExecute(path));
            target.AddPathCommand.Execute(path);

            target.SaveCommand.Execute();

            Assert.IsTrue(target.RemovePathCommand.CanExecute(path));
            target.RemovePathCommand.Execute(path);

            Assert.IsFalse(target.RemovePathCommand.CanExecute(path));
        }
    }
}
