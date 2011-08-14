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

        private int getmask(int from, int end)
        {
            int frommask = (1 << ((int)from)) - 1;
            int endmask = (1 << ((int)end + 1)) - 1;
            return (endmask & (~frommask));

        }

        private byte getval(int from, int end, byte val)
        {
            if (from > end)
                throw new ArgumentException("from > end");

            int ret = (val & getmask(from, end)) >> from;

            if (ret > 0xFF || ret < 0)
                throw new InvalidCastException("shift failed");

            return (byte)ret;
        }

        private int setval(int from, int end, int dest, int val)
        {
            if (from > end)
                throw new ArgumentException("from > end");

            if (val >= (1 << end))
                throw new ArgumentException("val > (1<<end)");

            int mask = getmask(from, end);
            dest &= ~mask;
            dest |= val << from;

            return dest;
        }

        public byte GlobalAddr
        {
            get
            {
                return getval(3, 7, this.ParentPart);
            }
            set
            {
                this.ParentPart = (byte)setval(3, 7, this.ParentPart, value);
            }
        }

        public byte InDeviceAddr
        {
            get
            {
                return getval(0, 2, this.ParentPart);
            }
            set
            {
                this.ParentPart = (byte)setval(0, 2, this.ParentPart, value);
            }
        }

        public bool RemoteBit
        {
            get
            {
                return getval(7, 7, this.ModulePart) != 0;
            }
            set
            {
                this.ModulePart = (byte)setval(7, 7, this.ModulePart, ((value) ? 1 : 0));
            }
        }

        public byte ModuleAddr
        {
            get
            {
                return (byte)getval(4, 6, this.ModulePart);
            }
            set
            {
                this.ModulePart = (byte)setval(4, 6, this.ModulePart, value);
            }
        }

        public byte InternalAddr
        {
            get
            {
                return (byte)getval(0, 3, this.ModulePart);
            }
            set
            {
                this.ModulePart = (byte)setval(0, 3, this.ModulePart, value);
            }
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
        RemoteModule = 0x04,

        Unknown = 0x0F,
    }


}