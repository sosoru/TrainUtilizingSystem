using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;

namespace SensorLibrary
{
    [StructLayout(LayoutKind.Sequential, Size = 2)]
    public struct DeviceID
        : IEquatable<DeviceID>
    {
        public DeviceID(byte parent, byte module)
        {
            this.ParentPart = parent;
            this.ModulePart = module;
        }

        public byte ParentPart;
        public byte ModulePart;

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

    }

    [StructLayout(LayoutKind.Sequential, Size = 32)]
    public class DevicePacket
    {
        public byte ReadMark = 0xFF;
        public DeviceID ID;
        public ModuleTypeEnum ModuleType;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        private byte[] _data = new byte[28];
        public byte[] Data
        {
            get
            {
                if (_data == null)
                    _data = new byte[28];

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
            return this.Data.ToObject<T>(0);
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

    public interface IPacketDeviceData
    {

    }

    public enum ModuleTypeEnum
        : byte
    {
        MotherBoard = 0x00,
        TrainSensor = 0x01,
        PointModule = 0x02,
        TrainController = 0x03,

        Unknown = 0x0F,
    }


}