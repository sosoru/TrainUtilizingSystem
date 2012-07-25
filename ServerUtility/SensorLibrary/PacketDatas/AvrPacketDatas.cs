using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SensorLibrary.Packet
{
    // pdata:
	//	ControlMode : 1 byte
	//	Direction	: 1 byte
	//	DutyValue	: 1 byte
	//	VoltageValue: 4 byte
	//	
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 28)]
    public class MotorData
        : IPacketDeviceData
    {
        public byte ControlMode;
        public byte Direction;
        public byte Duty;
        public byte Current;
    }

    //struct PointModuleState
    //{
    //    uint8_t DeadTime;
    //    uint8_t ChangingTime;
    //    PositionEnum Position;
    //};
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 28)]
    public class SwitchData
        : IPacketDeviceData
    {
        public byte DeadTime;
        public byte ChangingTime;
        public byte Position;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 28)]
    public class SensorData
        : IPacketDeviceData
    {
        public byte Voltage;
    }
}
