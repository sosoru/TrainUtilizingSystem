using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;

namespace SensorLibrary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 28)]
    public class TrainSensorData
         : IPacketDeviceData
    {
        public TrainSensorMode Mode;
        public UInt16 Timer;
        public UInt16 OverflowedCount;
        public byte ReferenceVoltageMinus;
        public byte ReferenceVoltagePlus;
        public byte VoltageResolution;
        public ushort DeviceThresholdVoltage;
        public ushort DeviceCurrentVoltage;
        public byte IsDetected;


    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 28)]
    public class MotherBoardData
        : IPacketDeviceData
    {
        public byte ParentId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private byte[] _moduleType;
        public ushort Timer;

        public byte[] ModuleType
        {
            get
            {
                if (_moduleType == null)
                    _moduleType = new byte[2];
                return _moduleType;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 28)]
    public class PointModuleData
        : IPacketDeviceData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        private byte[] _directions;

        public byte[] Directions
        {
            get
            {
                if (this._directions == null)
                    this._directions = new byte[8];

                return this._directions;
            }
        }

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 28)]
    public class TrainControllerData
        : IPacketDeviceData
    {
        public ushort duty;
        public byte dutyEnabledBits;

        public byte period;
        public TrainControllerPrescale prescale;
        public byte frequency;
        public TrainControllerDirection direction;
        public TrainControllerMode mode;
        public ushort voltage;
        public byte voltageEnabledBits;
        public ushort meisuredVoltage;

        public float paramp;
        public float parami;
        //public float paramd;

        public ushort meisuredVoltage2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 28)]
    public class RemoteModuleData
        : IPacketDeviceData
    {
        public DeviceID remoteid;
    }

    public struct PidParams
    {
        public float paramp;
        public float parami;
        public float paramd;
    }

    public enum TrainControllerMode
        : byte
    {
        Duty = 0,
        Following = 1,
        OnDevice = 2,
    }

    public enum TrainControllerDirection
        : byte
    {
        Positive = 0,
        Negative = 1,
    }

    public enum TrainControllerPrescale
        : byte
    {
        PS_1_1 = 252,
        PS_1_4 = 253,
        PS_1_16 = 254,
    }

    public enum PointStateEnum
        : byte
    {
        Straight = 0x00,
        Curve = 0x01,
        Any = 0xFF,
    }

    public enum TrainSensorMode
        : byte
    {
        meisuring = 0x01,
        detecting = 0x02,
    }


}
