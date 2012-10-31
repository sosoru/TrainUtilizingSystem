using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;
using System.Reactive.Linq;
using System.Reactive;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Data;
using SensorLibrary.Devices;

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

        public DeviceID(ushort parent, byte module, byte inter)
        {
            this.ParentPart = parent;
            this.ModulePart = 0;
            this.ModuleAddr = module;
            this.InternalAddr = inter;
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

        public int GetUniqueIdByBoard()
        {
            return this.ParentPart.GetHashCode() ^ this.ModuleAddr.GetHashCode();
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
        //[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        //public byte ReadMark = 0xFF;

        [MarshalAs(UnmanagedType.Struct, SizeConst = 4)]
        public DeviceID ID;

        //[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        //public ModuleTypeEnum ModuleType;

        public const int DATA_SIZE = 26;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
        private byte[] _data = new byte[DATA_SIZE];
        public byte[] Data
        {
            get
            {
                if (_data == null)
                    _data = new byte[DATA_SIZE];

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

            return string.Format("({0},{1}){2}", ID.ParentPart, ID.ModulePart, sb.ToString());
        }

        public IEnumerable<IDeviceState<IPacketDeviceData>> ExtractPackedPacket()
        {
            var bufind = 0;
            var factory = new SensorLibrary.Devices.AvrDeviceFactoryProvider();

            while (bufind <= DATA_SIZE && this.Data[bufind] != 0x00)
            {
                var len = this.Data[bufind];
                var internelid = this.Data[bufind + 1];
                var mtype = (ModuleTypeEnum)this.Data[bufind + 2];
                var f = factory.AvailableDeviceTypes.First(a => a.ModuleType == mtype);
                var state = f.DeviceStateCreate();
                var data = state.Data;
                var cpbuffer = new byte[len];

                state.ID = new DeviceID(state.ID.ParentPart, state.ID.ModuleAddr, internelid);

                Array.Copy(this.Data, bufind, cpbuffer, 0, len);
                data.RestoreObject(cpbuffer);

                yield return state;

                bufind += len;
            }

        }

        public static IEnumerable<DevicePacket> CreatePackedPacket(params IDevice<IDeviceState<IPacketDeviceData>>[] devs)
        {
            return CreatePackedPacketInternal(devs);
        }

        public static IEnumerable<DevicePacket> CreatePackedPacket(IEnumerable<IDevice<IDeviceState<IPacketDeviceData>>> devs)
        {
            return CreatePackedPacketInternal(devs);
        }

        private static IEnumerable<DevicePacket> CreatePackedPacketInternal(IEnumerable<IDevice<IDeviceState<IPacketDeviceData>>> devenumerator)
        {
            if (devenumerator == null || !devenumerator.Any())
                return new DevicePacket[] { };

            var devs = devenumerator.Select((a, ind) => new { ind = ind, val = a });
            var pack = new DevicePacket() { ID = devs.First().val.DeviceID };

            using (var mst = new MemoryStream(pack.Data))
            {
                foreach (var dev in devs)
                {
                    var data = dev.val.CurrentState.Data;
                    data.InternalAddr = dev.val.DeviceID.InternalAddr;

                    if (mst.Position + data.DataLength > pack.Data.Length)
                    {
                        return new[] { pack }.Concat(CreatePackedPacket(devenumerator.Skip(dev.ind)));
                    }
                    else
                    {
                        mst.Write(data.ToByteArray(), 0, data.DataLength);
                    }
                }
            }

            return new[] { pack };
        }



    }

    [StructLayout(LayoutKind.Sequential, Size = 40)]
    public class EthPacket
    {
        public EthPacket()
        {
            this.DataPacket = new DevicePacket();
        }

        public DeviceID srcId;
        public DeviceID destId;

        public DevicePacket DataPacket;

    }

    public interface IPacketDeviceData
    {
        byte InternalAddr { get; set; }
        byte DataLength { get; set; }
        byte ModuleType { get; set; }
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

        AvrKernel = 0x11,
        AvrMotor = 0x12,
        AvrSwitch = 0x13,
        AvrSensor = 0x14,
        AvrUartSetting = 0x15,

        Unknown = 0x0F,
    }


}