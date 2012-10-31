using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SensorLibrary.Packet.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size= 3)]
    public class PacketDeviceHeader
        : IPacketDeviceData
    {
        public byte DataLength { get; set; }
        public byte InternalAddr { get; set; }
        public byte ModuleType { get; set; }
    }

    // pdata:
	//	ControlMode : 1 byte
	//	Direction	: 1 byte
	//	DutyValue	: 1 byte
	//	VoltageValue: 1 byte
	//
	[StructLayout(LayoutKind.Sequential, Pack=1, Size=14)]
    public class MotorData
        : PacketDeviceHeader
    {
        public MotorData() { DataLength=14;  ModuleType = (byte)ModuleTypeEnum.AvrMotor; }

        public byte ControlMode;
        public byte Direction;
        public byte Duty;
        public byte Current;
        public byte ThresholdValue;
        public DeviceID DestinationID;
        public byte MemoryAfterentered;
        public byte DestinationMemory;
   }

    //struct PointModuleState
    //{
    //    uint8_t DeadTime;
    //    uint8_t ChangingTime;
    //    PositionEnum Position;
    //};
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 6)]
    public class SwitchData
        : PacketDeviceHeader
    {
        public SwitchData() { DataLength = 6; ModuleType = (byte)ModuleTypeEnum.AvrSwitch; }

        public byte DeadTime;
        public byte ChangingTime;
        public byte Position;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 6)]
    public class SensorData
        : PacketDeviceHeader 
    {
        public SensorData() { DataLength = 6; ModuleType = (byte)ModuleTypeEnum.AvrSensor; }

        public byte VoltageOn;
        public byte VoltageOff;
        public byte Threshold;

    }

    [StructLayout(LayoutKind.Sequential, Pack= 1, Size=4)]
    public class UsartSettingData
        : PacketDeviceHeader
    {
        public UsartSettingData() { DataLength = 4; ModuleType = (byte)ModuleTypeEnum.AvrUartSetting; }

        public byte ModuleCount;
    }

    [StructLayout(LayoutKind.Sequential, Pack=1, Size = 8)]
    public class KernelData
        : PacketDeviceHeader
    {
        public KernelData()
        {
            DataLength = 8;
            ModuleType = (byte)ModuleTypeEnum.AvrKernel;
        }

        public KernelCommand Command;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        private byte[] _data = new byte[4];
        public byte[] Content
        {
            get
            {
                if (_data == null)
                    _data = new byte[4];

                return _data;
            }
        }

    }

    public enum KernelCommand
        : byte
    {
        InquiryState = 0x01,
        MemoryState = 0x02,
    }
}
