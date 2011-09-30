using SensorLivetView.ViewModels.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Livet.Command;
using Moq;
using Moq.Protected;

using SensorLivetView.Models;
using SensorLibrary;
using SensorLibrary.Manipulators;

namespace TestProject
{


    /// <summary>
    ///ManipulatorViewModelTest のテスト クラスです。すべての
    ///ManipulatorViewModelTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ManipulatorViewModelTest
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

        private Mock<TrainSensor> triggerMock;
        private Mock<TrainSensor> abotionMock;
        private Mock<TrainController> controllerMock;

        public ManipulatorViewModelTest()
        {
            var detectedState = new TrainSensorState()
            {
                BasePacket = new DevicePacket() { ID = new DeviceID() { ParentPart = 100, ModuleAddr = 2 } },
                Mode = TrainSensorMode.detecting,
                IsDetected = true,
            };

            var decliningState =  new TrainSensorState()
            {
                BasePacket = new DevicePacket() { ID = new DeviceID() { ParentPart = 100, ModuleAddr = 3 } },
                Mode = TrainSensorMode.detecting,
                IsDetected = false,
            };

            var abortState = new TrainSensorState()
            {
                BasePacket = new DevicePacket() { ID = new DeviceID() { ParentPart = 100, ModuleAddr = 3 } },
                Mode = TrainSensorMode.detecting,
                IsDetected = true,
            };

            this.triggerMock = new Mock<TrainSensor>();
            this.triggerMock.SetupGet((sens) => sens.CurrentState).Returns(detectedState);

            var callingCount = 0;
            this.abotionMock = new Mock<TrainSensor>();
            this.abotionMock.SetupGet((sens) => sens.CurrentState).Returns(() =>
                {
                    if (callingCount++ > 10)
                        return decliningState;
                    else
                        return abortState;
                });

            this.controllerMock = new Mock<TrainController>();
            this.controllerMock.Setup(cnt => cnt.SendPacket(It.IsAny<IDeviceState<IPacketDeviceData>>())).Callback((IDeviceState<IPacketDeviceData> state) =>
                {
                    this.controllerMock.SetupGet((cnt) => cnt.CurrentState).Returns(state as TrainControllerState);
                });

        }


        /// <summary>
        ///ExecuteCommand のテスト
        ///</summary>
        [TestMethod()]
        public void ControllerManipExecuteCommandTest()
        {
            ManipulatorViewModel target = new ManipulatorViewModel(); // TODO: 適切な値に初期化してください

            var manipulator = new ControllerManipulator()
            {
                TriggerSensor = this.triggerMock.Object,
                AbortingSensor = this.abotionMock.Object,
                TargetDevice = this.controllerMock.Object,
                Duration = new TimeSpan(0, 0, 5),
                To = 0.01,
            };

            var cntid = new DeviceID() { ParentPart = 100, ModuleAddr = 1 };
            var defaultdata = new TrainControllerData()
            {
                duty = 900,
                dutyEnabledBits = 10,
                frequency = 48,
            };
            var defaultstate = new TrainControllerState()
            {
                Data = defaultdata,
            };

            this.controllerMock.Object.DeviceID = cntid;
            this.controllerMock.Object.SendPacket(defaultstate);

            var model = new ControllerManipulatorModel()
            {
                Manipulator = manipulator,
            };

            target.Model = model;
            target.ExecuteCommand.Execute();

            while (target.IsExecuting) ;

        }
    }
}
