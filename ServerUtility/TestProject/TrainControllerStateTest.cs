using SensorLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SensorLibrary;

namespace TestProject
{


    /// <summary>
    ///TrainControllerStateTest のテスト クラスです。すべての
    ///TrainControllerStateTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class TrainControllerStateTest
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

        public DevicePacket TestingPacket
        {
            get
            {
                var packet = new DevicePacket()
                {
                    ID = new DeviceID()
                    {
                        ParentPart = 1,
                        ModulePart = 1,
                    },
                    ModuleType = ModuleTypeEnum.TrainController,
                };

                return packet;
            }
        }


        /// <summary>
        ///Duty のテスト
        ///</summary>
        [TestMethod()]
        public void DutyTest()
        {
            TrainControllerState target = new TrainControllerState() { BasePacket = TestingPacket };
            target.Data.dutyEnabledBits = 10;
            int expected = 0xFFFF;
            int actual;
            target.Duty = expected;
            actual = target.Duty;
            Assert.AreNotEqual(expected, actual);
            Assert.AreEqual(0x3FF, actual);
        }

        /// <summary>
        ///DeviceFrequency のテスト
        ///</summary>
        [TestMethod()]
        public void DeviceFrequencyTest()
        {
            TrainControllerState target = new TrainControllerState() { BasePacket = TestingPacket };
            target.Data.frequency = 48;
            double actual;
            actual = target.DeviceFrequency;
            Assert.AreEqual(actual, 48 * 1000000);
        }

        /// <summary>
        ///DeviceRegisteredPeriod のテスト
        ///</summary>
        [TestMethod()]
        public void DeviceRegisteredPeriodTest()
        {
            TrainControllerState target = new TrainControllerState() { BasePacket = TestingPacket };
            byte expected = 16;
            byte actual;
            target.DeviceRegisteredPeriod = expected;
            actual = target.DeviceRegisteredPeriod;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///PreScale のテスト
        ///</summary>
        [TestMethod()]
        public void PreScaleTest()
        {
            TrainControllerState target = new TrainControllerState() { BasePacket = TestingPacket };
            try
            {
                target.PreScale = 0;
                Assert.Fail();
            }
            catch (InvalidOperationException) { }

            try
            {
                foreach (int i in new[] { 1, 4, 16, 17 })
                {
                    int expected = i;
                    int actual;
                    target.PreScale = expected;
                    actual = target.PreScale;
                    Assert.AreEqual(expected, actual);
                }
                Assert.Fail();
            }
            catch (InvalidOperationException) { }
        }

        /// <summary>
        ///DevicePeriod のテスト
        ///</summary>
        [TestMethod()]
        public void DevicePeriodTest()
        {
            TrainControllerState target = new TrainControllerState() { BasePacket = TestingPacket };
            byte exp = 44;
            target.Data.frequency = exp;
            double actual;
            actual = target.DevicePeriod;
            Assert.AreEqual(actual, 1.0 / target.DeviceFrequency);
        }

        /// <summary>
        ///EssentialDutyResolution のテスト
        ///</summary>
        [TestMethod()]
        public void EssentialDutyResolutionTest()
        {
            TrainControllerState target = new TrainControllerState() { BasePacket = TestingPacket };

            target.Data.frequency = 40;
            target.PreScale = 4;
            target.DeviceRegisteredPeriod = 0xFF;
            Assert.IsTrue(target.EssentialDutyResolution >= 10);

            target.Data.frequency = 40;
            target.PreScale = 1;
            target.DeviceRegisteredPeriod = 0x3F;
            Assert.IsTrue(target.EssentialDutyResolution >= 8);

        }

        /// <summary>
        ///IsSatisfiedDutyResolution のテスト
        ///</summary>
        [TestMethod()]
        public void IsSatisfiedDutyResolutionTest()
        {
            TrainControllerState target = new TrainControllerState() { BasePacket = TestingPacket };

            target.Data.dutyEnabledBits = 10;
            target.Data.frequency = 40;
            target.PreScale = 4;
            target.DeviceRegisteredPeriod = 0xFF;
            Assert.IsTrue(target.IsSatisfiedDutyResolution);

            target.Data.dutyEnabledBits = 10;
            target.Data.frequency = 40;
            target.PreScale = 1;
            target.DeviceRegisteredPeriod = 0x3F;
            Assert.IsTrue(!target.IsSatisfiedDutyResolution);
        }

        /// <summary>
        ///PWMFreqency のテスト
        ///</summary>
        [TestMethod()]
        public void PWMFreqencyTest()
        {
            TrainControllerState target = new TrainControllerState() { BasePacket = TestingPacket };

            target.Data.dutyEnabledBits = 10;
            target.Data.frequency = 40;
            target.PreScale = 4;
            target.DeviceRegisteredPeriod = 0xFF;
            
            Assert.IsTrue(target.PWMFreqency >= 9765 && target.PWMFreqency < 9774);

            target.Data.dutyEnabledBits = 10;
            target.Data.frequency = 40;
            target.PreScale = 1;
            target.DeviceRegisteredPeriod = 0x3F;

            Assert.IsTrue(target.PWMFreqency >= 156245 && target.PWMFreqency < 156254);
        }

        /// <summary>
        ///PWMPeriod のテスト
        ///</summary>
        [TestMethod()]
        public void PWMPeriodTest()
        {
            TrainControllerState target = new TrainControllerState() { BasePacket = TestingPacket };

            target.Data.dutyEnabledBits = 10;
            target.Data.frequency = 40;
            target.PreScale = 4;
            target.DeviceRegisteredPeriod = 0xFF;

            Assert.IsTrue(target.PWMPeriod == 1.0/target.PWMFreqency);
        }
    }
}
