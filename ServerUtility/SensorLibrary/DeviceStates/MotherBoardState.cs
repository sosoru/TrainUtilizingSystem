using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace SensorLibrary
{
    public class MotherBoardState
    : DeviceState<MotherBoardData>
    {
        public MotherBoardState(DevicePacket packet, MotherBoardData data, PacketServer server)
            : base(packet, data, server)
        {
            if (packet.ModuleType != ModuleTypeEnum.MotherBoard)
                throw new ArgumentException("invalid module type");
        }

        public MotherBoardState(DevicePacket packet)
            : this(packet, null, null)
        { }

        public ushort Timer
        {
            get { return this.Data.Timer; }
            set { this.Data.Timer = value; }
        }

        public ModuleTypeEnum GetModuleType(int index)
        {
            int arindex = index / 2;
            int shift = (index % 2) * 4;
            return (ModuleTypeEnum)((this.Data.ModuleType [arindex] & (0x0F << shift)) >> shift);
        }

        public void SetModuleType(int index, ModuleTypeEnum value)
        {
            int arindex = index / 2;
            int shift = (index % 2) * 4;
            this.Data.ModuleType [arindex] &= (byte)(~(0x0F << shift));
            this.Data.ModuleType [arindex] |= (byte)((int)value << shift);
        }

        public int ModuleTypeLength
        {
            get { return this.Data.ModuleType.Length * 2; }
        }

        public byte ParentID
        {
            get { return this.Data.ParentId; }
            set { this.Data.ParentId = value; }
        }

        public ModuleTypeEnum this [int addr]
        {
            get
            {
                return GetModuleType(addr);
            }
            set
            {
                SetModuleType(addr, value);
            }
        }
    }

    public class MotherBoardStateComparer
        : IEqualityComparer<MotherBoardState>
    {

        public bool Equals(MotherBoardState x, MotherBoardState y)
        {
            if (x == null || y == null)
                return false;
            else if (x == null && y == null)
                return true;

            if (x.Data.ModuleType.Length != y.Data.ModuleType.Length)
                return false;

            for (int i =0; i < x.Data.ModuleType.Length; ++i)
            {
                if (x.Data.ModuleType [i] != y.Data.ModuleType [i])
                    return false;
            }
            return true;
        }

        public int GetHashCode(MotherBoardState obj)
        {
            return obj.GetHashCode();
        }
    }

}