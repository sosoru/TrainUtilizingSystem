using akiduki.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace TestProject1
{
    
    
    /// <summary>
    ///AkidukiDomainServiceTest のテスト クラスです。すべての
    ///AkidukiDomainServiceTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class AkidukiDomainServiceTest
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
        ///GetInfo のテスト
        ///</summary>
        // TODO: UrlToTest 属性が ASP.NET ページへの URL を指定していることを確認します (たとえば、
        // http://.../Default.aspx)。これはページ、Web サービス、または WCF サービスのいずれをテストする
        //場合でも、Web サーバー上で単体テストを実行するために必要です。
        [TestMethod()]
        public void GetInfoTest()
        {
            AkidukiDomainService target = new AkidukiDomainService(); // TODO: 適切な値に初期化してください
            string uri = @"http://akizukidenshi.com/catalog/g/gI-00097/"; // TODO: 適切な値に初期化してください
            //PartsInfo expected = null; // TODO: 適切な値に初期化してください
            PartsInfo actual;
            actual = target.GetInfo(uri);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }
    }
}
