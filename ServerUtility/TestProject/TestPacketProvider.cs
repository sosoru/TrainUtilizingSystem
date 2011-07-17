using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;

namespace TestProject
{
    public static class TestPacketProvider
    {
        public static DevicePacket TestPacket
        {
            get
            {
                var pack = new DevicePacket()
                {
                    ID = new DeviceID()
                    {
                        ParentPart = 0x01,
                        ModulePart = 0x01,
                    },
                    ModuleType = ModuleTypeEnum.TrainSensor,
                    ReadMark = 0xFF,
                };
                return pack;
            }
        }

        public static TrainSensorData TestTrainSensorData
        {
            get
            {
                var data = new TrainSensorData
                {
                    Mode = TrainSensorMode.detecting,
                    VoltageResolution = 10,
                    ReferenceVoltageMinus = 0,
                    ReferenceVoltagePlus = 5,

                    DeviceCurrentVoltage = 0x0200, // 2.5V
                    DeviceThresholdVoltage = 0x0100, // 1.25V
                    IsDetected = 1,

                    OverflowedCount = 0,
                    Timer = 0,
                };
                return data;
            }
        }

        public static TrainSensorState TestTrainSensorState
        {
            get
            {
                var pack = TestPacket;
                pack.ModuleType = ModuleTypeEnum.TrainSensor;
                var state = new TrainSensorState(pack, TestTrainSensorData, null);
                return state;
            }
        }

        public static MotherBoardData TestMotherBoardData
        {
            get
            {
                var data = new MotherBoardData()
                {
                    ParentId = 0x01,
                    Timer = 0,
                };
                return data;
            }
        }

        public static MotherBoardState TestMotherBoardState
        {
            get
            {
                var pack = TestPacket;
                pack.ModuleType = ModuleTypeEnum.MotherBoard;
                var state = new MotherBoardState(pack)
                {
                    ParentID = 0x01,
                    Timer = 0,
                };
                return state;
            }
        }

        public static PointModuleData TestPointModuleData
        {
            get
            {
                var data = new PointModuleData();
                return data;
            }
        }

        public static PointModuleState TestPointModuleState
        {
            get
            {
                var pack = TestPacket;
                pack.ModuleType = ModuleTypeEnum.PointModule;
                var state = new PointModuleState(pack);
                return state;
            }
        }

    }
}
