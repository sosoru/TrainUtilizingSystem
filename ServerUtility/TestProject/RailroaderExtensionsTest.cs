﻿using RouteVisualizer.Railroader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RailroaderIO;
using RouteVisualizer.Models;

namespace TestProject
{


    /// <summary>
    ///RailroaderExtensionsTest のテスト クラスです。すべての
    ///RailroaderExtensionsTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class RailroaderExtensionsTest
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

        private RailroaderMap examplemap
        {
            get
            {
                var map = new RailroaderMap("Sample2.rrf");
                return map;
            }
        }

        /// <summary>
        ///ToLayout のテスト
        ///</summary>
        [TestMethod()]
        public void ToLayoutTest()
        {
            RailroaderMap map = examplemap; 
            LayoutModel actual;
            actual = RailroaderExtensions.ToLayout(map);

        }
    }
}
