using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;
using SensorLibrary.Packet;

namespace SensorLibrary
{
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct DeviceID
        : IEquatable<DeviceID>
    {
        public DeviceID(ushort parent, byte module)
        {
            this.ParentPart = parent;
            this.ModulePart = 0;
            this.ModuleAddr = module;
        }

        public ushort ParentPart;
        //subnetaddr : 16
        public ushort ModulePart;
        //moduleaddr : 8
        //internaladdr : 8

        public byte ModuleAddr
        {
            get
            {
                return (byte)(this.ModulePart >> 8);
            }
            set
            {
                this.ModulePart &= 0x00ff;
                this.ModulePart |= (ushort)(value << 8);
            }
        }

        public byte InternalAddr
        {
            get
            {
                return (byte)(this.ModulePart & 0x00ff);
            }
            set
            {
                this.ModulePart &= 0xff00;
                this.ModulePart |= (ushort)(value);
            }
        }

        public bool IsMatched(int parent, int module, int inter)
        {
            return (this.ParentPart == parent || parent < 0)
                   && (this.ModuleAddr == module || module < 0)
                   && (this.InternalAddr == inter || inter < 0);
        }


        #region implementation of IEqualable
        public static bool operator ==(DeviceID A, DeviceID B)
        {
            if (ReferenceEquals(A, B))
                return true;
            else if ((object)A == null || (object)B == null)
                return false;
            else
                return (A.ParentPart == B.ParentPart && A.ModulePart == B.ModulePart);
        }
        public static bool operator !=(DeviceID A, DeviceID B) { return !(A == B); }

        public bool Equals(DeviceID other)
        {
            return (this == other);
        }

        public override int GetHashCode()
        {
            return this.ModulePart.GetHashCode() ^ this.ParentPart.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this == (DeviceID)obj;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        #endregion

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", this.ParentPart, this.ModuleAddr, this.InternalAddr);
        }

    }

    [StructLayout(LayoutKind.Sequential, Size = 32)]
    public class DevicePacket
    {
        public byte ReadMark = 0xFF;
        public DeviceID ID;
        public ModuleTypeEnum ModuleType;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        private byte[] _data = new byte [28];
        public byte [] Data
        {
            get
            {
                if (_data == null)
                    _data = new byte [28];

                return _data;
            }
        }

        public void CopyToData<T>(T obj)
            where T : IPacketDeviceData
        {
            obj.ToByteArray().CopyTo(this.Data, 0);
        }

        public T CopyFromData<T>()
            where T : IPacketDeviceData
        {
            return this.Data.ToObject<T>();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (this.Data == null)
                sb.Append("(Empty)");
            else
            {
                foreach (var b in Data)
                {
                    sb.Append(b.ToString("X2"));
                    sb.Append(" ");
                }
            }

            return string.Format("({0},{1}, {2}){3}", ID.ParentPart, ID.ModulePart, ModuleType.ToString(), sb.ToString());
        }
    }

    [StructLayout(LayoutKind.Sequential, Size = 73)]
    public class EthPacket
    {
        //DeviceID srcId; 
        //DeviceID destId; 

        //BYTE command; 
        //BYTE error; 

        //char message[ETH_MSG_LEN]; 
        //BYTE pdata[ETH_DATA_LEN]; 

        public DeviceID srcId;
        public DeviceID destId;

        byte command;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        private byte[] _data = new byte [64];
        public byte [] Data
        {
            get
            {
                return _data;
            }
        }

    }

    public interface IPacketDeviceData
    {

    }

    public enum EthCommandEnum
        : byte
    {

    }

    public enum EthErrorEnum
        : byte
    {

    }

    public enum ModuleTypeEnum
        : byte
    {
        MotherBoard = 0x00,
        TrainSensor = 0x01,
        PointModule = 0x02,
        TrainController = 0x03,
        RemoteModule = 0x04,

        AvrMotor = 0x12,
        AvrSwitch = 0x13,
        AvrSensor = 0x14,

        Unknown = 0x0F,
    }


}