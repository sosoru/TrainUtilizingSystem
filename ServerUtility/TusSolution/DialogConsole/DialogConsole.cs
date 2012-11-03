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
using SensorLibrary.Devices.TusAvrDevices;

using RouteLibrary;
using RouteLibrary.Base;
using RouteLibrary.Parser;

namespace DialogConsole
{
    class DialogCnosole
    {
        static void Main(string[] args)
        {
            var serv = CreateServer(IPAddress.Parse("192.168.2.0"), IPAddress.Parse("255.255.255.0"));
            var sht = CreateSheet("layout.yaml", serv);



        }

        static void ChangeSwitchAll(BlockSheet sht,  PointStateEnum pos)
        {
            sht.InquiryAllMotors
        }

        static void Detect(BlockSheet sht)
        {
            sht.ChangeDetectingMode();

            System.Threading.Thread.Sleep(3000);
            Console.WriteLine(sht.InnerBlocks
               .Where(b => b.IsDetectingTrain)
                .Aggregate("", (ac, b) => ac += b.Name + ", "));
        }

        static Route InputRoute(BlockSheet sht)
        {
            Console.WriteLine("route?");
            var content = Console.ReadLine();

            try
            {
                var spil = content.Split(',');
                var rt = new Route(sht, spil.Select(s => sht.GetBlock(s)));

                return rt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static PacketServer CreateServer(IPAddress ipbase, IPAddress ipmask)
        {
            var serv = new PacketServer(new AvrDeviceFactoryProvider());
            var io = new SensorLibrary.Packet.IO.TusEthernetIO(ipbase, ipmask)
            {
                SourceID = new DeviceID(9, 0, 0),
                Port = 8000,
            };

            serv.Controller = io;

            serv.LoopStart();
            return serv;
        }

        static BlockSheet CreateSheet(string path, PacketServer serv)
        {
            var parser = new BlockYaml();
            var blocks = parser.Parse(path);

            return new BlockSheet(blocks, serv);
        }


    }

    public class DeviceDiagnostics
    {
        private PacketServer serv;
        private PacketDispatcher dispat;
        private DeviceID devid;

        public Stream Writer { get; private set; }
        public Stream Reader { get; private set; }

        public DeviceDiagnostics(DeviceID id, IPAddress baseip)
        {
            this.devid = new DeviceID(baseip.GetAddressBytes()[3], 0, 0);
            var io = new SensorLibrary.Packet.IO.TusEthernetIO(baseip, new IPAddress(new byte[] { 255, 255, 255, 0 }))
            {
                SourceID = devid,
                Port = 8000,
            };
            this.serv = new PacketServer(new AvrDeviceFactoryProvider())
                {
                    Controller = io,
                };

            this.dispat = new PacketDispatcher();
            this.serv.AddAction(this.dispat);

        }

        public void Start(Stream reader, Stream writer)
        {
            this.serv.LoopStart();

            if (!reader.CanRead)
                throw new InvalidOperationException("requires readable stream");

            if (!writer.CanWrite)
                throw new InvalidOperationException("requires writable stream");

            this.Reader = reader;
            this.Writer = writer;

        }

        public TDev CreateDevice<TDev>(DeviceID devid)
            where TDev : Device<IDeviceState<IPacketDeviceData>>, new()
        {
            var dev = new TDev() { DeviceID = devid };
            dev.ReceivingServer = this.serv;
            dev.Observe(this.dispat);

            return dev;
        }

    }
}
