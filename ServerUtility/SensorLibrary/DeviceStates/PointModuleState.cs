using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace SensorLibrary
{

    public class PointModuleState
        : DeviceState<PointModuleData>
    {
        public const byte MAX_ADDRESS_COUNT = 8;

        public PointModuleState(DevicePacket packet, PointModuleData data, PacketServer server)
            : base(packet, data, server) { }

        public PointModuleState(DevicePacket packet)
            : this(packet, null, null)
        {
        }

        public PointStateEnum GetPointState(int addr)
        {
            if (addr >= StateLength)
                throw new IndexOutOfRangeException("インデックスが使用できる範囲を超えています");

            int arindex = addr / 8;
            int bitindex = addr % 8;
            return (PointStateEnum)((this.Data.Directions[arindex] & (1 << bitindex)) >> bitindex);
        }

        public void SetPointState(int addr, PointStateEnum state)
        {
            if (addr >= StateLength)
                throw new IndexOutOfRangeException("インデックスが使用できる範囲を超えています");

            int arindex = addr / 8;
            int bitindex = addr % 8;

            this.Data.Directions[arindex] &= (byte)(~(1 << bitindex));
            this.Data.Directions[arindex] |= (byte)((uint)(state) << bitindex);
        }

        public int StateLength
        {
            get { return this.Data.Directions.Length * 8; }
        }

        public PointStateEnum this[int addr]
        {
            get
            {
                return GetPointState(addr);
            }
            set
            {
                this.SetPointState(addr, value);
            }
        }
    }

}