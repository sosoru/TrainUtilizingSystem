using RouteVisualizer.RailEditor.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RouteVisualizer.EF;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace TestProject
{
    
    
    /// <summary>
    ///RailDataViewModelTest のテスト クラスです。すべての
    ///RailDataViewModelTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class RailDataViewModelTest
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

        public RailDataViewModel sample
        {
            get
            {
                var data = new RailData()
                {
                    Manifacturer = "test",
                    RailName = "R-999",
                    Pathes = new List<PathData>(),
                    Gates = new List<GateData>(),
                };
                var vm = new RailDataViewModel()
                {
                    Model = data,
                };

                return vm;
            }
        }

        [TestMethod()]
        public void pathvmsyncTest()
        {
            var vm = sample;

            var pathdata = new PathData();
            var pathvm = new PathDataViewModel()
            {
                Model = pathdata,
            };

            vm.pathvms.Add(pathvm);

            Assert.IsTrue(vm.Model.Pathes.Contains(pathdata));

            vm.pathvms.Remove(pathvm);

            Assert.IsFalse(vm.Model.Pathes.Contains(pathdata));
        }

        [TestMethod()]
        public void gatesyncTest()
        {
            var vm = sample;

            var pathdata = new PathData();
            var pathvm = new PathDataViewModel()
            {
                Model = pathdata,
            };

            vm.pathvms.Add(pathvm);

            var gatem = new GateData() { GateName = "testgate" };
            var gate = new GateDataViewModel { Model = gatem };
            vm.gates.Add(gate);

            Assert.IsTrue(pathvm.AvailableGates.Contains(gate));
        }

        [TestMethod()]
        public void gatesearchTest()
        {
            var stagate = new GateData { GateName = "gatestart" };
            var endgate =  new GateData { GateName = "gateend" };
            var pathdata = new PathData()
            {
                GateStart = stagate,
                GateEnd = endgate
            };
            var raildata = new RailData()
            {
                Pathes = new List<PathData>(),
                Gates = new List<GateData>(),
            };
            raildata.Pathes.Add(pathdata);

            var rvm = new RailDataViewModel()
            {
                Model = raildata,
            };

            Assert.IsTrue(rvm.gates.Count == 2);
            Assert.IsTrue(rvm.pathvms[0].AvailableGates.Count == 2);
        }
    }
}
