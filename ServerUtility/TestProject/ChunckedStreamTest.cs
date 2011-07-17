using SensorLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace TestProject
{
    
    
    /// <summary>
    ///ChunckedStreamTest のテスト クラスです。すべての
    ///ChunckedStreamTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ChunckedStreamTest
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

        private void WriteFF(ChunckedStreamController st, int count )
        {
            var buf = Enumerable.Repeat<byte>(0xFF, count).ToArray();
            st.Write(buf, 0, count);
            st.Flush();
        }

        public byte[] SampleArray(int count )
        {
            var rnd = new Random();
            var buf = new byte[count];
            rnd.NextBytes(buf);
            return buf;
        }

        public bool CompareByteArray(IList<byte> A, IList<byte> B)
        {
            for(int i = 0 ; i < A.Count; i++)
            {
                if (A[i] != B[i])
                    return false;
            }
            return true;
        }


        /// <summary>
        ///Flush のテスト
        ///</summary>
        [TestMethod()]
        public void FlushTest()
        {
            var testbuf = new byte[32];
            using (var ms = new MemoryStream(testbuf))
            using(var target = new ChunckedStreamController(ms, 16))
            {
                target.EmptyData = 0x01;
                WriteFF(target, 16);

                target.Flush();
            }
            var expected = Enumerable.Repeat<byte>(0xFF, 16).Concat(Enumerable.Repeat<byte>(0x01, 16)).ToArray();

            Assert.IsTrue(CompareByteArray(testbuf, expected));
        }

        /// <summary>
        ///Read のテスト
        ///</summary>
        [TestMethod()]
        public void ReadTest()
        {
            var testbuf = SampleArray(64);
            var actual = new byte[64];
            using (var ms = new MemoryStream(testbuf))
            using (var target = new ChunckedStreamController(ms, 32))
            {
                for (int i = 0; i < actual.Length; i++)
                {
                    var readed = target.Read(actual, i, 1);
                    Assert.IsTrue(readed == 1);
                }
            }
            Assert.IsTrue(CompareByteArray(actual, testbuf));
        }


        /// <summary>
        ///Write のテスト
        ///</summary>
        [TestMethod()]
        public void WriteTest()
        {
            var testbuf = SampleArray(64);
            var actual = new byte[64];
            using (var ms = new MemoryStream(actual))
            using (var target = new ChunckedStreamController(ms, 32))
            {
                for (int i = 0; i < actual.Length; i++)
                {
                    target.Write(testbuf, i, 1);
                }
                target.Flush();
            }
            Assert.IsTrue(CompareByteArray(actual, testbuf));
        }

    }
}
