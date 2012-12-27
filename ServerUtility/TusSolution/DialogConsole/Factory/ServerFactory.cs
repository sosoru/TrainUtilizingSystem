using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Net;

using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Control;
using SensorLibrary.Packet.Data;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using SensorLibrary.Packet.IO;

using RouteLibrary;
using RouteLibrary.Base;
using RouteLibrary.Parser;

namespace DialogConsole.Factory
{
    using DialogConsole.Properties;

    [Export]
    class ServerFactory
    {
        [DefaultValue(Settings.Default.IpSegment)]
        public string IpSegment { get; set; }

        [DefaultValue(Settings.Default.IpMask)]
        public string IpMask   { get; set;}

        [DefaultValue(Settings.Default.MyDeviceID)]
        public string MyDeviceID { get; set; }

        [DefaultValue(Settings.Default.IpPort)]
        public int IpPort { get; set; }

        public PacketServer Create()
        {
            var ipbase = IPAddress.Parse(DialogConsole.Properties.Settings.Default.IpSegment);
            var ipmask = IPAddress.Parse(DialogConsole.Properties.Settings.Default.IpMask);

            var dialog = new DialogCnosole();
            var io = new TusEthernetIO(ipbase, ipmask)
            {
                SourceID = new DeviceIdParser().FromString(DialogConsole.Properties.Settings.Default.MyDeviceID).First(),
                Port = DialogConsole.Properties.Settings.Default.IpPort,
            };

           var serv = new PacketServer(new AvrDeviceFactoryProvider());
           serv.Controller = io;

           return serv;
        }
    }
}
