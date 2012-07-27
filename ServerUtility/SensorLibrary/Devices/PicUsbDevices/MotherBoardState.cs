using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

using SensorLibrary.Packet.Data;

namespace SensorLibrary.Devices.PicUsbDevices
{
    public class MotherBoardState
    : DeviceState<MotherBoardData>
    {
        public MotherBoardState()
            : base()
        {
        }

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

        public IDevice<IDeviceState<IPacketDeviceData>> GetDevice(int addr)
        {
            var mtype =  GetModuleType(addr);
            var fact = new PicDeviceFactoryProvider().AvailableDeviceTypes.FirstOrDefault((f) => f.ModuleType == mtype);

            if (fact == null)
                return null; // throw new InvalidCastException("invalid packet thrown considering strange module type");
            var dev = fact.DeviceCreate();
            dev.DeviceID = new DeviceID()
            {
                ParentPart = this.ParentID,
                ModulePart = (byte)addr,
            };
            return dev;
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

        public IEnumerable<IDevice<IDeviceState<IPacketDeviceData>>> DeviceEnumerate()
        {
            var len = this.ModuleTypeLength;
            for (int i=0; i < len; ++i)
            {
                var dev = this.GetDevice(i);
                if (dev == null)
                    continue;

                yield return dev;
            }
        }

        public IEnumerable<ModuleTypeEnum> ModuleTypeEnumerate()
        {
            var len = this.ModuleTypeLength;
            for (int i =0; i < len; ++i)
                yield return this [i];
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