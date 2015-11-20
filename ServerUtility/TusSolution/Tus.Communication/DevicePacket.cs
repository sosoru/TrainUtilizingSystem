using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;
using System.Reactive.Linq;
using System.Reactive;
namespace Tus.Communication
{
    [StructLayout(LayoutKind.Sequential, Size = 50)]
    public class DevicePacket
    {
        //[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        //public byte ReadMark = 0xFF;

        [MarshalAs(UnmanagedType.Struct, SizeConst = 4)]
        public DeviceID ID;

        //[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        //public ModuleTypeEnum ModuleType;


        public const int DATA_SIZE = 46;
        public const int PACKET_SIZE = 50;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 46)]
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



    }
}
