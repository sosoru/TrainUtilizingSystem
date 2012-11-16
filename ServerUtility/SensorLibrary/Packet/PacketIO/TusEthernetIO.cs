using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Reactive;
using System.Reactive.Linq;

namespace SensorLibrary.Packet.IO
{
    public class TusEthernetIO
        : IDeviceIO
    {
        public int Port { get; set; }
        public UInt32 IpBase { get; private set; }
        public UInt32 IpMask { get; private set; }
        public DeviceID SourceID { get; set; }

        protected EthClient Client { get; private set; }

        public TusEthernetIO(IPAddress ipbase, IPAddress mask)
            : this(BitConverter.ToUInt32(ipbase.GetAddressBytes(), 0), BitConverter.ToUInt32(mask.GetAddressBytes(), 0)) { }

        public TusEthernetIO(UInt32 ipbase, UInt32 mask)
        {
            this.IpBase = ipbase;
            this.IpMask = mask;
            this.Client = new EthClient();
        }

        private IPEndPoint ToEndPoint(DeviceID id)
        {
            var ip = (this.IpBase & this.IpMask) | ((UInt32)id.ParentPart << 24);
            var end = new IPEndPoint((long)ip, this.Port);

            return end;
        }

        public DevicePacket ReadPacket()
        {
            try
            {
                var ob = this.Client.AsyncReceive()
                    .Timeout(TimeSpan.FromMilliseconds(100))
                    .First();

                return ob.DataPacket;
            }
            catch (TimeoutException ex)
            {
                return null;
            }

        }

        public void WritePacket(DevicePacket packet)
        {
            var end = ToEndPoint(packet.ID);
            var eth = new EthPacket()
            {
                srcId = SourceID,
                destId = packet.ID,
                DataPacket = packet,
            };
            //this.Client.Connect(end);
            this.Client.Address = end.Address;
            this.Client.Send(eth);
            //this.Client.Close();
        }

        public IObservable<Unit> GetWritingPacket(DevicePacket pack)
        {
            this.Client.Address = ToEndPoint(pack.ID).Address;
            var eth = new EthPacket()
                          {
                              srcId = SourceID,
                              destId = pack.ID,
                              DataPacket = pack,
                          };
            return this.Client.AsyncSend(eth);
        }

        public IObservable<DevicePacket> GetReadingPacket()
        {
            return this.Client.AsyncReceive()
                                .Select(p => p.DataPacket);
        }
    }

}
