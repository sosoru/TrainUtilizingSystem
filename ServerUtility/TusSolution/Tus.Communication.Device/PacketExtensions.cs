using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.IO;

namespace Tus.Communication
{
    using Tus.Communication.Device.Composition;

    public static class PacketExtension
    {
        public static DevicePacket ReadPacket(this ChunckedStreamController st)
        {
            //waiting readmark;
            //while (st.ReadByte() != 0xFF) ;
            st.ReadByte();

            byte[] buf = new byte [31];
            st.Read(buf, 0, buf.Length);

            return buf.ToDevicePacket();

        }

        public static DevicePacket ToDevicePacket(this IDevice<IDeviceState<IPacketDeviceData>> dev)
        {
            var data = dev.CurrentState.Data;

            var packet = new DevicePacket()
            {
                ID = dev.DeviceID,
            };

            packet.CopyToData(data);
            //packet.ModuleType = dev.ModuleType;

            return packet;
        }

        public static IEnumerable<IDeviceState<IPacketDeviceData>> ExtractPackedPacket(this DevicePacket packet)
        {
            var bufind = 0;

            while (bufind <= DevicePacket.DATA_SIZE && packet.Data[bufind] != 0x00)
            {
                var len = packet.Data[bufind];
                var internelid = packet.Data[bufind + 1];
                var mtype = (ModuleTypeEnum)packet.Data[bufind + 2];
                var f = AvrDeviceFactoryProvider.Factories.Value.First(p => p.Metadata.ModuleType == mtype).Value;
                var state = f.CreateDeviceState();
                var data = state.Data;
                var cpbuffer = new byte[len];

                state.ID = new DeviceID(packet.ID.ParentPart, packet.ID.ModuleAddr, internelid);

                Array.Copy(packet.Data, bufind, cpbuffer, 0, len);
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

        //public static void WritePacket(this ChunckedStreamController st, DevicePacket pack)
        //{
        //    byte[] buf = new byte [32];
        //    using (var ms = new MemoryStream(buf))
        //    using (var sw = new BinaryWriter(ms))
        //    {
        //        sw.Write((byte)0xFF); // readmark
        //        sw.Write(pack.ID.ParentPart);
        //        sw.Write(pack.ID.ModulePart);
        //        sw.Write((byte)pack.ModuleType);
        //        sw.Write(pack.Data, 0, 28);
        //    }

        //    st.Write(buf, 0, buf.Length);
        //}
    }

}