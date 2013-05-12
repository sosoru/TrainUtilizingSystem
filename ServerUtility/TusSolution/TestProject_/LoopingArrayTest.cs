using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Reactive.Linq;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;

namespace TestProject
{
    
    
    /// <summary>
    ///LoopingArrayTest のテスト クラスです。すべての
    ///LoopingArrayTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class LoopingArrayTest
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


        /// <summary>
        ///LoopingArray`1 コンストラクター のテスト
        ///</summary>
        public void LoopingArrayConstructorTestHelper<T>()
        {
            var target = new LoopingArray<T>(256);

            try
            {
                target = new LoopingArray<T>(-1);
                Assert.Fail("not thrown an argument exception");
            }
            catch (ArgumentException)
            { }
        }

        [TestMethod()]
        public void LoopingArrayConstructorTest()
        {
            LoopingArrayConstructorTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///GetEnumerator のテスト
        ///</summary>
        public void GetEnumeratorTestHelper<T>()
        {
            int count = 16; // TODO: 適切な値に初期化してください
            LoopingArray<T> target = new LoopingArray<T>(count); // TODO: 適切な値に初期化してください
            IEnumerator<T> actual;
            actual = target.GetEnumerator();
            Assert.AreNotEqual(actual, null);
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            GetEnumeratorTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///Push のテスト
        ///</summary>
        public void PushTestHelper<T>()
        {
            int count = 16; 
            LoopingArray<T> target = new LoopingArray<T>(count);
            T obj = default(T); 
            
            Observable.Range(0,32).Do((i)=>
                target.Push(obj)
            );
        }

        [TestMethod()]
        public void PushTest()
        {
            PushTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///System.Collections.IEnumerable.GetEnumerator のテスト
        ///</summary>
        public void GetEnumeratorTest1Helper<T>()
        {
            int count = 16; // TODO: 適切な値に初期化してください
            IEnumerable target = new LoopingArray<T>(count); // TODO: 適切な値に初期化してください
            IEnumerator actual = null;
            actual = target.GetEnumerator();
            Assert.AreNotEqual(null, actual);
        }

        [TestMethod()]
        [DeploymentItem("SensorLibrary.dll")]
        public void GetEnumeratorTest1()
        {
            GetEnumeratorTest1Helper<GenericParameterHelper>();
        }
    }
}
