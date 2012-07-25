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

        public PointModuleState()
            : base() { }


        public PointStateEnum GetPointState(int addr)
        {
            if (addr >= StateLength)
                throw new IndexOutOfRangeException("インデックスが使用できる範囲を超えています");

            return (PointStateEnum)(this.Data.Directions [addr] & 1);
        }

        public void SetPointState(int addr, PointStateEnum state)
        {
            if (addr >= StateLength)
                throw new IndexOutOfRangeException("インデックスが使用できる範囲を超えています");

            this.Data.Directions [addr] = (byte)state;
        }

        public int StateLength
        {
            get { return this.Data.Directions.Length; }
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