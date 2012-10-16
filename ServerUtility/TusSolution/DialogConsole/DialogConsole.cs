using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Control;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using SensorLibrary.Packet.IO;

namespace DialogConsole
{
    class DialogCnosole
    {
        static void Main(string[] args)
        {

        }
    }

    public class DeviceDiagnostics
    {
        private PacketServer serv;
        private PacketDispatcher dispat;
        private DeviceID devid;

        public Stream Writer { get; private set;}
        public Stream Reader { get; private set;}

        public DeviceDiagnostics(DeviceID id, IPAddress baseip)
        {
            this.devid = new DeviceID(baseip.GetAddressBytes()[3], 0,0);
            var io = new SensorLibrary.Packet.IO.TusEthernetIO(baseip, new IPAddress(new byte[]{255,255,255,0})
            {
                SourceID = devid,
                Port = 8000,
            };
            this.serv  = new PacketServer(new AvrDeviceFactoryProvider())
                {
                    Controller = io,
                };
            
            this.dispat = new PacketDispatcher();
            this.serv.AddAction(this.dispat);

        }

        public void Start(Stream reader, Stream writer)
        {
            this.serv.LoopStart();
            
            if(!reader.CanRead)
                throw new InvalidOperationException("requires readable stream");

            if(!writer.CanWrite)
                throw new InvalidOperationException("requires writable stream");

            this.Reader = reader;
            this.Writer = writer;}

        }


}
