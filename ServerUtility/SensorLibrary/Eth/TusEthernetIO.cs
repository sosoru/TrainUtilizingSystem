using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SensorLibrary
{
    public class TusEthernetIO
        : IDeviceIO
    {
        public int Port { get; private set; }
        public UInt32 IpBase { get; private set; }
        public UInt32 IpMask { get; private set; }

        protected UdpClient Client { get; private set; }

        public TusEthernetIO(UInt32 ipbase, UInt32 mask, int port)
        {
            this.IpBase = ipbase;
            this.IpMask = mask;
            this.Client = new UdpClient(port);
        }

        private IPEndPoint ToEndPoint(DeviceID id)
        {
            var ip = (this.IpBase & this.IpMask) | (UInt32)id.ModulePart;

            return new IPEndPoint((long)ip, this.Port);
        }

        public DevicePacket ReadPacket()
        {
            var end = new IPEndPoint(IPAddress.Any, this.Port);

            var dgram = this.Client.Receive(ref end);
            var packet = dgram.ToDevicePacket();

            return packet;
        }

        public void WritePacket(DevicePacket packet)
        {
            var end = ToEndPoint(packet.ID);
            var dgram = packet.ToByteArray();

            this.Client.Send(dgram, dgram.Length, end);
        }
    }

}
